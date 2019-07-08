using Model.Data.PatternMVVM;
using System.Data.Entity;
using Model.DataBase.Model;
using System.Windows.Input;
using Model.DataBase.Context;
using Model.Data.PatternMVVM.DataBase;

namespace ViewModel.DataBase
{
    public class CertificationsViewModel
    {
        public CertificateModel _certificate { get; set; }
        public FindCertificateModel findModel { get; set; }
        DataBaseContext DataBaseContext;
        
        public CertificationsViewModel()
        {
            DataBaseContext = new DataBaseContext();
            DataBaseContext.Certificate.Load();
            _certificate = new CertificateModel()
            {
                Certifications = DataBaseContext.Certificate.Local.ToBindingList()
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
                    Certificate certification = selectedItem as Certificate;

                    Certificate certificateEdit = new Certificate()
                    {
                        id = certification.id,
                        party = certification.party,
                        startEducation = certification.startEducation,
                        endEducation = certification.endEducation,
                        issueDate = certification.issueDate
                    };

                    // получаем измененный объект
                    certification = DataBaseContext.Certificate.Find(certificateEdit.id);
                    if (certification != null)
                    {
                        certification.party = certificateEdit.party;
                        certification.startEducation = certificateEdit.startEducation;
                        certification.endEducation = certificateEdit.endEducation;
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
                      Certificate certification = selectedItem as Certificate;
                      DataBaseContext.Certificate.Remove(certification);
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
                      Certificate certification = selectedItem as Certificate;
                      certification.LoadCertificate(certification);
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
            Certificate certificate = new Certificate();
            using (DataBaseContext context = new DataBaseContext())
            {
                if (findModel.Fio != "" && findModel.DataBirth != "")
                {
                    _certificate.IdStudent = student.FindIdStudentByFioAndDateBirth(context, findModel.Fio.Trim(), findModel.DataBirth.Trim());
                    _certificate.FindCertificateByIdStudent = DataBaseContext.Certificate.Local.ToBindingList();
                }
                else
                {
                    if (findModel.Fio != "")
                    {
                        _certificate.IdStudent = student.FindIdStudentByFio(context, findModel.Fio.Trim());
                        _certificate.FindCertificateByIdStudent = DataBaseContext.Certificate.Local.ToBindingList();
                    }
                }

                if(findModel.Group != "")
                {
                    _certificate.IdCertificate = certificate.FindIdCertificateByNumber(context, findModel.Group.Trim());
                    _certificate.FindCertificateByIdCertificate = DataBaseContext.Certificate.Local.ToBindingList();
                }
            }
        }
    }
}
