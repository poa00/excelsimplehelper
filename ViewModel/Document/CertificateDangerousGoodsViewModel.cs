using Model.Data.PatternMVVM;
using Model.DataBase.Context;
using Model.DataBase.Model;
using Model.Message;
using Model.Write.Word.Document;
using System.Collections.Generic;
using System.Data.Entity;

namespace ViewModel.Document
{
    public class CertificateDangerousGoodsViewModel
    {
        public CertificateDangerousGoodsModel CertificateDangerousGoods { get; set; }
        DataBaseContext DataBaseContext;

        public CertificateDangerousGoodsViewModel()
        {
            DataBaseContext = new DataBaseContext();
            DataBaseContext.ProgramDGs.Load();

            CertificateDangerousGoods = new CertificateDangerousGoodsModel()
            {
                DateIssue = "11.11.2019",
                Number = "999",
                IsSelectedStatement = false,
                Programs = DataBaseContext.ProgramDGs.Local.ToBindingList()
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
                        ProgramDGs programs = selectedItem as ProgramDGs;
                        ManagerDocument managerDocument = new ManagerDocument(programs, CertificateDangerousGoods);
                        managerDocument.DocumentCreate();

                        string message = " ";
                        foreach (string itr in MessageBug.GetMessages())
                        {
                            message += itr + "\n";
                        }
                        CertificateDangerousGoods.Message = message;
                    }));
            }
        }
    }
}
