using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace FixBill2
{
    public partial class mainWindow : Form
    {
        string INDir = @Properties.Settings.Default.INDIR;
        string OUTDir = @Properties.Settings.Default.OUTDIR;
 
        public mainWindow()
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

        static void FixDir(string inDir, string outDir)
        {
            var dirIN = new DirectoryInfo(@inDir); //папка с входящими файлами 
            var dirOUT = new DirectoryInfo(@outDir); //папка с исходящими файлами  
            string dirName = "";


            foreach (DirectoryInfo dir in dirIN.GetDirectories()) // ищем все подкаталоги в каталоге dirIN
            {
                dirName = Path.GetFileName(dir.FullName); // получаем имя текущего подкаталога
                dirName = dirOUT + @"\" + dirName;
                if (!Directory.Exists(dirName))
                    Directory.CreateDirectory(dirName);
                FixDir(dir.FullName, dirName);

            }
            FixFiles(dirIN.FullName, dirOUT.FullName);
        }

        static void FixFiles(string inDir, string outDir)
        {

            var dirIN = new DirectoryInfo(@inDir); // папка с входящими файлами 
            var dirOUT = new DirectoryInfo(@outDir); // папка с исходящими файлами 

            //int quantityOfFile = dirIN.GetFiles().Length; // получаем количество файлов в папке для ProgressBar

            DirectoryInfo tmpDir; // временная папка для создания архива
            DirectoryInfo tmpUnZipDir; //временная папка для распаковки архива
            string fileName = "";
            string outFileName = "";



            foreach (FileInfo file in dirIN.GetFiles())
            {
                fileName = Path.GetFileName(file.FullName);
                if (Path.GetExtension(fileName) == ".zip")
                {
                    tmpDir = CreateTempDir();
                    tmpUnZipDir = UnzipFileToTempDir(file.FullName);
                    FixDir(tmpUnZipDir.FullName, tmpDir.FullName);
                    outFileName = @outDir + @"\" + fileName;
                    DeleteExistFile(outFileName);
                    ZipFile.CreateFromDirectory(tmpDir.FullName, outFileName);
                    tmpDir.Delete(true);
                    tmpUnZipDir.Delete(true);
                }
                else //Path.GetExtension(fileName) <> ".zip"
                {
                    fileName = RemoveInvalidFilePathCharacters(fileName, "");
                    outFileName = @outDir + @"\" + fileName;
                    DeleteExistFile(outFileName);
                    FixBill(@file.FullName, outFileName);
                }
            }
        }
        public static void DeleteExistFile(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }

        public static DirectoryInfo CreateTempDir()
        // Создает временную папку и возращает ссылку на нее
        {
            DirectoryInfo tempDir;
            tempDir = Directory.CreateDirectory(Path.GetTempPath() + Path.GetRandomFileName());
            return tempDir;
        }

        public static DirectoryInfo UnzipFileToTempDir(string fileName)
        // Распаковывает архив во временную папку и возвращает ссылку на неё.      
        {
            DirectoryInfo tempDir;
            tempDir = CreateTempDir();
            ZipFile.ExtractToDirectory(fileName, @tempDir.FullName);
            return tempDir;
        }

        public static string RemoveInvalidFilePathCharacters(string filename, string replaceChar)
        // Удаляет запрещенные символы в именах файлов      
        {
            return Regex.Replace(filename, "[\\[\\]]+", replaceChar, RegexOptions.Compiled);
        }


        public static void FixBill(string inFileName, string outFileName = "")
        {
            // Объявляем приложение
            Excel.Application exc = new Microsoft.Office.Interop.Excel.Application();

            Excel.XlReferenceStyle RefStyle = exc.ReferenceStyle;

            Excel.Workbook wb = null;

            try
            {
                wb = exc.Workbooks.Add(inFileName); // !!! 
            }
            catch (System.Exception ex)
            {
                throw new Exception("Не удалось загрузить файл! " + inFileName + "\n" + ex.Message);
            }
            //Console.WriteLine("Файл найден, начинаю работу. Это может занять несколько минут.");
            //Excel.Sheets excelsheets;


            if (Properties.Settings.Default.DELETE_REASON)
                FixReason(wb);
            if (Properties.Settings.Default.REPLACE_SIGN)
                FixManager(wb, "Заместитель генерального директора\nРоманова Ю.В.\nпо доверенности № 17 от 27.03.2020");

            if (outFileName != "")
                wb.SaveAs(outFileName);
            else
                wb.SaveAs(inFileName);
            exc.Quit();

        }

        public static void FixReason(Excel.Workbook wb, string reason = "")
        {
            Excel.Worksheet wsh = wb.Worksheets.get_Item(1) as Excel.Worksheet;

            Excel.Range excelcells;

            excelcells = wsh.get_Range("C19", "C19");
            excelcells.Value2 = reason;
        }

        public static void FixManager(Excel.Workbook wb, string manager = "")
        {
            Excel.Worksheet wsh = wb.Worksheets.get_Item(1) as Excel.Worksheet;

            Excel.Range excelcells;
            Excel.Range currentFind = null;
            //Excel.Range firstFind = null;

            excelcells = wsh.get_Range("C30");
            currentFind = excelcells.Find("Руководитель", Type.Missing,
          Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
          Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext);
            excelcells = currentFind.EntireRow;
            excelcells = excelcells.Cells[1, 3];
            excelcells.Value2 = manager;
            excelcells.EntireRow.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            excelcells.EntireRow.VerticalAlignment = Excel.XlVAlign.xlVAlignBottom;
            excelcells.EntireRow.RowHeight = 60;
        }

        private async void StartFixButton_Click(object sender, EventArgs e)
        {
            ProgressBar progressBar = progressBar1;

            tableLayoutPanel1.Enabled = false;

            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;
            try
            {
                await Task.Run( () => FixDir(Properties.Settings.Default.INDIR, Properties.Settings.Default.OUTDIR));
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                MessageBox.Show("Все файлы обработаны.", "Готово!");
            }
            
   
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.MarqueeAnimationSpeed = 0;
            progressBar.Visible = false;
            tableLayoutPanel1.Enabled = true;
        }
    }
}
