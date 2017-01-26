namespace FtcReportControls.WindowsSystem.VistaTaskDialog
{
    using System;
    using System.Runtime.InteropServices;

    internal static partial class Native
    {
        public const uint GENERIC_ALL = 0x10000000;
        public const uint DESKTOP_SWITCHDESKTOP = 0x100;
        public const uint SPI_GETSNAPTODEFBUTTON = 95;

        [DllImport("user32.dll", SetLastError=true)]
        public static extern IntPtr GetThreadDesktop(uint dwThreadId);

        [DllImport("user32.dll", SetLastError=true)]
        public static extern IntPtr OpenInputDesktop(uint dwFlags, bool fInherit, uint dwDesiredAccess);

        [DllImport("user32.dll", EntryPoint="CreateDesktop", CharSet=CharSet.Unicode, SetLastError=true)]
        public static extern IntPtr CreateDesktop(
            [MarshalAs(UnmanagedType.LPWStr)] string desktop,
            [MarshalAs(UnmanagedType.LPWStr)] string device,	// must be null 
            [MarshalAs(UnmanagedType.LPWStr)] string devmode, // must be null
            uint flags, // use 0
            uint desiredAccess,
            IntPtr lpsa
        );

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetThreadDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SwitchDesktop(IntPtr hDesktop);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseDesktop(IntPtr hDesktop);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref uint pvParam, uint fWinIni);
    }
}
