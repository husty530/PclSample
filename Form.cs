using System;
using System.IO;
using System.Windows.Forms;

namespace PclSample
{
    public partial class Form : System.Windows.Forms.Form
    {


        public Form()
        {
            InitializeComponent();
        }

        private void OpenButton1_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.Description = "フォルダ選択";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (ColorCheck.Checked)
                {
                    PclWrapper.ViewFromPng($"{fbd.SelectedPath}\\P.png", $"{fbd.SelectedPath}\\C.png", int.Parse(ThreshTx.Text));
                }
                else
                {
                    PclWrapper.ViewFromPng($"{fbd.SelectedPath}\\P.png", null, int.Parse(ThreshTx.Text));
                }
            }
        }

        private void OpenButton2_Click(object sender, EventArgs e)
        {
            var a = SliderA.Value;
            var b = SliderB.Value;
            var c = SliderC.Value;
            PclWrapper.ViewCustomImage(a, b, c);
        }

        private void OpenButton3_Click(object sender, EventArgs e)
        {
            var fbd1 = new FolderBrowserDialog();
            fbd1.Description = "フォルダ選択";
            if (fbd1.ShowDialog() == DialogResult.OK)
            {
                var sourceP = $"{fbd1.SelectedPath}\\P.png";
                var sourceC = $"{fbd1.SelectedPath}\\C.png";
                var fbd2 = new FolderBrowserDialog();
                fbd2.Description = "フォルダ選択";
                if (fbd2.ShowDialog() == DialogResult.OK)
                {
                    var targetP = $"{fbd2.SelectedPath}\\P.png";
                    var targetC = $"{fbd2.SelectedPath}\\C.png";
                    var save = (SaveCheckBox.Checked) ? true : false;
                    var threshold = (int.Parse(ThreshTx.Text) > 100) ? int.Parse(ThreshTx.Text) : 100;
                    var iterations = (int.Parse(IterationsTx.Text) > 0) ? int.Parse(IterationsTx.Text) : 0;
                    var interval = (int.Parse(IntervalTx.Text) > 0) ? int.Parse(IntervalTx.Text) : 1;
                    var leafSize = (float.Parse(LeafSizeTx.Text) > 0) ? float.Parse(LeafSizeTx.Text) : 1;
                    if (!ColorCheck.Checked)
                    {
                        PclWrapper.MatchPoints(sourceP, targetP, save, iterations, interval, interval, threshold, leafSize);
                    }
                    else
                    {
                        PclWrapper.MatchPointsWithColor(sourceC, sourceP, targetC, targetP, save, iterations, interval, interval, threshold, leafSize);
                    }
                }
            }
        }

        private void AutoRegistrationButton_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.Description = "フォルダ選択";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                var save = (SaveCheckBox.Checked) ? true : false;
                var threshold = (int.Parse(ThreshTx.Text) > 100) ? int.Parse(ThreshTx.Text) : 100;
                var iterations = (int.Parse(IterationsTx.Text) > 0) ? int.Parse(IterationsTx.Text) : 0;
                var interval = (int.Parse(IntervalTx.Text) > 0) ? int.Parse(IntervalTx.Text) : 1;
                var leafSize = (float.Parse(LeafSizeTx.Text) > 0) ? float.Parse(LeafSizeTx.Text) : 1;
                var resultPathP = $"D:\\PclDirectory\\RegistrationResult\\P.png";
                var resultPathC = $"D:\\PclDirectory\\RegistrationResult\\C.png";
                var filesP = Directory.GetFiles(fbd.SelectedPath, "*P.png", SearchOption.AllDirectories);
                var filesC = Directory.GetFiles(fbd.SelectedPath, "*C.png", SearchOption.AllDirectories);
                var first = true;
                if (!save) return;
                for(int i = 0; i < filesP.Length; i++)
                {
                    if (first)
                    {
                        if (!ColorCheck.Checked)
                        {
                            PclWrapper.MatchPoints(filesP[0], filesP[1], true, iterations, interval, interval, threshold, leafSize);
                        }
                        else
                        {
                            PclWrapper.MatchPointsWithColor(filesC[0], filesP[0], filesC[1], filesP[1], save, iterations, interval, interval, threshold, leafSize);
                        }
                        first = false;
                    }
                    else
                    {
                        if (!ColorCheck.Checked)
                        {
                            PclWrapper.MatchPoints(filesP[i], resultPathP, true, iterations, interval, 1, threshold, leafSize);
                        }
                        else
                        {
                            PclWrapper.MatchPointsWithColor(filesC[i], filesP[i], resultPathC, resultPathP, save, iterations, interval, 1, threshold, leafSize);
                        }
                    }
                }
            }
        }
    }
}
