using Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.PatternMVVM.DataBase
{
    public class CertificateModel : INotifyPropertyChanged
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
        private int _IdCertificate;
        public int IdCertificate
        {
            get { return _IdCertificate; }
            set
            {
                if (_IdCertificate != value)
                {
                    _IdCertificate = value;
                    OnPropertyChanged("IdCertificate");
                }
            }
        }

        private IEnumerable<Certificate> _Certifications;
        public IEnumerable<Certificate> Certifications
        {
            get { return _Certifications; }
            set
            {
                if (_Certifications != value)
                {
                    _Certifications = value;
                    OnPropertyChanged("Certifications");
                }
            }
        }
        
        public IEnumerable<Certificate> FindCertificateByIdStudent
        {
            get { return _Certifications; }
            set
            {
                if (_Certifications != _Certifications.Where(c => c.idStudent == _IdStudent))
                {
                    _Certifications = _Certifications.Where(c => c.idStudent == _IdStudent);
                    OnPropertyChanged("Certifications");
                }
            }
        }

        public IEnumerable<Certificate> FindCertificateByIdCertificate
        {
            get { return _Certifications; }
            set
            {
                if (_Certifications != _Certifications.Where(c => c.id == _IdCertificate))
                {
                    _Certifications = _Certifications.Where(c => c.id == _IdCertificate);
                    OnPropertyChanged("Certifications");
                }
            }
        }
    }
}
