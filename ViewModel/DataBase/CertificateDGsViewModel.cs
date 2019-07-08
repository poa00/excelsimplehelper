using Model.Data.PatternMVVM;
using Model.Data.PatternMVVM.DataBase;
using Model.DataBase.Context;
using Model.DataBase.Model;
using System.Data.Entity;
using System.Windows.Input;

namespace ViewModel.DataBase
{
    public class CertificateDGsViewModel
    {
        public CertificateDGsModel _certificate { get; set; }
        DataBaseContext DataBaseContext;

        public FindCertificateModel findModel { get; set; }

        public CertificateDGsViewModel()
        {
            DataBaseContext = new DataBaseContext();
            DataBaseContext.CertificateDGs.Load();
            _certificate = new CertificateDGsModel()
            {
                Certificate = DataBaseContext.CertificateDGs.Local.ToBindingList()
            };

            findModel = new FindCertificateModel()
            {
                Fio = "",
                Group = "",
                DataBirth = ""
            };
            certifictationFind = new RelayCommand(arg => CertifictationFind());
        }

        RelayCommand certifictationEdit;
        // команда редактирования
        public RelayCommand CertifictationEdit
        {
            get
            {
                return certifictationEdit ??
                  (certifictationEdit = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      CertificateDGs certification = selectedItem as CertificateDGs;

                      CertificateDGs certificateEdit = new CertificateDGs()
                      {
                          id = certification.id,
                          party = certification.party,
                          issueDate = certification.issueDate
                      };

                      // получаем измененный объект
                      certification = DataBaseContext.CertificateDGs.Find(certificateEdit.id);
                      if (certification != null)
                      {
                          certification.party = certificateEdit.party;
                          certification.issueDate = certificateEdit.issueDate;
                          DataBaseContext.Entry(certification).State = EntityState.Modified;
                          DataBaseContext.SaveChanges();
                      }

                  }));
            }
        }

        RelayCommand certifictationDelete;
        // команда удаления
        public RelayCommand CertifictationDelete
        {
            get
            {
                return certifictationDelete ??
                  (certifictationDelete = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      CertificateDGs certification = selectedItem as CertificateDGs;
                      DataBaseContext.CertificateDGs.Remove(certification);
                      DataBaseContext.SaveChanges();
                  }));
            }
        }


        RelayCommand certifictationLoad;
        /// <summary>
        /// Загрузка Сертификата
        /// </summary>
        public RelayCommand CertifictationLoad
        {
            get
            {
                return certifictationLoad ??
                  (certifictationLoad = new RelayCommand((selectedItem) =>
                  {
                      if (selectedItem == null) return;
                      // получаем выделенный объект
                      CertificateDGs certification = selectedItem as CertificateDGs;
                      certification.LoadCertification(certification);
                  }));
            }
        }

        /// <summary>
        /// Поиск по параметрам
        /// </summary>
        public ICommand certifictationFind { get; set; }
        public void CertifictationFind()
        {
            Students student = new Students();
            CertificateDGs certificate = new CertificateDGs();
            using (DataBaseContext context = new DataBaseContext())
            {
                if (findModel.Fio != "" && findModel.DataBirth != "")
                {
                    _certificate.IdStudent = student.FindIdStudentByFioAndDateBirth(context, findModel.Fio.Trim(), findModel.DataBirth.Trim());
                    _certificate.FindCertificateDGByIdStudent = DataBaseContext.CertificateDGs.Local.ToBindingList();
                }
                else
                {
                    if (findModel.Fio != "")
                    {
                        _certificate.IdStudent = student.FindIdStudentByFio(context, findModel.Fio.Trim());
                        _certificate.FindCertificateDGByIdStudent = DataBaseContext.CertificateDGs.Local.ToBindingList();
                    }
                }

                if (findModel.Group != "")
                {
                    _certificate.IdCertificateDG = certificate.FindIdCertificateByNumber(context, findModel.Group.Trim());
                    _certificate.FindCertificateDGByIdCertificate = DataBaseContext.CertificateDGs.Local.ToBindingList();
                }
            }
        }
    }
}
