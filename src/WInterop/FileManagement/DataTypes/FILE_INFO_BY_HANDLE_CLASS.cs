﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364228.aspx
    // Note that support is gleaned from documentation for the structures- Ge/SetFileInformationByHandle
    // aren't themselves filtered. Some may work that claim they don't.
    public enum FILE_INFO_BY_HANDLE_CLASS : uint
    {
        // Supported in Store apps, thunks to NtQueryInformationFile
        FileBasicInfo = 0,
        // Supported in Store apps, thunks to NtQueryInformationFile
        FileStandardInfo = 1,
        // Supported in Store apps, thunks to NtQueryInformationFile
        FileNameInfo = 2,
#if !(COMPACT)
        // For setting, Desktop only (MoveFileEx is supported)
        FileRenameInfo = 3,
        // For setting, Desktop only (DeleteFile is supported)
        FileDispositionInfo = 4,
        // For setting, Desktop only
        FileAllocationInfo = 5,
        // For setting, Desktop only (SetEndOfFile is supported)
        FileEndOfFileInfo = 6,
#endif
        // Supported in Store apps, thunks to NtQueryInformationFile
        FileStreamInfo = 7,
#if !(COMPACT)
        // Supported in Store apps, thunks to NtQueryInformationFile
        FileCompressionInfo = 8,
        // Supported in Store apps, thunks to NtQueryInformationFile
        FileAttributeTagInfo = 9,
        // Supported in Store apps, thunks to NtQueryDirectoryFile
        FileIdBothDirectoryInfo = 10, // 0xA
        // Supported in Store apps, thunks to NtQueryDirectoryFile
        FileIdBothDirectoryRestartInfo = 11, // 0xB
        // For setting, Desktop only
        FileIoPriorityHintInfo = 12, // 0xC
        // For setting, Desktop only
        FileRemoteProtocolInfo = 13, // 0xD
        // Desktop only, thunks to NtQueryDirectoryFile
        FileFullDirectoryInfo = 14, // 0xE
        // Desktop only, thunks to NtQueryDirectoryFile
        FileFullDirectoryRestartInfo = 15, // 0xF
        // Supported in Store apps, thunks to NtQueryVolumeInformationFile
        FileStorageInfo = 16, // 0x10
        // Supported in Store apps, thunks to NtQueryInformationFile
        FileAlignmentInfo = 17, // 0x11
        // Desktop only, thunks to NtQueryInformationFile
        FileIdInfo = 18, // 0x12
        // Server desktop only, thunks to NtQueryDirectoryFile
        FileIdExtdDirectoryInfo = 19, // 0x13
        // Server desktop only, thunks to NtQueryDirectoryFile
        FileIdExtdDirectoryRestartInfo = 20, // 0x14
        MaximumFileInfoByHandlesClass = 21
#endif
    }
}
