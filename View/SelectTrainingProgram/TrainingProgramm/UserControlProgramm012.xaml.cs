using System.Windows.Controls;
using ViewModel.Programs;

namespace View.SelectTrainingProgram.TrainingProgramm
{
    /// <summary>
    /// Логика взаимодействия для UserControlProgram012.xaml
    /// </summary>
    public partial class UserControlProgramm012 : UserControl
    {
        public UserControlProgramm012()
        {
            InitializeComponent();
            DataContext = new Program012ViewModel();
        }
    }
}
