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
            PathFolderResult = Properties.Settings.Default.PathFolderResult;
            //PathResulInputForParallelFolder = Properties.Settings.Default.PathResulInputForParallelFolder;

            PathFileWordUdostovereniyeTemplate = Properties.Settings.Default.PathFileWordUdostovereniyeTemplate;
            PathFileWordEvidenceTemplate = Properties.Settings.Default.PathFileWordEvidenceTemplate;
            PathFileWordStatementTemplate = Properties.Settings.Default.PathFileWordStatementTemplate;
            PathFileWordCertificateDGTemplate = Properties.Settings.Default.PathFileWordCertificateDGTemplate;
            PathFileWordCertificate12DGTemplate = Properties.Settings.Default.PathFileWordCertificate12DGTemplate;

            PathFileExcelDataStudents = Properties.Settings.Default.PathFileExcelDataStudents;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        private string _PathFolderResult;
        public string PathFolderResult
        {
            get { return _PathFolderResult; }
            set
            {
                if (_PathFolderResult != value)
                {
                    _PathFolderResult = value;
                    Properties.Settings.Default.PathFolderResult = _PathFolderResult;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("PathFolderResult");
                }
            }
        }

        //private string _PathResulInputForParallelFolder;
        //public string PathResulInputForParallelFolder
        //{
        //    get { return _PathResulInputForParallelFolder; }
        //    set
        //    {
        //        if (_PathResulInputForParallelFolder != value)
        //        {
        //            _PathResulInputForParallelFolder = value;
        //            Properties.Settings.Default.PathResulInputForParallelFolder = _PathResulInputForParallelFolder;
        //            Properties.Settings.Default.Save();
        //            OnPropertyChanged("PathResulInputForParallelFolder");
        //        }
        //    }
        //}
        
        private string _PathFileExcelDataStudents;
        public string PathFileExcelDataStudents
        {
            get { return _PathFileExcelDataStudents; }
            set
            {
                if (_PathFileExcelDataStudents != value)
                {
                    _PathFileExcelDataStudents = value;
                    Properties.Settings.Default.PathFileExcelDataStudents = PathFileExcelDataStudents;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("PathFileExcelDataStudents");
                }
            }
        }
        
        private string _PathFileWordUdostovereniyeTemplate;
        public string PathFileWordUdostovereniyeTemplate
        {
            get { return _PathFileWordUdostovereniyeTemplate; }
            set
            {
                if (_PathFileWordUdostovereniyeTemplate != value)
                {
                    _PathFileWordUdostovereniyeTemplate = value;
                    Properties.Settings.Default.PathFileWordUdostovereniyeTemplate = _PathFileWordUdostovereniyeTemplate;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("PathFileWordUdostovereniyeTemplate");
                }
            }
        }
        
        private string _PathFileWordEvidenceTemplate;
        public string PathFileWordEvidenceTemplate
        {
            get { return _PathFileWordEvidenceTemplate; }
            set
            {
                if (_PathFileWordEvidenceTemplate != value)
                {
                    _PathFileWordEvidenceTemplate = value;
                    Properties.Settings.Default.PathFileWordEvidenceTemplate = _PathFileWordEvidenceTemplate;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("PathFileWordEvidenceTemplate");
                }
            }
        }
        
        private string _PathFileWordStatementTemplate;
        public string PathFileWordStatementTemplate
        {
            get { return _PathFileWordStatementTemplate; }
            set
            {
                if (_PathFileWordStatementTemplate != value)
                {
                    _PathFileWordStatementTemplate = value;
                    Properties.Settings.Default.PathFileWordStatementTemplate = _PathFileWordStatementTemplate;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("PathFileWordStatementTemplate");
                }
            }
        }
        
        private string _PathFileWordCertificate12DGTemplate;
        public string PathFileWordCertificate12DGTemplate
        {
            get { return _PathFileWordCertificate12DGTemplate; }
            set
            {
                if (_PathFileWordCertificate12DGTemplate != value)
                {
                    _PathFileWordCertificate12DGTemplate = value;
                    Properties.Settings.Default.PathFileWordCertificate12DGTemplate = _PathFileWordCertificate12DGTemplate;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("PathFileWordCertificate12DGTemplate");
                }
            }
        }

        private string _PathFileWordCertificateDGTemplate;
        public string PathFileWordCertificateDGTemplate
        {
            get { return _PathFileWordCertificateDGTemplate; }
            set
            {
                if (_PathFileWordCertificateDGTemplate != value)
                {
                    _PathFileWordCertificateDGTemplate = value;
                    Properties.Settings.Default.PathFileWordCertificateDGTemplate = _PathFileWordCertificateDGTemplate;
                    Properties.Settings.Default.Save();
                    OnPropertyChanged("PathFileWordCertificateDGTemplate");
                }
            }
        }
    }
}
