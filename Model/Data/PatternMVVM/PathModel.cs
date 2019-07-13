using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.PatternMVVM
{
    public class PathModel : INotifyPropertyChanged
    {

        public PathModel()
        {
            TextPathFolderResult = Properties.Settings.Default.TextPathFolderResult;
            TextPathResulInputForParallelFolder = Properties.Settings.Default.TextPathResulInputForParallelFolder;

            TextPathFileWordUdostovereniyeTemplate = Properties.Settings.Default.TextPathFileWordUdostovereniyeTemplate;
            TextPathFileWordEvidenceTemplate = Properties.Settings.Default.TextPathFileWordEvidenceTemplate;
            TextPathFileWordStatementTemplate = Properties.Settings.Default.TextPathFileWordStatementTemplate;
            TextPathFileWordCertificateDGTemplate = Properties.Settings.Default.TextPathFileWordCertificateDGTemplate;
            TextPathFileWordCertificate12DGTemplate = Properties.Settings.Default.TextPathFileWordCertificate12DGTemplate;

            TextPathFileExcelDataStudents = Properties.Settings.Default.TextPathFileExcelDataStudents;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        private string _TextPathFolderResult;
        public string TextPathFolderResult
        {
            get { return _TextPathFolderResult; }
            set
            {
                if (_TextPathFolderResult != value)
                {
                    _TextPathFolderResult = value;
                    Properties.Settings.Default.TextPathFolderResult = _TextPathFolderResult;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("TextPathFolderResult");
                }
            }
        }

        private string _TextPathResulInputForParallelFolder;
        public string TextPathResulInputForParallelFolder
        {
            get { return _TextPathResulInputForParallelFolder; }
            set
            {
                if (_TextPathResulInputForParallelFolder != value)
                {
                    _TextPathResulInputForParallelFolder = value;
                    Properties.Settings.Default.TextPathResulInputForParallelFolder = _TextPathResulInputForParallelFolder;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("TextPathResulInputForParallelFolder");
                }
            }
        }
        
        private string _TextPathFileExcelDataStudents;
        public string TextPathFileExcelDataStudents
        {
            get { return _TextPathFileExcelDataStudents; }
            set
            {
                if (_TextPathFileExcelDataStudents != value)
                {
                    _TextPathFileExcelDataStudents = value;
                    Properties.Settings.Default.TextPathFileExcelDataStudents = TextPathFileExcelDataStudents;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("TextPathFileExcelDataStudents");
                }
            }
        }
        
        private string _TextPathFileWordUdostovereniyeTemplate;
        public string TextPathFileWordUdostovereniyeTemplate
        {
            get { return _TextPathFileWordUdostovereniyeTemplate; }
            set
            {
                if (_TextPathFileWordUdostovereniyeTemplate != value)
                {
                    _TextPathFileWordUdostovereniyeTemplate = value;
                    Properties.Settings.Default.TextPathFileWordUdostovereniyeTemplate = _TextPathFileWordUdostovereniyeTemplate;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("TextPathFileWordUdostovereniye");
                }
            }
        }
        
        private string _TextPathFileWordEvidenceTemplate;
        public string TextPathFileWordEvidenceTemplate
        {
            get { return _TextPathFileWordEvidenceTemplate; }
            set
            {
                if (_TextPathFileWordEvidenceTemplate != value)
                {
                    _TextPathFileWordEvidenceTemplate = value;
                    Properties.Settings.Default.TextPathFileWordEvidenceTemplate = _TextPathFileWordEvidenceTemplate;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("TextPathFileWordEvidenceTemplate");
                }
            }
        }
        
        private string _TextPathFileWordStatementTemplate;
        public string TextPathFileWordStatementTemplate
        {
            get { return _TextPathFileWordStatementTemplate; }
            set
            {
                if (_TextPathFileWordStatementTemplate != value)
                {
                    _TextPathFileWordStatementTemplate = value;
                    Properties.Settings.Default.TextPathFileWordStatementTemplate = _TextPathFileWordStatementTemplate;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("TextPathFileWordStatementTemplate");
                }
            }
        }
        
        private string _TextPathFileWordCertificate12DGTemplate;
        public string TextPathFileWordCertificate12DGTemplate
        {
            get { return _TextPathFileWordCertificate12DGTemplate; }
            set
            {
                if (_TextPathFileWordCertificate12DGTemplate != value)
                {
                    _TextPathFileWordCertificate12DGTemplate = value;
                    Properties.Settings.Default.TextPathFileWordCertificate12DGTemplate = _TextPathFileWordCertificate12DGTemplate;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("TextPathFileWordCertificate12DGTemplate");
                }
            }
        }

        private string _TextPathFileWordCertificateDGTemplate;
        public string TextPathFileWordCertificateDGTemplate
        {
            get { return _TextPathFileWordCertificateDGTemplate; }
            set
            {
                if (_TextPathFileWordCertificateDGTemplate != value)
                {
                    _TextPathFileWordCertificateDGTemplate = value;
                    Properties.Settings.Default.TextPathFileWordCertificateDGTemplate = _TextPathFileWordCertificateDGTemplate;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("TextPathFileWordCertificateDGTemplate");
                }
            }
        }
    }
}
