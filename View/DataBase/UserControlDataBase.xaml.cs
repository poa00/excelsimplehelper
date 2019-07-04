using System.Windows;
using System.Windows.Controls;
using View.DataBase.Document;

namespace View.DataBase
{
    /// <summary>
    /// Логика взаимодействия для UserControlDataBase.xaml
    /// </summary>
    public partial class UserControlDataBase : UserControl
    {
        public UserControlDataBase()
        {
            InitializeComponent();
        }

        private void ListViewMenuDataBase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;

            switch (index)
            {
                case 0:
                    GridDataBase.Children.Clear();
                    GridDataBase.Children.Add(new UserControlCertifications());
                    break;
                case 1:
                    GridDataBase.Children.Clear();
                    GridDataBase.Children.Add(new UserControlCertificateDGs());
                    break;

                default:
                    break;

            }
        }
    }
}
