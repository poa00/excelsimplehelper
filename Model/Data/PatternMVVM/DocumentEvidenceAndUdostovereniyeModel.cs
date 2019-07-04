using Model.DataBase.Model;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model.Data.PatternMVVM
{
    public class DocumentEvidenceAndUdostovereniyeModel : INotifyPropertyChanged
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
        /// Нужна ли ведомость
        /// </summary>
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

        /// <summary>
        /// Сообщение
        /// </summary>
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
        /// Дата начала обучения
        /// </summary>
        private string _DateStartEducation;
        public string DateStartEducation
        {
            get { return _DateStartEducation; }
            set
            {
                if (_DateStartEducation != value)
                {
                    _DateStartEducation = value;
                    OnPropertyChanged("DateStartEducation");
                }
            }
        }

        /// <summary>
        /// Дата конца обучения
        /// </summary>
        private string _DateEndEducation;
        public string DateEndEducation
        {
            get { return _DateEndEducation; }
            set
            {
                if (_DateEndEducation != value)
                {
                    _DateEndEducation = value;
                    OnPropertyChanged("DateEndEducation");
                }
            }
        }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        private string _DateIssueDocument;
        public string DateIssueDocument
        {
            get { return _DateIssueDocument; }
            set
            {
                if (_DateIssueDocument != value)
                {
                    _DateIssueDocument = value;
                    OnPropertyChanged("DateIssueDocument");
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
    }
}
