﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polling.Services.Contracts
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
