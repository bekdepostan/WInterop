﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.ErrorHandling;
using WInterop.Handles.DataTypes;

namespace WInterop.VolumeManagement.DataTypes
{
    /// <summary>
    /// Safe handle for a block of memory returned by GetEnvironmentStrings.
    /// </summary>
    public class SafeFindVolumeHandle : SafeHandleZeroIsInvalid
    {
        public SafeFindVolumeHandle() : base(ownsHandle: true)
        {
        }

        protected override bool ReleaseHandle()
        {
            if (!VolumeDesktopMethods.Direct.FindVolumeClose(handle))
            {
                throw ErrorHelper.GetIoExceptionForLastError();
            }

            handle = IntPtr.Zero;
            return true;
        }
    }
}
