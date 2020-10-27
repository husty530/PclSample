using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenCvSharp;

namespace PclSample
{
    static class PclWrapper
    {

        [DllImport("PCL", EntryPoint = "view")]
        static extern void ViewShortFromDll(IntPtr x, IntPtr y, IntPtr z, IntPtr r, IntPtr g, IntPtr b, int width, int height);

        public static void ViewFromPng(string pointPath, string colorPath = null)
        {

            var pm = Cv2.ImRead(pointPath, ImreadModes.Unchanged);
            var cm = new Mat(pm.Height, pm.Width, MatType.CV_8UC3, new Scalar(255, 255, 255));
            if(colorPath != null) cm = Cv2.ImRead(colorPath);
            int len = pm.Width * pm.Height;
            
            var sx = new short[len];
            var sy = new short[len];
            var sz = new short[len];
            var sr = new byte[len];
            var sg = new byte[len];
            var sb = new byte[len];
            unsafe
            {
                short* sp = (short*)pm.DataPointer;
                for (int i = 0; i < len; i++)
                {
                    sx[i] = sp[i * 3 + 0];
                    sy[i] = sp[i * 3 + 1];
                    sz[i] = sp[i * 3 + 2];
                }
                byte* ip = cm.DataPointer;
                for (int i = 0; i < len; i++)
                {
                    sr[i] = ip[i * 3 + 2];
                    sg[i] = ip[i * 3 + 1];
                    sb[i] = ip[i * 3 + 0];
                }
            }
            LaunchViewer(sx, sy, sz, sr, sg, sb, pm.Width, pm.Height);

        }

        public static void ViewCustomImage(int a, int b, int c)
        {
            var w = 120;
            var h = 120;
            var len = w * h;
            var sx = new short[len];
            var sy = new short[len];
            var sz = new short[len];
            var sr = new byte[len];
            var sg = new byte[len];
            var sb = new byte[len];
            for (int i = 0; i < len / 3; i++)
            {
                sx[i] = (short)(a * i + b * 2 - c * i * i + 60);
                sy[i] = (short)(a * i + Math.Sqrt(b) * i + c * i / 2);
                sz[i] = (short)(-a * 4 / (i + 1) + b * i - c * i * i + i * 10);
                sr[i] = 0;
                sg[i] = 0;
                sb[i] = 255;
            }
            for (int i = len / 3; i < len * 2 / 3; i++)
            {
                sx[i] = (short)(-a * i * i - b * 2 / i + c * c / i + 60);
                sy[i] = (short)(Math.Sqrt(a) * i + c * i * i / 2 + i * i);
                sz[i] = (short)(a * 4 / i / i + b * b * i + c * i + 70);
                sr[i] = 0;
                sg[i] = 255;
                sb[i] = 0;
            }
            for (int i = len * 2 / 3; i < len; i++)
            {
                sx[i] = (short)(a * i * i + b - c * c - i);
                sy[i] = (short)(a * a * i + b * 5 / i + Math.Sqrt(c / 2) * i);
                sz[i] = (short)(-a * 4 / i + b * 2 * i - i * 90);
                sr[i] = 255;
                sg[i] = 0;
                sb[i] = 0;
            }
            LaunchViewer(sx, sy, sz, sr, sg, sb, w, h);
        }

        private static void LaunchViewer(short[] sx, short[] sy, short[] sz, byte[] sr, byte[] sg, byte[] sb, int width, int height)
        {

            int len = width * height;
            IntPtr ptrx = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * len);
            IntPtr ptry = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * len);
            IntPtr ptrz = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * len);
            IntPtr ptrr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * len);
            IntPtr ptrg = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * len);
            IntPtr ptrb = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * len);
            Marshal.Copy(sx, 0, ptrx, len);
            Marshal.Copy(sy, 0, ptry, len);
            Marshal.Copy(sz, 0, ptrz, len);
            Marshal.Copy(sr, 0, ptrr, len);
            Marshal.Copy(sg, 0, ptrg, len);
            Marshal.Copy(sb, 0, ptrb, len);
            ViewShortFromDll(ptrx, ptry, ptrz, ptrr, ptrg, ptrb, width, height);
            Marshal.FreeCoTaskMem(ptrx);
            Marshal.FreeCoTaskMem(ptry);
            Marshal.FreeCoTaskMem(ptrz);
            Marshal.FreeCoTaskMem(ptrr);
            Marshal.FreeCoTaskMem(ptrg);
            Marshal.FreeCoTaskMem(ptrb);

        }
    }
}
