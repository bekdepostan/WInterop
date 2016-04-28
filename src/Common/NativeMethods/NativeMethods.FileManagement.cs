﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using WInterop.Authorization;
using WInterop.Buffers;
using WInterop.ErrorHandling;
using WInterop.FileManagement;
using WInterop.Handles;
using WInterop.Synchronization;
using WInterop.Utility;

namespace WInterop
{
    public static partial class NativeMethods
    {
        public static partial class FileManagement
        {
            /// <summary>
            /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
            /// </summary>
            /// <remarks>
            /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
            /// </remarks>
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            public static class Direct
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449422.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_2_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern SafeFileHandle CreateFile2(
                    string lpFileName,
                    uint dwDesiredAccess,
                    [MarshalAs(UnmanagedType.U4)] System.IO.FileShare dwShareMode,
                    [MarshalAs(UnmanagedType.U4)] System.IO.FileMode dwCreationDisposition,
                    [In] ref CREATEFILE2_EXTENDED_PARAMETERS pCreateExParams);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364946.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool GetFileAttributesExW(
                    string lpFileName,
                    GET_FILEEX_INFO_LEVELS fInfoLevelId,
                    out WIN32_FILE_ATTRIBUTE_DATA lpFileInformation);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365535.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool SetFileAttributesW(
                    string lpFileName,
                    FileAttributes dwFileAttributes);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364963.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern uint GetFullPathNameW(
                    string lpFileName,
                    uint nBufferLength,
                    SafeHandle lpBuffer,
                    IntPtr lpFilePart);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364419.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                public static extern SafeFindHandle FindFirstFileExW(
                     string lpFileName,
                     FINDEX_INFO_LEVELS fInfoLevelId,
                     out WIN32_FIND_DATA lpFindFileData,
                     FINDEX_SEARCH_OPS fSearchOp,
                     IntPtr lpSearchFilter,                 // Reserved
                     int dwAdditionalFlags);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364428.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool FindNextFileW(
                    SafeFindHandle hFindFile,
                    out WIN32_FIND_DATA lpFindFileData);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364413.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool FindClose(
                    IntPtr hFindFile);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364953.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l2_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool GetFileInformationByHandleEx(
                    SafeFileHandle hFile,
                    FILE_INFO_BY_HANDLE_CLASS FileInformationClass,
                    IntPtr lpFileInformation,
                    uint dwBufferSize);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363915.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool DeleteFileW(
                    string lpFilename);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365467.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                unsafe public static extern bool ReadFile(
                    SafeFileHandle hFile,
                    byte* lpBuffer,
                    uint nNumberOfBytesToRead,
                    out uint lpNumberOfBytesRead,
                    ref OVERLAPPED lpOverlapped);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364960.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                public static extern FileType GetFileType(
                    SafeFileHandle hFile);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364439.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool FlushFileBuffers(
                    SafeFileHandle hFile);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364992.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_2_0, SetLastError = true, ExactSpelling = true)]
                public static extern uint GetTempPathW(
                    uint nBufferLength,
                    SafeHandle lpBuffer);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364991.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern uint GetTempFileNameW(
                    string lpPathName,
                    string lpPrefixString,
                    uint uUnique,
                    SafeHandle lpTempFileName);
            }

            public static string GetTempPath()
            {
                return BufferInvoke((buffer) => Direct.GetTempPathW(buffer.CharCapacity, buffer));
            }

            public static string GetFullPathName(string path)
            {
                return BufferInvoke((buffer) => Direct.GetFullPathNameW(path, buffer.CharCapacity, buffer, IntPtr.Zero));
            }

            public static string GetTempFileName(string path, string prefix)
            {
                return StringBufferCache.CachedBufferInvoke((buffer) =>
                {
                    buffer.EnsureCharCapacity(Paths.MaxPath);
                    uint result = Direct.GetTempFileNameW(
                        lpPathName: path,
                        lpPrefixString: prefix,
                        uUnique: 0,
                        lpTempFileName: buffer);

                    if (result == 0) throw ErrorHelper.GetIoExceptionForLastError(path);

                    buffer.SetLengthToFirstNull();
                    return buffer.ToString();
                });
            }

            public static void DeleteFile(string path)
            {
                if (!Direct.DeleteFileW(path))
                    throw ErrorHelper.GetIoExceptionForLastError(path);
            }

            /// <summary>
            /// CreateFile wrapper that attempts to use CreateFile2 if running as Windows Store app.
            /// </summary>
            public static SafeFileHandle CreateFile(
                string path,
                System.IO.FileAccess fileAccess,
                System.IO.FileShare fileShare,
                System.IO.FileMode creationDisposition,
                FileAttributes fileAttributes = FileAttributes.NONE,
                FileFlags fileFlags = FileFlags.NONE,
                SecurityQosFlags securityQosFlags = SecurityQosFlags.NONE)
            {
                // We could also potentially add logic to use CreateFile2 if we're at Win8 or greater. Version checking can only be done if
                // we are running as a normal desktop app.

#if !WINRT
                if (Utility.Environment.IsWindowsStoreApplication())
#endif
                {
                    return CreateFile2(path, fileAccess, fileShare, creationDisposition, fileAttributes, fileFlags, securityQosFlags);
                }
#if !WINRT
                else
                {
                    return Desktop.CreateFileW(path, fileAccess, fileShare, creationDisposition, fileAttributes, fileFlags, securityQosFlags);
                }
#endif
            }

            public static SafeFileHandle CreateFile2(
                string path,
                System.IO.FileAccess fileAccess,
                System.IO.FileShare fileShare,
                System.IO.FileMode creationDisposition,
                FileAttributes fileAttributes = FileAttributes.NONE,
                FileFlags fileFlags = FileFlags.NONE,
                SecurityQosFlags securityQosFlags = SecurityQosFlags.NONE)
            {
                if (creationDisposition == System.IO.FileMode.Append) creationDisposition = System.IO.FileMode.OpenOrCreate;

                uint dwDesiredAccess =
                    ((fileAccess & System.IO.FileAccess.Read) != 0 ? (uint)GenericAccessRights.GENERIC_READ : 0) |
                    ((fileAccess & System.IO.FileAccess.Write) != 0 ? (uint)GenericAccessRights.GENERIC_WRITE : 0);

                CREATEFILE2_EXTENDED_PARAMETERS extended = new CREATEFILE2_EXTENDED_PARAMETERS();
                extended.dwSize = (uint)Marshal.SizeOf<CREATEFILE2_EXTENDED_PARAMETERS>();
                extended.dwFileAttributes = fileAttributes;
                extended.dwFileFlags = fileFlags;
                extended.dwSecurityQosFlags = securityQosFlags;
                extended.lpSecurityAttributes = IntPtr.Zero;
                extended.hTemplateFile = IntPtr.Zero;

                SafeFileHandle handle = Direct.CreateFile2(
                    lpFileName: path,
                    dwDesiredAccess: dwDesiredAccess,
                    dwShareMode: fileShare,
                    dwCreationDisposition: creationDisposition,
                    pCreateExParams: ref extended);

                if (handle.IsInvalid)
                    throw ErrorHelper.GetIoExceptionForLastError(path);

                return handle;
            }

            public static FileInfo GetFileAttributesEx(string path)
            {
                WIN32_FILE_ATTRIBUTE_DATA data;
                if (!Direct.GetFileAttributesExW(path, GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, out data))
                    throw ErrorHelper.GetIoExceptionForLastError(path);

                return new FileInfo(data);
            }

            public static void SetFileAttributes(string path, FileAttributes attributes)
            {
                if (!Direct.SetFileAttributesW(path, attributes))
                    throw ErrorHelper.GetIoExceptionForLastError(path);
            }

            public static void FlushFileBuffers(SafeFileHandle fileHandle)
            {
                if (!Direct.FlushFileBuffers(fileHandle))
                    throw ErrorHelper.GetIoExceptionForLastError();
            }

            /// <summary>
            /// Gets the file name from the given handle. This uses GetFileInformationByHandleEx, which does not give back the drive
            /// name for the path- but is available from Windows Store apps.
            /// </summary>
            public static string GetFileNameByHandle(SafeFileHandle fileHandle)
            {
                return StringBufferCache.CachedBufferInvoke(Paths.MaxPath, (buffer) =>
                {
                    while (!Direct.GetFileInformationByHandleEx(fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileNameInfo, buffer.DangerousGetHandle(), checked((uint)buffer.ByteCapacity)))
                    {
                        uint error = (uint)Marshal.GetLastWin32Error();
                        if (error != WinErrors.ERROR_MORE_DATA)
                            throw ErrorHelper.GetIoExceptionForError(error);
                        buffer.EnsureByteCapacity(buffer.ByteCapacity * 2);
                    }

                    var reader = new NativeBufferReader(buffer);
                    return reader.ReadString((int)(reader.ReadUint() / 2));
                });
            }

            public static FILE_STANDARD_INFO GetFileStandardInfoByHandle(SafeFileHandle fileHandle)
            {
                return StringBufferCache.CachedBufferInvoke((buffer) =>
                {
                    FILE_STANDARD_INFO info;
                    buffer.EnsureByteCapacity((ulong)Marshal.SizeOf<FILE_STANDARD_INFO>());

                    if (!Direct.GetFileInformationByHandleEx(fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileStandardInfo, buffer.DangerousGetHandle(), checked((uint)buffer.ByteCapacity)))
                        throw ErrorHelper.GetIoExceptionForLastError();

                    info = Marshal.PtrToStructure<FILE_STANDARD_INFO>(buffer.DangerousGetHandle());
                    return info;
                });
            }

            public static FileBasicInfo GetFileBasicInfoByHandle(SafeFileHandle fileHandle)
            {
                return StringBufferCache.CachedBufferInvoke((buffer) =>
                {
                    FILE_BASIC_INFO info;
                    buffer.EnsureByteCapacity((ulong)Marshal.SizeOf<FILE_BASIC_INFO>());

                    if (!Direct.GetFileInformationByHandleEx(fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileBasicInfo, buffer.DangerousGetHandle(), checked((uint)buffer.ByteCapacity)))
                        throw ErrorHelper.GetIoExceptionForLastError();

                    info = Marshal.PtrToStructure<FILE_BASIC_INFO>(buffer.DangerousGetHandle());
                    return new FileBasicInfo(info);
                });
            }

            public static IEnumerable<StreamInformation> GetStreamInformationByHandle(SafeFileHandle fileHandle)
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364406.aspx

                // typedef struct _FILE_STREAM_INFO
                // {
                //     DWORD NextEntryOffset;
                //     DWORD StreamNameLength;
                //     LARGE_INTEGER StreamSize;
                //     LARGE_INTEGER StreamAllocationSize;
                //     WCHAR StreamName[1];
                // } FILE_STREAM_INFO, *PFILE_STREAM_INFO;

                // We'll ensure we have at least 100 characters worth in the buffer to start
                return StringBufferCache.CachedBufferInvoke(100, (buffer) =>
                {
                    while (!Direct.GetFileInformationByHandleEx(fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileStreamInfo, buffer.DangerousGetHandle(), checked((uint)buffer.ByteCapacity)))
                    {
                        uint error = (uint)Marshal.GetLastWin32Error();
                        switch (error)
                        {
                            case WinErrors.ERROR_HANDLE_EOF:
                                // No streams
                                return Enumerable.Empty<StreamInformation>();
                            case WinErrors.ERROR_MORE_DATA:
                                buffer.EnsureByteCapacity(buffer.ByteCapacity * 2);
                                break;
                            default:
                                throw ErrorHelper.GetIoExceptionForError(error);
                        }
                    }

                    var infos = new List<StreamInformation>();
                    var reader = new NativeBufferReader(buffer);
                    uint offset = 0;

                    do
                    {
                        reader.ByteOffset = offset;
                        offset = reader.ReadUint();
                        uint nameLength = reader.ReadUint();
                        infos.Add(new StreamInformation
                        {
                            Size = reader.ReadUlong(),
                            AllocationSize = reader.ReadUlong(),
                            Name = reader.ReadString((int)(nameLength / 2))
                        });
                    } while (offset != 0);

                    return infos;
                });
            }
        }
    }
}
