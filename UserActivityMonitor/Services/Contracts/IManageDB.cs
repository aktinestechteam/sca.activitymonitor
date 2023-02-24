using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityMonitor.Services.Contracts
{
    public interface IManageDB
    {
        void CreateSqlLiteDatabase();
        void Changepassword();
        void Setpassword();
        void Removepassword();
        void Createtbproduct();
        void Createt_Login_Table();
    }
}
