using TSTP_PCL.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

/* ------------------------------------------ how to use ---------------------------
 public MainPageViewModel() {
  ...
  var database = new Database("People"); // Creates (if does not exist) a database named People
  database.CreateTable<Person>(); // Creates (if does not exist) a table of type Person
  ...

}


    Classes that needs to be saved to database need to inherit from  "MobileAppHowest.Models.BaseItem"    !!!!!!!!!!!!!!!!!
     
     */

namespace TSTP_PCL.Repositories
{
    public class DataBaseRepos// : ISQLite
    {

        static object locker = new object();

        ISQLite SQLite
        {
            get
            {
                return DependencyService.Get<ISQLite>();
            }
        }
        readonly SQLiteConnection _connection;
        readonly string DatabaseName;



        /// <summary>
        /// Makes a connection to the database
        /// </summary>
        /// <param name="databaseName">the name of the database</param>
        public DataBaseRepos(string databaseName)
        {
            DatabaseName = databaseName;
            _connection = DependencyService.Get<ISQLite>().GetConnection(DatabaseName);//get a registered class that implements the ISQLite interface and call its GetConnection method.
        }


        public long GetSize()
        {
            return SQLite.GetSize(DatabaseName);
        }


        /// <summary>
        /// create a table in the databace, if it doesn't already exist
        /// </summary>
        /// <typeparam name="T">The ObjectType of the table</typeparam>
        public void CreateTable<T>()
        {
            lock (locker)
            {
                _connection.CreateTable<T>();
            }
        }


        /// <summary>
        /// Update an item to the database, or insert it if it wasn't in the databace yet
        /// </summary>
        /// <typeparam name="T">ObjectType of the item</typeparam>
        /// <param name="item">The item to save to the databace</param>
        /// <returns>The Autoincremented ID-key of the object</returns>
        public int SaveItem<T>(T item)
        {
            lock (locker)
            {
                var id = ((BaseItem)(object)item).ID;
                if (id != 0)
                {
                    _connection.Update(item);
                    return id;
                }
                else
                {
                    try
                    {
                        return _connection.Insert(item);
                    }
                    catch(Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                        return -1;
                    }
                }
            }
        }


        /// <summary>
        /// Executes a prepared querry that doesn't return results  (ex. INSERT, DELETE, UPDATE)
        /// </summary>
        /// <param name="query">the fully escaped SQL</param>
        /// <param name="args">arguments to substitude the occurences of "?" in the querry</param>
        public void ExecuteQuery(string query, object[] args)
        {
            lock (locker)
            {
                _connection.Execute(query, args);
            }
        }

        /// <summary>
        /// Executes a SQL querry and returns the results
        /// </summary>
        /// <typeparam name="T">Table to run the querry on</typeparam>
        /// <param name="query">the fully escaped SQL</param>
        /// <param name="args">arguments to substitude the occurences of "?" in the querry</param>
        /// <returns></returns>
        public List<T> Query<T>(string query, object[] args) where T : new()
        {
            lock (locker)
            {
                return _connection.Query<T>(query, args);
            }
        }


        /// <summary>
        /// Gets The objects from a table
        /// </summary>
        /// <typeparam name="T">The ObjectType</typeparam>
        /// <returns>returns a IEnumerable of the given ObjectType</returns>
        public IEnumerable<T> GetItems<T>() where T : new()
        {
            lock (locker)
            {
                return (from i in _connection.Table<T>() select i).ToList();
            }
        }


        /// <summary>
        /// Deletes an item from the database
        /// </summary>
        /// <typeparam name="T">The OBjectType or Table</typeparam>
        /// <param name="id">the ibject to delete</param>
        /// <returns></returns>
        public int DeleteItem<T>(int id)
        {
            lock (locker)
            {
                return _connection.Delete<T>(id);
            }
        }


        /// <summary>
        /// Deletes all entrys From the given Table in the database
        /// </summary>
        /// <typeparam name="T">The ObjectType or Table</typeparam>
        /// <returns></returns>
        public int DeleteAll<T>()
        {
            lock (locker)
            {
                return _connection.DeleteAll<T>();
            }
        }






        

        //------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Haalt het eerste (of een default) UserInfo-object op uit de database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserInfo</returns>
        public UserInfo GetUser(int id)
        {
            return _connection.Table<UserInfo>().FirstOrDefault(t => t.ID == id);
        }

        /// <summary>
        /// Haalt de laatste (of een default) AuthenticatedUser-object op uit de database.
        /// Gebruik in context van ophalen tokens.
        /// </summary>
        /// <returns>AuthenticatedUser</returns>
        public AuthenticatedUser GetAuthorization()
        {
            return _connection.Table<AuthenticatedUser>().Last();
        }
    }
}
