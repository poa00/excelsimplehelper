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

            TextPathFileExcelDataStudentsUdostovereniye = Properties.Settings.Default.TextPathFileExcelDataStudentsUdostovereniye;
            TextPathFileExcelDataStudentsForStatement = Properties.Settings.Default.TextPathFileExcelDataStudentsForStatement;
            TextPathFileExcelDataStudentsForCertificateDG = Properties.Settings.Default.TextPathFileExcelDataStudentsForCertificateDG;
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
        
        private string _TextPathFileExcelDataStudentsUdostovereniye;
        public string TextPathFileExcelDataStudentsUdostovereniye
        {
            get { return _TextPathFileExcelDataStudentsUdostovereniye; }
            set
            {
                if (_TextPathFileExcelDataStudentsUdostovereniye != value)
                {
                    _TextPathFileExcelDataStudentsUdostovereniye = value;
                    Properties.Settings.Default.TextPathFileExcelDataStudentsUdostovereniye = TextPathFileExcelDataStudentsUdostovereniye;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("TextPathFileDataStudentsUdostovereniyeTemplate");
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
        
        private string _TextPathFileExcelDataStudentsForStatement;
        public string TextPathFileExcelDataStudentsForStatement
        {
            get { return _TextPathFileExcelDataStudentsForStatement; }
            set
            {
                if (_TextPathFileExcelDataStudentsForStatement != value)
                {
                    _TextPathFileExcelDataStudentsForStatement = value;
                    Properties.Settings.Default.TextPathFileExcelDataStudentsForStatement = _TextPathFileExcelDataStudentsForStatement;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("TextPathFileExcelDataStudentsForStatement");
                }
            }
        }
        
        private string _TextPathFileExcelDataStudentsForCertificateDG;
        public string TextPathFileExcelDataStudentsForCertificateDG
        {
            get { return _TextPathFileExcelDataStudentsForCertificateDG; }
            set
            {
                if (_TextPathFileExcelDataStudentsForCertificateDG != value)
                {
                    _TextPathFileExcelDataStudentsForCertificateDG = value;
                    Properties.Settings.Default.TextPathFileExcelDataStudentsForCertificateDG = _TextPathFileExcelDataStudentsForCertificateDG;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("TextPathFileExcelDataStudentsForCertificateDG");
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
