﻿using Device.Net.Windows;
using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace Device.Net
{
    public static class APICalls
    {
        #region Constants
        public const int DigcfDeviceinterface = 16;
        public const int DigcfPresent = 2;
        public const uint FileShareRead = 1;
        public const uint FileShareWrite = 2;
        public const uint GenericRead = 2147483648;
        public const uint GenericWrite = 1073741824;
        public const uint OpenExisting = 3;
        public const int FileAttributeNormal = 128;
        public const int FileFlagOverlapped = 1073741824;

        public const int ERROR_NO_MORE_ITEMS = 259;
        #endregion

        #region Methods

        #region Kernel32
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        //[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        //[SuppressUnmanagedCodeSecurity]
        //[return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(SafeFileHandle hObject);

        // Used to read bytes from the serial connection. 
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadFile(SafeFileHandle hFile, byte[] lpBuffer, int nNumberOfBytesToRead, out int lpNumberOfBytesRead, int lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteFile(SafeFileHandle hFile, byte[] lpBuffer, uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten, int lpOverlapped);
        #endregion

        #region SetupAPI
        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdienumdeviceinterfaces
        /// </summary>
        [DllImport(@"setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, IntPtr devInfo, ref Guid interfaceClassGuid, uint memberIndex, ref SpDeviceInterfaceData deviceInterfaceData);

        [DllImport("setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid classGuid, IntPtr enumerator, IntPtr hwndParent, uint flags);

        [DllImport(@"setupapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr hDevInfo, ref SpDeviceInterfaceData deviceInterfaceData, ref SpDeviceInterfaceDetailData deviceInterfaceDetailData, uint deviceInterfaceDetailDataSize, out uint requiredSize, ref SpDeviceInfoData deviceInfoData);
        #endregion

        #endregion
    }
}
