﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace PclSample
{
    static class PclWrapper
    {

        [DllImport("PCL", EntryPoint = "view")]
        static extern void ViewShortFromDll(IntPtr x, IntPtr y, IntPtr z, IntPtr r, IntPtr g, IntPtr b, int count);

        [DllImport("PCL", EntryPoint = "match")]
        static extern void MatchFromDll(IntPtr x1, IntPtr y1, IntPtr z1, IntPtr x2, IntPtr y2, IntPtr z2, int count1, int count2, out IntPtr ptrTrans, int maximumIterations);


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
            LaunchViewer(sx, sy, sz, sr, sg, sb, pm.Width * pm.Height);

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
            LaunchViewer(sx, sy, sz, sr, sg, sb, len);
        }

        public static void ViewMatching(string pointSourcePath, string pointTargetPath, bool save, int maximumIterations, int threshold)
        {
            var source = Cv2.ImRead(pointSourcePath, ImreadModes.Unchanged);
            var target = Cv2.ImRead(pointTargetPath, ImreadModes.Unchanged);

            var count1 = 0;
            var count2 = 0;
            var x1List = new List<short>();
            var y1List = new List<short>();
            var z1List = new List<short>();
            var x2List = new List<short>();
            var y2List = new List<short>();
            var z2List = new List<short>();
            unsafe
            {
                short* sp = (short*)source.DataPointer;
                for (int i = 0; i < source.Width * source.Height; i++)
                {
                    if(sp[i * 3 + 2] > 30 && sp[i * 3 + 2] < threshold)
                    {
                        x1List.Add(sp[i * 3 + 0]);
                        y1List.Add(sp[i * 3 + 1]);
                        z1List.Add(sp[i * 3 + 2]);
                        count1++;
                    }
                }
                short* tp = (short*)target.DataPointer;
                for (int i = 0; i < target.Width * target.Height; i++)
                {
                    if (tp[i * 3 + 2] > 30 && tp[i * 3 + 2] < threshold)
                    {
                        x2List.Add(tp[i * 3 + 0]);
                        y2List.Add(tp[i * 3 + 1]);
                        z2List.Add(tp[i * 3 + 2]);
                        count2++;
                    }
                }
            }
            var x1 = x1List.ToArray();
            var y1 = y1List.ToArray();
            var z1 = z1List.ToArray();
            var x2 = x2List.ToArray();
            var y2 = y2List.ToArray();
            var z2 = z2List.ToArray();
            var transMat = MatchPoints(x1, y1, z1, x2, y2, z2, count1, count2, maximumIterations);
            if (save)
            {
                var result = Transform3D(source, transMat);
                Cv2.ImWrite($"D:\\pairs\\Result\\P.png", result);
            }
        }

        private static void LaunchViewer(short[] sx, short[] sy, short[] sz, byte[] sr, byte[] sg, byte[] sb, int len)
        {

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
            ViewShortFromDll(ptrx, ptry, ptrz, ptrr, ptrg, ptrb, len);
            Marshal.FreeCoTaskMem(ptrx);
            Marshal.FreeCoTaskMem(ptry);
            Marshal.FreeCoTaskMem(ptrz);
            Marshal.FreeCoTaskMem(ptrr);
            Marshal.FreeCoTaskMem(ptrg);
            Marshal.FreeCoTaskMem(ptrb);

        }

        private static Mat MatchPoints(short[] x1, short[] y1, short[] z1, short[] x2, short[] y2, short[] z2, int count1, int count2, int maximumIterations)
        {

            IntPtr ptrx1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * count1);
            IntPtr ptry1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * count1);
            IntPtr ptrz1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * count1);
            IntPtr ptrx2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * count2);
            IntPtr ptry2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * count2);
            IntPtr ptrz2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * count2);
            IntPtr ptrTrans = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(double)) * 16);
            Marshal.Copy(x1, 0, ptrx1, count1);
            Marshal.Copy(y1, 0, ptry1, count1);
            Marshal.Copy(z1, 0, ptrz1, count1);
            Marshal.Copy(x2, 0, ptrx2, count2);
            Marshal.Copy(y2, 0, ptry2, count2);
            Marshal.Copy(z2, 0, ptrz2, count2);
            MatchFromDll(ptrx1, ptry1, ptrz1, ptrx2, ptry2, ptrz2, count1, count2, out ptrTrans, maximumIterations);
            var trans = new double[16];
            Marshal.Copy(ptrTrans, trans, 0, 16);
            Marshal.FreeCoTaskMem(ptrx1);
            Marshal.FreeCoTaskMem(ptry1);
            Marshal.FreeCoTaskMem(ptrz1);
            Marshal.FreeCoTaskMem(ptrx2);
            Marshal.FreeCoTaskMem(ptry2);
            Marshal.FreeCoTaskMem(ptrz2);

            return new Mat(4, 4, MatType.CV_64F, trans);

        }

        private static Mat Transform3D(Mat img, Mat transMat)
        {
            var lt = transMat.At<double>(0, 0);
            var ct = transMat.At<double>(0, 1);
            var rt = transMat.At<double>(0, 2);
            var lc = transMat.At<double>(1, 0);
            var cc = transMat.At<double>(1, 1);
            var rc = transMat.At<double>(1, 2);
            var lb = transMat.At<double>(2, 0);
            var cb = transMat.At<double>(2, 1);
            var rb = transMat.At<double>(2, 2);
            var tx = transMat.At<double>(0, 3);
            var ty = transMat.At<double>(1, 3);
            var tz = transMat.At<double>(2, 3);

            var result = new Mat(img.Height, img.Width, MatType.CV_16UC3);
            unsafe
            {
                var p = (short*)img.DataPointer;
                var r = (ushort*)result.DataPointer;
                for(int i = 0; i < img.Width * img.Height; i++)
                {
                    var xn = i * 3 + 0;
                    var yn = i * 3 + 1;
                    var zn = i * 3 + 2;
                    r[xn] = (ushort)(p[xn] * lt + p[yn] * ct + p[zn] * rt + tx * 100);
                    r[yn] = (ushort)(p[xn] * lc + p[yn] * cc + p[zn] * rc + ty * 100);
                    r[zn] = (ushort)(p[xn] * lb + p[yn] * cb + p[zn] * rb + tz * 100);
                }

            }
            return result;
        }
    }
}
