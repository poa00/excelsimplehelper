using Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.PatternMVVM.DataBase
{
    public class CertificationsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private IEnumerable<Certifications> _Certifications;
        public IEnumerable<Certifications> Certifications
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
