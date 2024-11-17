using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DawnWallpaper
{
    internal class HotKey
    {
        public const int HOTKEY_ID_LEFT = 1;
        public const int HOTKEY_ID_RIGHT = 2;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [Flags]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }


        public static void KeySet(nint Handle)
        {
            bool successLeft = RegisterHotKey(Handle, HOTKEY_ID_LEFT, (uint)(KeyModifiers.Control | KeyModifiers.Shift), (uint)Keys.Left);
            bool successRight = RegisterHotKey(Handle, HOTKEY_ID_RIGHT, (uint)(KeyModifiers.Control | KeyModifiers.Shift), (uint)Keys.Right);
        }

        public static void KeyOut(nint Handle)
        {
            UnregisterHotKey(Handle, HOTKEY_ID_LEFT);
            UnregisterHotKey(Handle, HOTKEY_ID_RIGHT);
        }
    }
}
