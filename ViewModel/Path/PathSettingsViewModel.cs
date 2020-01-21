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
            //SelectPathResulInputForParallelFolder = new RelayCommand(arg => СhoicePathResulInputForParallelFolder());
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
                        
        public ICommand SelectPathFileExcelDataStudents { get; set; }
        public void СhoicePathFileExcelDataStudents()
        {   
            OpenFileDialog openFileDialog = GetSettingFileExcelDialog("Выберете файл с данными студентов");
            openFileDialog.ShowDialog();
            if (openFileDialog.SafeFileName.Length > 0)
            {
                Path.PathFileExcelDataStudents = openFileDialog.FileName;
            }
        }
                        
        public ICommand SelectPathFileWordUdostovereniyeTemplate { get; set; }
        public void СhoicePathFileWordUdostovereniyeTemplate()
        {
            OpenFileDialog openFileDialog = GetSettingFileWordDialog("Выберете шаблон для удостоверения");
            openFileDialog.ShowDialog();
            if (openFileDialog.SafeFileName.Length > 0)
            {
                Path.PathFileWordUdostovereniyeTemplate = openFileDialog.FileName;
            }
        }

        public ICommand SelectPathFileWordEvidenceTemplate { get; set; }
        public void СhoicePathFileWordEvidenceTemplate()
        {
            OpenFileDialog openFileDialog = GetSettingFileWordDialog("Выберете шаблон для свидетельства");

            openFileDialog.ShowDialog();
            if (openFileDialog.SafeFileName.Length > 0)
            {
                Path.PathFileWordEvidenceTemplate = openFileDialog.FileName;
            }
        }

        public ICommand SelectPathFileWordStatementTemplate { get; set; }
        public void СhoicePathFileWordStatementTemplate()
        {
            OpenFileDialog openFileDialog = GetSettingFileWordDialog("Выберете шаблон для ведомости");

            openFileDialog.ShowDialog();
            if (openFileDialog.SafeFileName.Length > 0)
            {
                Path.PathFileWordStatementTemplate = openFileDialog.FileName;
            }
        }

        public ICommand SelectPathFileWordCertificateDGTemplate { get; set; }
        public void СhoicePathFileWordCertificateDGTemplate()
        {
            OpenFileDialog openFileDialog = GetSettingFileWordDialog("Выберете шаблон для сертификата");
            openFileDialog.ShowDialog();
            if (openFileDialog.SafeFileName.Length > 0)
            {
                Path.PathFileWordCertificateDGTemplate = openFileDialog.FileName;
            }
        }

        public ICommand SelectPathFileWordCertificate12DGTemplate { get; set; }
        public void СhoicePathFileWordCertificate12DGTemplate()
        {
            OpenFileDialog openFileDialog = GetSettingFileWordDialog("Выберете шаблон для сертификата 12 категории");
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
        private OpenFileDialog GetSettingFileWordDialog(string windowDescription)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "*.doc;*.docx";
            openFileDialog.Filter = "Microsoft Word (*.doc*)|*.docx*";
            openFileDialog.Title = windowDescription;
            return openFileDialog;
        }

        /// <summary>
        /// Настройка диалогового окна для выбора excel файла
        /// </summary>
        /// <returns>Объект с настройками</returns>
        private OpenFileDialog GetSettingFileExcelDialog(string windowDescription)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "*.xls;*.xlsx";
            openFileDialog.Filter = "Microsoft Excel (*.xls*)|*.xls*";
            openFileDialog.Title = windowDescription;
            return openFileDialog;
        }
    }
}
