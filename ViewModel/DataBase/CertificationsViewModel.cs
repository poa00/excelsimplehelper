using Model.Data;
using Model.Data.PatternMVVM.DataBase;
using Model.DataBase.Context;
using Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.DataBase
{
    public class CertificationsViewModel
    {
        public CertificationsModel certifications { get; set; }
        DataBaseContext DataBaseContext;
        RelayCommand certifictationEdit;
        RelayCommand certifictationDelete;
        RelayCommand certifictationLoad;

        public CertificationsViewModel()
        {
            DataBaseContext = new DataBaseContext();
            DataBaseContext.Certifications.Load();

            certifications = new CertificationsModel()
            {
                Certifications = DataBaseContext.Certifications.Local.ToBindingList()
            };
        }

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
                    Certifications certification = selectedItem as Certifications;

                    Certifications certificateEdit = new Certifications()
                    {
                        id = certification.id,
                        party = certification.party,
                        startEducation = certification.startEducation,
                        endEducation = certification.endEducation,
                        issueDate = certification.issueDate
                    };

                    // получаем измененный объект
                    certification = DataBaseContext.Certifications.Find(certificateEdit.id);
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
                      Certifications certification = selectedItem as Certifications;
                      DataBaseContext.Certifications.Remove(certification);
                      DataBaseContext.SaveChanges();
                  }));
            }
        }

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
                      Certifications certification = selectedItem as Certifications;
                      certification.LoadCertification(certification);
                  }));
            }
        }
    }
}
