﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Handles.DataTypes
{
    /// <summary>
    /// Information types returned by NtQueryObject. See <a href="https://msdn.microsoft.com/en-us/library/windows/hardware/ff550964.aspx">OBJECT_INFORMATION_CLASS</a>.
    /// </summary>
    public enum OBJECT_INFORMATION_CLASS
    {
        /// <summary>
        /// Gets attributes, access info, and handle/pointer counts
        /// </summary>
        ObjectBasicInformation,

        /// <summary>
        /// Get the name of the object. Typically the NT path to the object.
        /// Indirectly documented at <a href="https://msdn.microsoft.com/en-us/library/windows/hardware/ff548474.aspx">IoQueryFileDosDeviceName</a>.
        /// </summary>
        ObjectNameInformation,

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        ObjectTypeInformation,

        /// <summary>
        /// Gets all known types.
        /// </summary>
        // https://ntquery.wordpress.com/2014/03/30/anti-debug-ntqueryobject/#more-21
        ObjectTypesInformation
    }
}
