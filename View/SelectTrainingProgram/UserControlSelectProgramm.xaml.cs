using System.Windows.Controls;
using View.SelectTrainingProgram.TrainingProgramm;

namespace View.SelectTrainingProgramm
{
    /// <summary>
    /// Логика взаимодействия для UserControlSelectProgram.xaml
    /// </summary>
    public partial class UserControlSelectProgramm : UserControl
    {
        public UserControlSelectProgramm()
        {
            InitializeComponent();
        }

        private void ListViewMenuPrograms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;

            switch (index)
            {
                case 0:
                    GridDataBase.Children.Clear();
                    GridDataBase.Children.Add(new UserControlProgramm012());
                    break;
                case 1:
                    GridDataBase.Children.Clear();
                    GridDataBase.Children.Add(new UserControlProgramm3());
                    break;

                default:
                    break;
            }
        }
    }
}
