using Model.Data.PatternMVVM;
using Model.DataBase.Context;
using Model.DataBase.Model;
using Model.Write.Word.Document;
using System.Collections.Generic;
using System.Data.Entity;

namespace ViewModel.Document
{
    public class DocumentEvidenceAndUdostovereniyeViewModel
    {
        public DocumentEvidenceAndUdostovereniyeModel EvidenceAndUdostovereniye { get; set; }
        DataBaseContext DataBaseContext;

        public DocumentEvidenceAndUdostovereniyeViewModel()
        {
            DataBaseContext = new DataBaseContext();
            DataBaseContext.Programs.Load();

            EvidenceAndUdostovereniye = new DocumentEvidenceAndUdostovereniyeModel()
            {
                DateStartEducation = "11.09.2001",
                DateEndEducation = "11.03.2011",
                DateIssueDocument = "25.06.2019",
                IsSelectedStatement = true,
                Group = "666",
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
                        // получаем выделенный объект
                        Programs programs = selectedItem as Programs;
                        
                        ManagerDocument managerDocument = new ManagerDocument(programs, EvidenceAndUdostovereniye);
                        List<string> result = managerDocument.DocumentCreate();
                        foreach (string itr in result)
                        {
                            EvidenceAndUdostovereniye.Message += itr + "\n";
                        }
                    }));
            }
        }       
    }
}
