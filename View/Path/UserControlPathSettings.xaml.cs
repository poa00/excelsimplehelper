using System.Windows.Controls;
using ViewModel.Path;

namespace View.Path
{
    /// <summary>
    /// Логика взаимодействия для UserControlPathSettings.xaml
    /// </summary>
    public partial class UserControlPathSettings : UserControl
    {
        public UserControlPathSettings()
        {
            InitializeComponent();
            DataContext = new PathSettingsViewModel();
        }
    }
}