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
            PayWithDebtCheckBox.Checked = Properties.Settings.Default.DELETE_PAYWITHDEBT;
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

        private void PayWithDebtCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DELETE_PAYWITHDEBT = PayWithDebtCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        static void FixDir(ref Excel.Application exApp, string inDir, string outDir)
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
                FixDir(ref exApp, dir.FullName, dirName);

            }
            FixFiles(ref exApp, dirIN.FullName, dirOUT.FullName);
        }

        static void FixFiles(ref Excel.Application exApp, string inDir, string outDir)
        {

            var dirIN = new DirectoryInfo(@inDir); // папка с входящими файлами 
            var dirOUT = new DirectoryInfo(@outDir); // папка с исходящими файлами             

            DirectoryInfo tmpDir; // временная папка для создания архива
            DirectoryInfo tmpUnZipDir; //временная папка для распаковки архива
            string fileName = "";
            string outFileName = "";
            string dirName;


            foreach (FileInfo file in dirIN.GetFiles())
            {
                fileName = Path.GetFileName(file.FullName);
                if (Path.GetExtension(fileName) == ".zip")
                {
                    if (Properties.Settings.Default.UNZIP)
                    {                        
                        tmpUnZipDir = UnzipFileToTempDir(file.FullName);
                        dirName = Path.GetFileNameWithoutExtension(file.FullName); // получаем имя папки из имени файла архива
                        dirName = dirOUT + @"\" + dirName;
                        if (!Directory.Exists(dirName))
                            Directory.CreateDirectory(dirName);                        
                        FixDir(ref exApp, tmpUnZipDir.FullName, dirName);
                        tmpUnZipDir.Delete(true);
                    }
                    else
                    {
                        tmpDir = CreateTempDir();
                        tmpUnZipDir = UnzipFileToTempDir(file.FullName);
                        FixDir(ref exApp, tmpUnZipDir.FullName, tmpDir.FullName);
                        outFileName = @outDir + @"\" + fileName;
                        DeleteExistFile(outFileName);
                        ZipFile.CreateFromDirectory(tmpDir.FullName, outFileName);
                        tmpDir.Delete(true);
                        tmpUnZipDir.Delete(true);
                    }
                }
                else if (Path.GetExtension(fileName) == ".xlsx")
                {
                    fileName = RemoveInvalidFilePathCharacters(fileName, "");
                    outFileName = @outDir + @"\" + fileName;
                    DeleteExistFile(outFileName);
                    FixBill(ref exApp, @file.FullName, outFileName);
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


        public static void FixBill(ref Excel.Application exApp, string inFileName, string outFileName = "")
        {
            // Объявляем приложение
            //Excel.Application exc = new Microsoft.Office.Interop.Excel.Application();

            Excel.XlReferenceStyle RefStyle = exApp.ReferenceStyle;

            Excel.Workbook wb = null;

            try
            {
                wb = exApp.Workbooks.Add(inFileName); // !!! 
            }
            catch (System.Exception ex)
            {
                throw new Exception("Не удалось загрузить файл! " + inFileName + "\n" + ex.Message);
            }
            //Console.WriteLine("Файл найден, начинаю работу. Это может занять несколько минут.");
            //Excel.Sheets excelsheets;

            FixAdress(wb);
            
            if (Properties.Settings.Default.DELETE_REASON)
                FixReason(wb);
            if (Properties.Settings.Default.REPLACE_SIGN)
                FixManager(wb, "Заместитель генерального директора\nРоманова Ю.В.\nпо доверенности № 17 от 27.03.2020");
            if (Properties.Settings.Default.DELETE_PAYWITHDEBT)
                FixPayWithDebt(wb);


            if (outFileName != "")
                wb.SaveAs(outFileName);
            else
                wb.SaveAs(inFileName);
            
            wb.Saved = true;
            wb.Close();

        }

        public static void FixReason(in Excel.Workbook wb, string reason = "")
        {
            Excel.Worksheet wsh = wb.Worksheets.get_Item(1) as Excel.Worksheet;

            Excel.Range excelcells;

            excelcells = wsh.get_Range("C19", "C19");
            excelcells.Value2 = reason;
        }

        public static void FixManager(in Excel.Workbook wb, string manager = "")
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

        public static void FixPayWithDebt(in Excel.Workbook wb)
        {
            Excel.Worksheet wsh = wb.Worksheets.get_Item(1) as Excel.Worksheet;

            Excel.Range excelcells;
            Excel.Range currentFind = null;
            //Excel.Range firstFind = null;

            excelcells = wsh.get_Range("A20");
            currentFind = excelcells.Find("Итого к оплате с учетом задолженности:", Type.Missing,
          Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
          Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext);
            if (currentFind != null)
            {
                excelcells = currentFind.EntireRow;
                excelcells.Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
            }
            excelcells = wsh.get_Range("A20");
            currentFind = excelcells.Find("Сумма пени на", Type.Missing,
          Excel.XlFindLookIn.xlValues, Excel.XlLookAt.xlPart,
          Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext);
            if (currentFind != null)
            {
                excelcells = currentFind.EntireRow;
                excelcells.Delete(Excel.XlDeleteShiftDirection.xlShiftToLeft);
            }

        }

        public static void FixAdress(in Excel.Workbook wb, double rowHeight = 12.75, int trimLenght = 90, int rowAmount = 2)
        // Исправляет высоту строки адреса Заказчика
        {
            string adress;
            int adressLenght;
            double adressRowHeight;

            Excel.Worksheet wsh = wb.Worksheets.get_Item(1) as Excel.Worksheet;

            Excel.Range excelcells;
            excelcells = wsh.get_Range("C16");
            adress = excelcells.Value2;
            adressLenght = adress.Length;
            if (((adressLenght / trimLenght)>rowAmount) | ((adressLenght % trimLenght) > 0))
            {
                adressRowHeight = (adressLenght / trimLenght) * rowHeight;
                if ((adressLenght % trimLenght) > 0)
                    adressRowHeight += rowHeight;
                excelcells = wsh.get_Range("A17");
                excelcells.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                excelcells.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignTop;
                excelcells.EntireRow.RowHeight = adressRowHeight;
            }
        }

        private async void StartFixButton_Click(object sender, EventArgs e)
        {

            Excel.Application exc = new Microsoft.Office.Interop.Excel.Application();
            ProgressBar progressBar = progressBar1;

            tableLayoutPanel1.Enabled = false;

            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;
            try
            {
                await Task.Run( () => FixDir(ref exc, Properties.Settings.Default.INDIR, Properties.Settings.Default.OUTDIR));
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

            exc.DisplayAlerts = false;
            //wb.Close();
            exc.Quit();
        }


    }
}
