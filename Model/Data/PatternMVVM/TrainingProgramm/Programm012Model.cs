using Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.PatternMVVM.TrainingProgramm
{
    public class Programm012Model : INotifyPropertyChanged
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
        /// Время обучения
        /// </summary>
        private string _Clock;
        public string Clock
        {
            get { return _Clock; }
            set
            {
                if (_Clock != value)
                {
                    _Clock = value;
                    OnPropertyChanged("Clock");
                }
            }
        }

        /// <summary>
        /// Повышение квалификации
        /// </summary>
        private string _Training;
        public string Training
        {
            get { return _Training; }
            set
            {
                if (_Training != value)
                {
                    _Training = value;
                    OnPropertyChanged("Training");
                }
            }
        }

        /// <summary>
        /// Уроки
        /// </summary>
        private string _Lesson;
        public string Lesson
        {
            get { return _Lesson; }
            set
            {
                if (_Lesson != value)
                {
                    _Lesson = value;
                    OnPropertyChanged("Lesson");
                }
            }
        }

        /// <summary>
        /// Уроки
        /// </summary>
        private List<string> _Type;
        public List<string> Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        /// <summary>
        /// Список программ из базы данных
        /// </summary>
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
