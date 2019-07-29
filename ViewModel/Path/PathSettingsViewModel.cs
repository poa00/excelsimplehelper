using Model.Data.PatternMVVM;
using System.Windows.Forms;
using System.Windows.Input;

namespace ViewModel.Path
{
    public class PathSettingsViewModel
    {
        public PathModel Path { get; set; }

        public PathSettingsViewModel()
        {
            Path = new PathModel();

            SelectPathFolderResult = new RelayCommand(arg => СhoicePathFolderResult());
            SelectPathResulInputForParallelFolder = new RelayCommand(arg => СhoicePathResulInputForParallelFolder());
            SelectPathFileExcelDataStudents = new RelayCommand(arg => СhoicePathFileExcelDataStudents());
            SelectPathFileWordUdostovereniyeTemplate = new RelayCommand(arg => СhoicePathFileWordUdostovereniyeTemplate());
            SelectPathFileWordEvidenceTemplate = new RelayCommand(arg => СhoicePathFileWordEvidenceTemplate());
            SelectPathFileWordStatementTemplate = new RelayCommand(arg => СhoicePathFileWordStatementTemplate());
            SelectPathFileWordCertificateDGTemplate = new RelayCommand(arg => СhoicePathFileWordCertificateDGTemplate());
            SelectPathFileWordCertificate12DGTemplate = new RelayCommand(arg => СhoicePathFileWordCertificate12DGTemplate());
        }

        public ICommand SelectPathFolderResult { get; set; }
        public void СhoicePathFolderResult()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                Path.PathFolderResult = folderBrowser.SelectedPath;
            }
        }

        public ICommand SelectPathResulInputForParallelFolder { get; set; }
        public void СhoicePathResulInputForParallelFolder()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();

            if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
            {
                Path.PathResulInputForParallelFolder = folderBrowser.SelectedPath;
            }
        }
                        
        public ICommand SelectPathFileExcelDataStudents { get; set; }
        public void СhoicePathFileExcelDataStudents()
        {   
            OpenFileDialog openFileDialog = GetSettingFileExcelDialog();
            openFileDialog.ShowDialog();
            if (openFileDialog.SafeFileName.Length > 0)
            {
                Path.PathFileExcelDataStudents = openFileDialog.FileName;
            }
        }
                        
        public ICommand SelectPathFileWordUdostovereniyeTemplate { get; set; }
        public void СhoicePathFileWordUdostovereniyeTemplate()
        {
            OpenFileDialog openFileDialog = GetSettingFileWordDialog();
            openFileDialog.ShowDialog();
            if (openFileDialog.SafeFileName.Length > 0)
            {
                Path.PathFileWordUdostovereniyeTemplate = openFileDialog.FileName;
            }
        }

        public ICommand SelectPathFileWordEvidenceTemplate { get; set; }
        public void СhoicePathFileWordEvidenceTemplate()
        {
            OpenFileDialog openFileDialog = GetSettingFileWordDialog();

            openFileDialog.ShowDialog();
            if (openFileDialog.SafeFileName.Length > 0)
            {
                Path.PathFileWordEvidenceTemplate = openFileDialog.FileName;
            }
        }

        public ICommand SelectPathFileWordStatementTemplate { get; set; }
        public void СhoicePathFileWordStatementTemplate()
        {
            OpenFileDialog openFileDialog = GetSettingFileWordDialog();

            openFileDialog.ShowDialog();
            if (openFileDialog.SafeFileName.Length > 0)
            {
                Path.PathFileWordStatementTemplate = openFileDialog.FileName;
            }
        }

        public ICommand SelectPathFileWordCertificateDGTemplate { get; set; }
        public void СhoicePathFileWordCertificateDGTemplate()
        {
            OpenFileDialog openFileDialog = GetSettingFileWordDialog();
            openFileDialog.ShowDialog();
            if (openFileDialog.SafeFileName.Length > 0)
            {
                Path.PathFileWordCertificateDGTemplate = openFileDialog.FileName;
            }
        }

        public ICommand SelectPathFileWordCertificate12DGTemplate { get; set; }
        public void СhoicePathFileWordCertificate12DGTemplate()
        {
            OpenFileDialog openFileDialog = GetSettingFileWordDialog();
            openFileDialog.ShowDialog();
            if (openFileDialog.SafeFileName.Length > 0)
            {
                Path.PathFileWordCertificate12DGTemplate = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Настройка диалогового окна для выбора word файла
        /// </summary>
        /// <returns>Объект с настройками</returns>
        private OpenFileDialog GetSettingFileWordDialog(){
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "*.doc;*.docx";
            openFileDialog.Filter = "Microsoft Word (*.doc*)|*.docx*";
            openFileDialog.Title = "Выберите шаблон";
            return openFileDialog;
        }

        /// <summary>
        /// Настройка диалогового окна для выбора excel файла
        /// </summary>
        /// <returns>Объект с настройками</returns>
        private OpenFileDialog GetSettingFileExcelDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "*.xls;*.xlsx";
            openFileDialog.Filter = "Microsoft Excel (*.xls*)|*.xls*";
            openFileDialog.Title = "Выберите файл excel";
            return openFileDialog;
        }
    }
}
