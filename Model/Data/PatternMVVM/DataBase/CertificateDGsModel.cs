using Model.DataBase.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Model.Data.PatternMVVM.DataBase
{
    public class CertificateDGsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Id студента
        /// </summary>
        private int _IdStudent;
        public int IdStudent
        {
            get { return _IdStudent; }
            set
            {
                if (_IdStudent != value)
                {
                    _IdStudent = value;
                    OnPropertyChanged("IdStudent");
                }
            }
        }

        /// <summary>
        /// Id серртификата
        /// </summary>
        private int _IdCertificateDG;
        public int IdCertificateDG
        {
            get { return _IdCertificateDG; }
            set
            {
                if (_IdCertificateDG != value)
                {
                    _IdCertificateDG = value;
                    OnPropertyChanged("IdCertificateDG");
                }
            }
        }

        private IEnumerable<CertificateDGs> _Certificate;
        public IEnumerable<CertificateDGs> Certificate
        {
            get { return _Certificate; }
            set
            {
                if (_Certificate != value)
                {
                    _Certificate = value;
                    OnPropertyChanged("Certificate");
                }
            }
        }

        public IEnumerable<CertificateDGs> FindCertificateDGByIdStudent
        {
            get { return _Certificate; }
            set
            {
                if (_Certificate != _Certificate.Where(c => c.idStudent == _IdStudent))
                {
                    _Certificate = _Certificate.Where(c => c.idStudent == _IdStudent);
                    OnPropertyChanged("Certificate");
                }
            }
        }

        public IEnumerable<CertificateDGs> FindCertificateDGByIdCertificate
        {
            get { return _Certificate; }
            set
            {
                if (_Certificate != _Certificate.Where(c => c.id == _IdCertificateDG))
                {
                    _Certificate = _Certificate.Where(c => c.id == _IdCertificateDG);
                    OnPropertyChanged("Certificate");
                }
            }
        }
    }
}
