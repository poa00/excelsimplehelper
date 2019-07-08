using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ViewModel.DataBase;

namespace View.DataBase.Document
{
    /// <summary>
    /// Логика взаимодействия для UserControlCertifications.xaml
    /// </summary>
    public partial class UserControlCertifications : UserControl
    {
        public UserControlCertifications()
        {
            InitializeComponent();
            DataContext = new CertificationsViewModel();
        }
    }
}
