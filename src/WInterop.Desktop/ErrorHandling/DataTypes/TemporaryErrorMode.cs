﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.ErrorHandling.DataTypes
{
    /// <summary>
    /// Simple helper class to temporarily enable thread error flag modes if necessary.
    /// </summary>
    public class TemporaryErrorMode : IDisposable
    {
        bool _restoreOldMode;
        ErrorMode _oldMode;

        private TemporaryErrorMode(ErrorMode modesToEnable)
        {
            _oldMode = ErrorDesktopMethods.GetThreadErrorMode();
            if ((_oldMode & modesToEnable) != modesToEnable)
            {
                _oldMode = ErrorDesktopMethods.SetThreadErrorMode(_oldMode | modesToEnable);
                _restoreOldMode = true;
            }
        }

        /// <summary>
        /// Set the given error mode flags if needed. Use in using statement to clear flags when done.
        /// </summary>
        public static IDisposable EnableMode(ErrorMode modesToEnable)
        {
            return new TemporaryErrorMode(modesToEnable);
        }

        public void Dispose()
        {
            if (_restoreOldMode)
            {
                _restoreOldMode = false;
                ErrorDesktopMethods.SetThreadErrorMode(_oldMode);
            }
        }
    }
}
