using System.Windows;
using System.Windows.Input;
using View.DataBase;
using View.Document;
using View.Path;

namespace View.Main
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void ListViewMenu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCuursorMenu(index);

            switch (index)
            {
                case 0:
                    GridPrincipial.Children.Clear();
                    break;
                case 1:
                    GridPrincipial.Children.Clear();
                    GridPrincipial.Children.Add(new UserControlDocumentEvidenceAndUdostovereniyeView());
                    break;
                case 2:
                    GridPrincipial.Children.Clear();
                    GridPrincipial.Children.Add(new UserControlCertificateDangerousGoodsView());
                    break;
                case 3:
                    GridPrincipial.Children.Clear();
                    GridPrincipial.Children.Add(new UserControlDataBase());
                    break;
                case 4:
                    GridPrincipial.Children.Clear();
                    GridPrincipial.Children.Add(new UserControlPathSettings());
                    break;

                default:
                    break;

            }
        }

        private void MoveCuursorMenu(int index)
        {
            TransitionContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, 100 + (60 * index), 0, 0);
        }

        private void Button_Click_Close_Application(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
