using System;
using System.IO.Compression;
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
                    PclWrapper.ViewFromPng($"{fbd.SelectedPath}\\P.png", $"{fbd.SelectedPath}\\C.png");
                }
                else
                {
                    PclWrapper.ViewFromPng($"{fbd.SelectedPath}\\P.png");
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
    }
}
