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
                if (ColorModeButton.Checked)
                {
                    PclWrapper.ViewFromPng($"{fbd.SelectedPath}\\P.png", $"{fbd.SelectedPath}\\C.png", int.Parse(ThreshTx.Text));
                }
                else if (GrayModeButton.Checked)
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
            string source = "";
            string target = "";
            var fbd1 = new FolderBrowserDialog();
            fbd1.Description = "フォルダ選択";
            if (fbd1.ShowDialog() == DialogResult.OK)
            {
                source = $"{fbd1.SelectedPath}\\P.png";
                var fbd2 = new FolderBrowserDialog();
                fbd2.Description = "フォルダ選択";
                if (fbd2.ShowDialog() == DialogResult.OK)
                {
                    target = $"{fbd2.SelectedPath}\\P.png";
                    var save = (SaveCheckBox.Checked) ? true : false;
                    var threshold = (int.Parse(ThreshTx.Text) > 100) ? int.Parse(ThreshTx.Text) : 100;
                    var iterations = (int.Parse(IterationsTx.Text) > 0) ? int.Parse(IterationsTx.Text) : 0;
                    var interval = (int.Parse(IntervalTx.Text) > 0) ? int.Parse(IntervalTx.Text) : 1;
                    var leafSize = (float.Parse(LeafSizeTx.Text) > 0) ? float.Parse(LeafSizeTx.Text) : 1;
                    PclWrapper.MatchPoints(source, target, save, iterations, interval, threshold, leafSize);
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
                var resultPath = $"D:\\PclDirectory\\RegistrationResult\\P.png";
                var files = Directory.GetFiles(fbd.SelectedPath, "*P.png", SearchOption.AllDirectories);
                var first = true;
                if (!save) return;
                for(int i = 0; i < files.Length; i++)
                {
                    if (first)
                    {
                        PclWrapper.MatchPoints(files[0], files[1], true, iterations, interval, threshold, leafSize);
                        first = false;
                    }
                    else
                    {
                        PclWrapper.MatchPoints(files[i], resultPath, true, iterations, interval, threshold, leafSize);
                    }
                }
            }
        }
    }
}
