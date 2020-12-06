using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace PclSample
{
    static class PclWrapper
    {

        [DllImport("PCL", EntryPoint = "view")]
        static extern void ViewShortFromDll(IntPtr x, IntPtr y, IntPtr z, IntPtr r, IntPtr g, IntPtr b, int count);

        [DllImport("PCL", EntryPoint = "match_XYZ")]
        static extern void MatchFromDll(IntPtr x1, IntPtr y1, IntPtr z1, IntPtr x2, IntPtr y2, IntPtr z2, out IntPtr x3, out IntPtr y3, out IntPtr z3,
            int count1, int count2, out int count3, out IntPtr ptrTrans, double magnification, int maximumIterations, float leafsize, bool view);

        [DllImport("PCL", EntryPoint = "match_XYZRGB")]
        static extern void MatchFromDllWithColor(IntPtr x1, IntPtr y1, IntPtr z1, IntPtr r1, IntPtr g1, IntPtr b1, 
            IntPtr x2, IntPtr y2, IntPtr z2, IntPtr r2, IntPtr g2, IntPtr b2, 
            out IntPtr x3, out IntPtr y3, out IntPtr z3, out IntPtr r3, out IntPtr g3, out IntPtr b3,
            int count1, int count2, out int count3, out IntPtr ptrTrans, double magnification, int maximumIterations, float leafsize, bool view);

        public static void ViewFromPng(string pointPath, string colorPath = null, int threshold = 10000)
        {
            var pm = Cv2.ImRead(pointPath, ImreadModes.Unchanged);
            var cm = new Mat(pm.Height, pm.Width, MatType.CV_8UC3, new Scalar(255, 255, 255));
            if(colorPath != null) cm = Cv2.ImRead(colorPath);
            var points = MatToPointCloudXyzBgr(pm, cm, 1, threshold);
            LaunchViewer(points.X, points.Y, points.Z, points.R, points.G, points.B, points.Count);
        }

        public static void ViewCustomImage(int a, int b, int c)
        {
            var w = 120;
            var h = 120;
            var count = w * h;
            var sx = new short[count];
            var sy = new short[count];
            var sz = new short[count];
            var sr = new byte[count];
            var sg = new byte[count];
            var sb = new byte[count];
            for (int i = 0; i < count / 3; i++)
            {
                sx[i] = (short)(a * i + b * 2 - c * i * i + 60);
                sy[i] = (short)(a * i + Math.Sqrt(b) * i + c * i / 2);
                sz[i] = (short)(-a * 4 / (i + 1) + b * i - c * i * i + i * 10);
                sr[i] = 0;
                sg[i] = 0;
                sb[i] = 255;
            }
            for (int i = count / 3; i < count * 2 / 3; i++)
            {
                sx[i] = (short)(-a * i * i - b * 2 / i + c * c / i + 60);
                sy[i] = (short)(Math.Sqrt(a) * i + c * i * i / 2 + i * i);
                sz[i] = (short)(a * 4 / i / i + b * b * i + c * i + 70);
                sr[i] = 0;
                sg[i] = 255;
                sb[i] = 0;
            }
            for (int i = count * 2 / 3; i < count; i++)
            {
                sx[i] = (short)(a * i * i + b - c * c - i);
                sy[i] = (short)(a * a * i + b * 5 / i + Math.Sqrt(c / 2) * i);
                sz[i] = (short)(-a * 4 / i + b * 2 * i - i * 90);
                sr[i] = 255;
                sg[i] = 0;
                sb[i] = 0;
            }
            LaunchViewer(sx, sy, sz, sr, sg, sb, count);
        }

        public static void MatchPoints(string pointSourcePath, string pointTargetPath, bool save, int maximumIterations, int interval1, int interval2, int threshold, float leafSize)
        {
            var source = Cv2.ImRead(pointSourcePath, ImreadModes.Unchanged);
            var target = Cv2.ImRead(pointTargetPath, ImreadModes.Unchanged);
            var results = Match(source, target, interval1, interval2, threshold, maximumIterations, leafSize);
            if (save)
            {
                var result = PointCloudArrayToMat(results.X, results.Y, results.Z, results.Count);
                Cv2.ImWrite($"D:\\PclDirectory\\RegistrationResult\\P.png", result);
            }
        }

        public static void MatchPointsWithColor(string colorSourcePath, string pointSourcePath, string colorTargetPath, string pointTargetPath, 
            bool save, int maximumIterations, int interval1, int interval2, int threshold, float leafSize)
        {
            var sourceC = Cv2.ImRead(colorSourcePath);
            var targetC = Cv2.ImRead(colorTargetPath);
            var sourceP = Cv2.ImRead(pointSourcePath, ImreadModes.Unchanged);
            var targetP = Cv2.ImRead(pointTargetPath, ImreadModes.Unchanged);
            var results = MatchWithColor(sourceP, sourceC, targetP, targetC, interval1, interval2, threshold, maximumIterations, leafSize);
            if (save)
            {
                var resultP = PointCloudArrayToMat(results.X, results.Y, results.Z, results.Count);
                var resultC = ColorArrayToMat(results.R, results.G, results.B, results.Count);
                Cv2.ImWrite($"D:\\PclDirectory\\RegistrationResult\\P.png", resultP);
                Cv2.ImWrite($"D:\\PclDirectory\\RegistrationResult\\C.png", resultC);
            }
        }

        private static void LaunchViewer(short[] sx, short[] sy, short[] sz, byte[] sr, byte[] sg, byte[] sb, int count)
        {

            IntPtr ptrx = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * count);
            IntPtr ptry = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * count);
            IntPtr ptrz = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * count);
            IntPtr ptrr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * count);
            IntPtr ptrg = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * count);
            IntPtr ptrb = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * count);
            Marshal.Copy(sx, 0, ptrx, count);
            Marshal.Copy(sy, 0, ptry, count);
            Marshal.Copy(sz, 0, ptrz, count);
            Marshal.Copy(sr, 0, ptrr, count);
            Marshal.Copy(sg, 0, ptrg, count);
            Marshal.Copy(sb, 0, ptrb, count);
            ViewShortFromDll(ptrx, ptry, ptrz, ptrr, ptrg, ptrb, count);
            Marshal.FreeCoTaskMem(ptrx);
            Marshal.FreeCoTaskMem(ptry);
            Marshal.FreeCoTaskMem(ptrz);
            Marshal.FreeCoTaskMem(ptrr);
            Marshal.FreeCoTaskMem(ptrg);
            Marshal.FreeCoTaskMem(ptrb);
        }

        private static (short[] X, short[] Y, short[] Z, int Count, Mat Transform) Match(Mat source, Mat target, int interval1, int interval2, int threshold, int maximumIterations, float leafSize)
        {
            var points1 = MatToPointCloudXyz(source, interval1, threshold);
            var points2 = MatToPointCloudXyz(target, interval2, threshold);
            IntPtr ptrx1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points1.Count);
            IntPtr ptry1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points1.Count);
            IntPtr ptrz1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points1.Count);
            IntPtr ptrx2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points2.Count);
            IntPtr ptry2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points2.Count);
            IntPtr ptrz2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points2.Count);
            Marshal.Copy(points1.X, 0, ptrx1, points1.Count);
            Marshal.Copy(points1.Y, 0, ptry1, points1.Count);
            Marshal.Copy(points1.Z, 0, ptrz1, points1.Count);
            Marshal.Copy(points2.X, 0, ptrx2, points2.Count);
            Marshal.Copy(points2.Y, 0, ptry2, points2.Count);
            Marshal.Copy(points2.Z, 0, ptrz2, points2.Count);
            MatchFromDll(ptrx1, ptry1, ptrz1, ptrx2, ptry2, ptrz2, out IntPtr ptrx3, out IntPtr ptry3, out IntPtr ptrz3,
                points2.Count, points2.Count, out int count3, out IntPtr ptrTrans, 1, maximumIterations, leafSize, true);
            var trans = new double[16];
            Marshal.Copy(ptrTrans, trans, 0, 16);
            var count = count3;
            var x = new short[count];
            var y = new short[count];
            var z = new short[count];
            Marshal.Copy(ptrx3, x, 0, count);
            Marshal.Copy(ptry3, y, 0, count);
            Marshal.Copy(ptrz3, z, 0, count);
            Marshal.FreeCoTaskMem(ptrx1);
            Marshal.FreeCoTaskMem(ptry1);
            Marshal.FreeCoTaskMem(ptrz1);
            Marshal.FreeCoTaskMem(ptrx2);
            Marshal.FreeCoTaskMem(ptry2);
            Marshal.FreeCoTaskMem(ptrz2);

            return (x, y, z, count, new Mat(4, 4, MatType.CV_64F, trans));

        }

        private static (short[] X, short[] Y, short[] Z, byte[] B, byte[] G, byte[] R, int Count, Mat Transform) MatchWithColor(Mat sourceP, Mat sourceC, Mat targetP, Mat targetC, int interval1, int interval2, int threshold, int maximumIterations, float leafSize)
        {
            
            var points1 = MatToPointCloudXyzBgr(sourceP, sourceC, interval1, threshold);
            var points2 = MatToPointCloudXyzBgr(targetP, targetC, interval2, threshold);

            TransformXZ(ref points1.X, ref points1.Y, ref points1.Z, points1.Count, new Point(0, 400), -38);

            IntPtr ptrx1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points1.Count);
            IntPtr ptry1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points1.Count);
            IntPtr ptrz1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points1.Count);
            IntPtr ptrb1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * points1.Count);
            IntPtr ptrg1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * points1.Count);
            IntPtr ptrr1 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * points1.Count);
            IntPtr ptrx2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points2.Count);
            IntPtr ptry2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points2.Count);
            IntPtr ptrz2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(short)) * points2.Count);
            IntPtr ptrb2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * points2.Count);
            IntPtr ptrg2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * points2.Count);
            IntPtr ptrr2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte)) * points2.Count);
            Marshal.Copy(points1.X, 0, ptrx1, points1.Count);
            Marshal.Copy(points1.Y, 0, ptry1, points1.Count);
            Marshal.Copy(points1.Z, 0, ptrz1, points1.Count);
            Marshal.Copy(points1.B, 0, ptrb1, points1.Count);
            Marshal.Copy(points1.G, 0, ptrg1, points1.Count);
            Marshal.Copy(points1.R, 0, ptrr1, points1.Count);
            Marshal.Copy(points2.X, 0, ptrx2, points2.Count);
            Marshal.Copy(points2.Y, 0, ptry2, points2.Count);
            Marshal.Copy(points2.Z, 0, ptrz2, points2.Count);
            Marshal.Copy(points2.B, 0, ptrb2, points2.Count);
            Marshal.Copy(points2.G, 0, ptrg2, points2.Count);
            Marshal.Copy(points2.R, 0, ptrr2, points2.Count);
            MatchFromDllWithColor(ptrx1, ptry1, ptrz1, ptrr1, ptrg1, ptrb1, ptrx2, ptry2, ptrz2, ptrr2, ptrg2, ptrb2,
                out IntPtr ptrx3, out IntPtr ptry3, out IntPtr ptrz3, out IntPtr ptrr3, out IntPtr ptrg3, out IntPtr ptrb3,
                points1.Count, points2.Count, out int count3, out IntPtr ptrTrans, 1, maximumIterations, leafSize, true);
            var trans = new double[16];
            Marshal.Copy(ptrTrans, trans, 0, 16);
            var count = count3;
            var x = new short[count];
            var y = new short[count];
            var z = new short[count];
            var b = new byte[count];
            var g = new byte[count];
            var r = new byte[count];
            Marshal.Copy(ptrx3, x, 0, count);
            Marshal.Copy(ptry3, y, 0, count);
            Marshal.Copy(ptrz3, z, 0, count);
            Marshal.Copy(ptrb3, b, 0, count);
            Marshal.Copy(ptrg3, g, 0, count);
            Marshal.Copy(ptrr3, r, 0, count);
            Marshal.FreeCoTaskMem(ptrx1);
            Marshal.FreeCoTaskMem(ptry1);
            Marshal.FreeCoTaskMem(ptrz1);
            Marshal.FreeCoTaskMem(ptrx2);
            Marshal.FreeCoTaskMem(ptry2);
            Marshal.FreeCoTaskMem(ptrz2);

            return (x, y, z, b, g, r, count, new Mat(4, 4, MatType.CV_64F, trans));

        }

        unsafe private static (short[] X, short[] Y, short[] Z, int Count) MatToPointCloudXyz(Mat pMat, int interval, int threshold)
        {
            var count = 0;
            var xList = new List<short>();
            var yList = new List<short>();
            var zList = new List<short>();
            short* sp = (short*)pMat.DataPointer;
            for (int h = 0; h < pMat.Height; h += interval)
            {
                for (int w = 0; w < pMat.Width; w += interval)
                {
                    var i = (h * pMat.Width + w) * 3;
                    if (sp[i + 2] > 30 && sp[i + 2] < threshold)
                    {
                        xList.Add(sp[i + 0]);
                        yList.Add(sp[i + 1]);
                        zList.Add(sp[i + 2]);
                        count++;
                    }
                }
            }
            return (xList.ToArray(), yList.ToArray(), zList.ToArray(), count);
        }

        unsafe private static (short[] X, short[] Y, short[] Z, byte[] B, byte[] G, byte[] R, int Count) MatToPointCloudXyzBgr(Mat pMat, Mat cMat, int interval, int threshold)
        {
            var count = 0;
            var xList = new List<short>();
            var yList = new List<short>();
            var zList = new List<short>();
            var bList = new List<byte>();
            var gList = new List<byte>();
            var rList = new List<byte>();
            short* sp = (short*)pMat.DataPointer;
            byte* ip = cMat.DataPointer;
            for (int h = 0; h < pMat.Height; h += interval)
            {
                for (int w = 0; w < pMat.Width; w += interval)
                {
                    var i = (h * pMat.Width + w) * 3;
                    if (sp[i + 2] > 30 && sp[i + 2] < threshold)
                    {
                        xList.Add(sp[i + 0]);
                        yList.Add(sp[i + 1]);
                        zList.Add(sp[i + 2]);
                        bList.Add(ip[i + 0]);
                        gList.Add(ip[i + 1]);
                        rList.Add(ip[i + 2]);
                        count++;
                    }
                }
            }
            return (xList.ToArray(), yList.ToArray(), zList.ToArray(), bList.ToArray(), gList.ToArray(), rList.ToArray(), count);
        }

        unsafe private static Mat PointCloudArrayToMat(short[] x, short[] y, short[] z, int count)
        {
            var img = new Mat(count, 1, MatType.CV_16UC3);
            var p = (short*)img.DataPointer;
            for(int i = 0; i < count; i++)
            {
                p[i * 3 + 0] = x[i];
                p[i * 3 + 1] = y[i];
                p[i * 3 + 2] = z[i];
            }
            return img;
        }

        unsafe private static Mat ColorArrayToMat(byte[] r, byte[] g, byte[] b, int count)
        {
            var img = new Mat(count, 1, MatType.CV_8UC3);
            var p = img.DataPointer;
            for (int i = 0; i < count; i++)
            {
                p[i * 3 + 0] = b[i];
                p[i * 3 + 1] = g[i];
                p[i * 3 + 2] = r[i];
            }
            return img;
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

        unsafe private static void TransformXZ(ref short[] x, ref short[] y, ref short[] z, int count, Point centerXZ, double thetaDeg)
        {
            for(int i = 0; i < count; i++)
            {
                x[i] = (short)(x[i] - centerXZ.X);
                z[i] = (short)(z[i] - centerXZ.Y);
            }

            var thetaRad = thetaDeg * Math.PI / 180;
            var lt = Math.Cos(thetaRad);
            var ct = 0.0;
            var rt = -Math.Sin(thetaRad);
            var lb = Math.Sin(thetaRad);
            var cb = 0.0;
            var rb = Math.Cos(thetaRad);

            var tmpx = new short[count];
            var tmpy = new short[count];
            var tmpz = new short[count];
            for (int i = 0; i < count; i++)
            {
                var xn = i * 3 + 0;
                var yn = i * 3 + 1;
                var zn = i * 3 + 2;
                tmpx[i] = (short)(x[i] * lt + y[i] * ct + z[i] * rt);
                tmpz[i] = (short)(x[i] * lb + y[i] * cb + z[i] * rb);
            }

            for (int i = 0; i < count; i++)
            {
                x[i] = (short)(tmpx[i] + centerXZ.X);
                z[i] = (short)(tmpz[i] + centerXZ.Y);
            }
        }
    }
}
