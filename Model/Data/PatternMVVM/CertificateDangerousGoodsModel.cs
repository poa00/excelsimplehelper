using Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.PatternMVVM
{
    public class CertificateDangerousGoodsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool _IsSelectedStatement;
        public bool IsSelectedStatement
        {
            get { return _IsSelectedStatement; }
            set
            {
                if (_IsSelectedStatement != value)
                {
                    _IsSelectedStatement = value;
                    OnPropertyChanged("IsSelectedStatement");
                }
            }
        }

        private string _Message;
        public string Message
        {
            get { return _Message; }
            set
            {
                if (_Message != value)
                {
                    _Message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        /// <summary>
        /// Номер сертификата
        /// </summary>
        private string _Number;
        public string Number
        {
            get { return _Number; }
            set
            {
                if (_Number != value)
                {
                    _Number = value;
                    OnPropertyChanged("Number");
                }
            }
        }

        /// <summary>
        /// Группа обучения
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

        private string _DateIssue;
        public string DateIssue
        {
            get { return _DateIssue; }
            set
            {
                if (_DateIssue != value)
                {
                    _DateIssue = value;
                    OnPropertyChanged("DateIssue");
                }
            }
        }

        private IEnumerable<ProgramDGs> _Programs;
        public IEnumerable<ProgramDGs> Programs
        {
            get { return _Programs; }
            set
            {
                if (_Programs != value)
                {
                    _Programs = value;
                    OnPropertyChanged("Programs");
                }
            }
        }
    }
}
