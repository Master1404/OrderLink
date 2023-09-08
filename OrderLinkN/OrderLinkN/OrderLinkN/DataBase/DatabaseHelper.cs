using OrderLinkN.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace OrderLinkN.DataBase
{
    public class DatabaseHelper
    {
        private SQLiteAsyncConnection _connection;

        public DatabaseHelper()
        {
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _connection.CreateTableAsync<User>().Wait();
        }
    }
}
