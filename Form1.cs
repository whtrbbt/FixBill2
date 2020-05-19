using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows.Forms;

namespace FixBill2
{
    public partial class Form1 : Form
    {
        string INDir;
        string OUTDir;
        
        public Form1()
        {
            InitializeComponent();
            inDirTextBox.Text = Properties.Settings.Default.INDIR;
            outDirTextBox.Text = Properties.Settings.Default.OUTDIR;
            deleteReasonCheckBox.Checked = Properties.Settings.Default.DELETE_REASON;
            replaceSignCheckBox.Checked = Properties.Settings.Default.REPLACE_SIGN;
            unzipCheckBox.Checked = Properties.Settings.Default.UNZIP;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SelectInDirButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                INDir = dialog.FileName;
                inDirTextBox.Text = INDir;                
                Properties.Settings.Default.INDIR = INDir; // сохраняем путь в настройках программы
                Properties.Settings.Default.Save();
            }
        }

        private void SelectOutDirButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                OUTDir = dialog.FileName;
                outDirTextBox.Text = OUTDir;
                Properties.Settings.Default.OUTDIR = OUTDir; // сохраняем путь в настройках программы
                Properties.Settings.Default.Save();
            }
        }

        private void DeleteReasonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DELETE_REASON = deleteReasonCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void ReplaceSignCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.REPLACE_SIGN = replaceSignCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void UnzipCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.UNZIP = unzipCheckBox.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
