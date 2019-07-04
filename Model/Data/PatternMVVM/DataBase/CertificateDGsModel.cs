using Model.DataBase.Model;
using System.Collections.Generic;
using System.ComponentModel;

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

        private IEnumerable<CertificateDGs> _Certifications;
        public IEnumerable<CertificateDGs> Certifications
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
    }
}
