using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    interface IStudentRecord
    {
        string Check(string key, string value, int indexLine);
        string CheckMark(string date, int indexLine);
        string CheckDate(string date, int indexLine);

        void AddPropertyRecord(string bookmark, string value, int indexLine);
        void AddPropertyRecord(string bookmark, string value);
    }
}
