using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.PatternMVVM
{
    public class FindCertificateModel : INotifyPropertyChanged
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
        /// Фамилия Имя Отчество
        /// </summary>
        private string _Fio;
        public string Fio
        {
            get { return _Fio; }
            set
            {
                if (_Fio != value)
                {
                    _Fio = value;
                    OnPropertyChanged("Fio");
                }
            }
        }
        
        /// <summary>
        /// Группа
        /// </summary>
        private string _Group;
        public string Group
        {
            get { return _Group; }
            set
            {
                if (_Group != value)
                {
                    _Group = value;
                    OnPropertyChanged("Group");
                }
            }
        }

        private string _DataBirth;
        public string DataBirth
        {
            get { return _DataBirth; }
            set
            {
                if (_DataBirth != value)
                {
                    _DataBirth = value;
                    OnPropertyChanged("DataBirth");
                }
            }
        }
    }
}
