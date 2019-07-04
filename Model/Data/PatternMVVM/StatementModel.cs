using Model.DataBase.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model.Data.PatternMVVM
{
    public class StatementModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

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

        private IEnumerable<Programs> _Programs;
        public IEnumerable<Programs> Programs
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
