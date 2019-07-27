using Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.PatternMVVM.TrainingProgramm
{
    public class Programm3Model : INotifyPropertyChanged
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
        /// Название программы
        /// </summary>
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
        /// Кем и когда одобрена
        /// </summary>
        private string _DateNumberApproved;
        public string DateNumberApproved
        {
            get { return _DateNumberApproved; }
            set
            {
                if (_DateNumberApproved != value)
                {
                    _DateNumberApproved = value;
                    OnPropertyChanged("DateNumberApproved");
                }
            }
        }

        /// <summary>
        /// Список программ из базы данных
        /// </summary>
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
    }
}
