using System.Windows.Controls;
using ViewModel.Document;

namespace View.Document
{
    /// <summary>
    /// Логика взаимодействия для UserControlStatementView.xaml
    /// </summary>
    public partial class UserControlStatementView : UserControl
    {
        public UserControlStatementView()
        {
            InitializeComponent();
            DataContext = new StatementViewModel();
        }
    }
}