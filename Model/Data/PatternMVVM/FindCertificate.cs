using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.PatternMVVM
{
    public class FindCertificate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Отчество
        /// </summary>
        private string _Patronymic;
        public string Patronymic
        {
            get { return _Patronymic; }
            set
            {
                if (_Patronymic != value)
                {
                    _Patronymic = value;
                    OnPropertyChanged("Patronymic");
                }
            }
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        private string _Surname;
        public string Surname
        {
            get { return _Surname; }
            set
            {
                if (_Surname != value)
                {
                    _Surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }
    }
}
