using System.Windows.Controls;
using ViewModel.Document;

namespace View.Document
{
    /// <summary>
    /// Логика взаимодействия для UserControlCertificateView.xaml
    /// </summary>
    public partial class UserControlDocumentEvidenceAndUdostovereniyeView : UserControl
    {
        public UserControlDocumentEvidenceAndUdostovereniyeView()
        {
            InitializeComponent();
            DataContext = new EvidenceAndUdostovereniyeViewModel();
        }
    }
}
