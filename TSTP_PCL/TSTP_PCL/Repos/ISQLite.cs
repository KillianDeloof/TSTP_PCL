using System;
using SQLite;

namespace TSTP_PCL.Repositories
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection(string databaseName);
        long GetSize(string databaseName);
    }
}