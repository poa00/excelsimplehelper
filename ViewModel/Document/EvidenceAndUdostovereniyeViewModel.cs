using Model.Data.PatternMVVM;
using Model.DataBase.Context;
using Model.Message;
using Model.Write.Word.Document;
using System.Data.Entity;

namespace ViewModel.Document
{
    public class EvidenceAndUdostovereniyeViewModel
    {
        public DocumentEvidenceAndUdostovereniyeModel EvidenceAndUdostovereniye { get; set; }
        DataBaseContext DataBaseContext;

        public EvidenceAndUdostovereniyeViewModel()
        {
            DataBaseContext = new DataBaseContext();
            DataBaseContext.Programs.Load();

            EvidenceAndUdostovereniye = new DocumentEvidenceAndUdostovereniyeModel()
            {
                DateStartEducation = "25.06.2019",
                DateEndEducation = "25.06.2019",
                DateIssueDocument = "25.06.2019",
                IsSelectedStatement = true,
                Group = "777",
                Programs = DataBaseContext.Programs.Local.ToBindingList()
            };
        }

        RelayCommand createDocument;
        public RelayCommand CreateDocument
        {
            get
            {
                return createDocument ??
                    (createDocument = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        MessageBug.ClearMessages();
                        // получаем выделенный объект
                        Model.DataBase.Model.Programs programs = selectedItem as Model.DataBase.Model.Programs;
                        
                        ManagerDocument evidenceAndUdostovereniye = new ManagerDocument(programs, EvidenceAndUdostovereniye);
                        evidenceAndUdostovereniye.DocumentCreate();

                        ManagerDocument statement = new ManagerDocument(programs, EvidenceAndUdostovereniye);
                        statement.DocumentCreate();

                        string message = " ";
                        foreach (string itr in MessageBug.GetMessages())
                        {
                            message += itr + "\n";
                        }
                        EvidenceAndUdostovereniye.Message = message;
                    }));
            }
        }       
    }
}
