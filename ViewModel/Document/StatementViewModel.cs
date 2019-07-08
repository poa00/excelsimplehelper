using Model.Data.PatternMVVM;
using Model.DataBase.Context;
using Model.DataBase.Model;
using Model.Write.Word;
using System.Collections.Generic;
using System.Data.Entity;

namespace ViewModel.Document
{
    public class StatementViewModel
    {
        public StatementModel _Statement { get; set; }
        DataBaseContext DataBaseContext;

        public StatementViewModel()
        {
            DataBaseContext = new DataBaseContext();
            DataBaseContext.Programs.Load();

            _Statement = new StatementModel()
            {
                Group = "666",
                Programs = DataBaseContext.Programs.Local.ToBindingList()
            };
        }

        RelayCommand statementCreate;
        public RelayCommand StatementCreate
        {
            get
            {
                return statementCreate ??
                    (statementCreate = new RelayCommand((selectedItem) =>
                    {
                        if (selectedItem == null) return;
                        // получаем выделенный объект
                        Programs programs = selectedItem as Programs;
                        Statement statement = new Statement(programs.name, _Statement);
                        List<string> result = statement.DocumentCreate();
                        foreach (string itr in result)
                        {
                            _Statement.Message += itr + "\n";
                        }
                    }));
            }
        }          
    }
}
