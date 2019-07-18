using System.Windows.Controls;
using ViewModel.Programs;

namespace View.SelectTrainingProgram.TrainingProgramm
{
    /// <summary>
    /// Логика взаимодействия для UserControlProgram3.xaml
    /// </summary>
    public partial class UserControlProgramm3 : UserControl
    {
        public UserControlProgramm3()
        {
            InitializeComponent();
            DataContext = new Program3ViewModel();
        }
    }
}
