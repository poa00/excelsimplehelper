using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
