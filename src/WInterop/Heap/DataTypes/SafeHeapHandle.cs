﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WInterop.Support.Buffers;

namespace WInterop.Heap.DataTypes
{
    /// <summary>
    /// Handle for heap memory
    /// </summary>
    /// <remarks>
    /// Uses new Heap* methods instead of Local* methods, which are depreciated.  While both calls utilize the same underlying
    /// heap allocation, Local* adds some overhead (*significant* overhead if LMEM_MOVEABLE is used). .NET forces LMEM_FIXED
    /// with LocalAlloc for Marshal.AllocHGlobal so it doesn't hit the super slow path.
    /// 
    /// Windows attempts to grab space from the low fragmentation heap if the requested memory is below a platform specific
    /// threshold and certain flags aren't in play (such as NO_SERIALIZE).
    /// </remarks>
    public class SafeHeapHandle : SafeBuffer, ISizedBuffer
    {
        /// <summary>
        /// Create an empty heap handle
        /// </summary>
        public SafeHeapHandle() : this(0) { }

        /// <summary>
        /// Allocate a buffer of the given size and zero memory if requested.
        /// </summary>
        /// <param name="nameof(byteLength)">Required size in bytes. Must be less than UInt32.MaxValue for 32 bit or UInt64.MaxValue for 64 bit.</param>
        /// <exception cref="OutOfMemoryException">Thrown if the requested memory size cannot be allocated.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if size is greater than the maximum memory size.</exception>
        public SafeHeapHandle(ulong byteLength, bool zeroMemory = false) : base(ownsHandle: true)
        {
            if (byteLength > 0)
            {
                Resize(byteLength, zeroMemory);
            }
            else
            {
                Initialize(0);
            }
        }

        public ulong ByteCapacity { get { return ByteLength; } }

        public override bool IsInvalid
        {
            get { return handle == IntPtr.Zero; }
        }

        /// <summary>
        /// Resize the buffer to the given size and zero memory if requested.
        /// </summary>
        /// <param name="nameof(byteLength)">Required size in bytes. Must be less than UInt32.MaxValue for 32 bit or UInt64.MaxValue for 64 bit.</param>
        /// <exception cref="OutOfMemoryException">Thrown if the requested memory size cannot be allocated.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if size is greater than the maximum memory size.</exception>
        public void Resize(ulong byteLength, bool zeroMemory = false)
        {
            if (IsClosed) throw new ObjectDisposedException("HeapHandle");

            if (handle == IntPtr.Zero)
            {
                handle = Heap.HeapMethods.HeapAllocate(byteLength, zeroMemory);
            }
            else
            {
                // This may or may not be the same handle, Windows may realloc in place. If the
                // handle changes Windows will deal with the old handle, trying to free it will
                // cause an error.
                handle = Heap.HeapMethods.HeapReallocate(handle, byteLength, zeroMemory);
            }

            if (handle == IntPtr.Zero)
            {
                // Only real plausible answer
                throw new OutOfMemoryException();
            }

            Initialize(byteLength);
        }

        protected override bool ReleaseHandle()
        {
            bool success = Heap.HeapMethods.HeapFree(handle);
            Debug.Assert(success);
            handle = IntPtr.Zero;
            return success;
        }
    }
}
