﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Support;
using WInterop.DiskManagement;
using WInterop.FileManagement;
using Xunit;

namespace Tests.DiskManagementTests
{
    public class Methods
    {
        [Fact]
        public void GetDiskFreeSpaceForCurrentDrive()
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                var freeSpace = DiskMethods.GetDiskFreeSpace(null);
                freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfBytes);
                freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfFreeBytes);
            });
        }

        [Fact]
        public void GetDiskFreeSpaceForTempDirectory()
        {
            string tempPath = FileMethods.GetTempPath();
            var freeSpace = DiskMethods.GetDiskFreeSpace(tempPath);
            freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfBytes);
            freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfFreeBytes);
        }
    }
}
