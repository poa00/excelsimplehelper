using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    interface IStudentRecord
    {
        string Check(string key, string value, byte indexLine);
        string CheckMark(string date, byte indexLine);
        string CheckDate(string date, byte indexLine);

        void AddPropertyRecord(string bookmark, string value, byte indexLine);
        void AddPropertyRecord(string bookmark, string value);
    }
}
