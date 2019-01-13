﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EmguCVSandbox
{
    class ScreenShot
    {
        public static Bitmap RectangleScreenshot(Rectangle rect)
        {
            //int width = rect.right - rect.left;
            //int height = rect.bottom - rect.top;

            int width = rect.Width;
            int height = rect.Height;

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(rect.Location.X, rect.Location.Y, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);

            return bmp;
        }

        public static Bitmap GetScreenShop(string procName)
        {
            var proc = Process.GetProcessesByName(procName)[0];
            var rect = new User32.Rect();
            User32.GetWindowRect(proc.MainWindowHandle, ref rect);

            //int width = rect.right - rect.left;
            //int height = rect.bottom - rect.top;

            int width = 1680;
            int height = 1050;

            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.CopyFromScreen(rect.left, rect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);

            return bmp;
        }

        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct Rect
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
        }
    }
}
