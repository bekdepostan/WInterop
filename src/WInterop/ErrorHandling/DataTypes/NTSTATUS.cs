﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.ErrorHandling.DataTypes
{
    /// <summary>
    /// NtStatus defines.
    /// </summary>
    public enum NTSTATUS : int
    {
        // NTSTATUS values
        // https://msdn.microsoft.com/en-us/library/cc704588.aspx
        STATUS_SUCCESS = 0x00000000,

        /// <summary>
        /// Returned by enumeration APIs to indicate more information is available to successive calls.
        /// </summary>
        STATUS_MORE_ENTRIES = 0x00000105,

        /// <summary>
        /// {Buffer Overflow} The data was too large to fit into the specified buffer.
        /// </summary>
        STATUS_BUFFER_OVERFLOW = unchecked((int)0x80000005),

        /// <summary>
        /// {Operation Failed} The requested operation was unsuccessful.
        /// </summary>
        STATUS_UNSUCCESSFUL = unchecked((int)0xC0000001),

        /// <summary>
        /// {Not Implemented} The requested operation is not implemented.
        /// </summary>
        STATUS_NOT_IMPLEMENTED = unchecked((int)0xC0000002),

        /// <summary>
        /// {Invalid Parameter} The specified information class is not a valid information class for the specified object.
        /// </summary>
        STATUS_INVALID_INFO_CLASS = unchecked((int)0xC0000003),

        /// <summary>
        /// The specified information record length does not match the length that is required for the specified information class.
        /// </summary>
        STATUS_INFO_LENGTH_MISMATCH = unchecked((int)0xC0000004),

        /// <summary>
        /// An invalid HANDLE was specified.
        /// </summary>
        STATUS_INVALID_HANDLE = unchecked((int)0xC0000008),

        /// <summary>
        /// An invalid parameter was passed to a service or function.
        /// </summary>
        STATUS_INVALID_PARAMETER = unchecked((int)0xC000000D),

        /// <summary>
        /// {Access Denied} A process has requested access to an object but has not been granted those access rights.
        /// </summary>
        STATUS_ACCESS_DENIED = unchecked((int)0xC0000022),

        /// <summary>
        /// {Buffer Too Small} The buffer is too small to contain the entry. No information has been written to the buffer.
        /// </summary>
        STATUS_BUFFER_TOO_SMALL = unchecked((int)0xC0000023),

        /// <summary>
        /// The object name is invalid.
        /// </summary>
        STATUS_OBJECT_NAME_INVALID = unchecked((int)0xC0000033),

        /// <summary>
        /// The object name is not found.
        /// </summary>
        STATUS_OBJECT_NAME_NOT_FOUND = unchecked((int)0xC0000034),

        /// <summary>
        /// The object name already exists.
        /// </summary>
        STATUS_OBJECT_NAME_COLLISION = unchecked((int)0xC0000035),

        /// <summary>
        /// The object path component was not a directory object.
        /// </summary>
        STATUS_OBJECT_PATH_INVALID = unchecked((int)0xC0000039),

        /// <summary>
        /// {Path Not Found} The path does not exist.
        /// </summary>
        STATUS_OBJECT_PATH_NOT_FOUND = unchecked((int)0xC000003A),

        /// <summary>
        /// The object path component was not a directory object.
        /// </summary>
        STATUS_OBJECT_PATH_SYNTAX_BAD = unchecked((int)0xC000003B),

        /// <summary>
        /// Insufficient system resources exist to complete the API.
        /// </summary>
        STATUS_INSUFFICIENT_RESOURCES = unchecked((int)0xC000009A)
    }
}
