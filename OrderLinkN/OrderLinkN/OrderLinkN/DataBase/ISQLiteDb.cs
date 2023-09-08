using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderLinkN.DataBase
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
