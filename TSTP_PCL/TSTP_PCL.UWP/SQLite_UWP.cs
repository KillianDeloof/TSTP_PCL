using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Xamarin.Forms;
using Windows.Storage;
using TSTP_PCL.Repositories;
using TSTP_PCL.UWP;

[assembly: Dependency(typeof(SQLite_UWP))]

namespace TSTP_PCL.UWP
{
    public class SQLite_UWP : ISQLite
    {
        string GetPath(string databaseName)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentException("invalid databasename", nameof(databaseName));
            }
            var sqliteFilename = $"{databaseName}.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            return path;
        }

        public SQLiteConnection GetConnection(string databaseName)
        {
            return new SQLiteConnection(GetPath(databaseName));
        }

        public long GetSize(string databaseName)
        {
            var fileInfo = new FileInfo(GetPath(databaseName));
            return fileInfo != null ? fileInfo.Length : 0;
        }
    }
}
