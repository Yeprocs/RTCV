using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace RTCV.UI
{
    public static class HandCursor
    {
        //why does winforms implement its own hand cursor? why??
        //https://superuser.com/a/1501044
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);
        private static Cursor LoadCustomCursor(string path)
        {
            IntPtr hCurs = LoadCursorFromFile(path);
            if (hCurs == IntPtr.Zero) throw new Win32Exception();
            var curs = new Cursor(hCurs);
            // Note: force the cursor to own the handle so it gets released properly
            var fi = typeof(Cursor).GetField("ownHandle", BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(curs, true);
            return curs;
        }

        /// <summary>
        /// Attempts to load the current pointing hand cursor from the registry.
        /// </summary>
        /// <returns>
        /// The hand cursor from the current cursor theme, or the default WinForms hand cursor if that fails for any reason.
        /// </returns>
        public static Cursor Get()
        {
            try
            {
                string path = Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Cursors", "Hand", null).ToString();
                return LoadCustomCursor(path);
            }
            catch (Exception)
            {
                return Cursors.Hand;
            }
        }
    }
}