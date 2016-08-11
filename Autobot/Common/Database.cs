using Autobot.Infrastructure;
using SQLite;

namespace Autobot.Common
{
    public class Database
    {
        private static Database instance;

        public static Database Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                }
                return instance;
            }
        }

        public void Initialize(string dbPath)
        {
            var db = new SQLiteConnection(dbPath);

            db.CreateTable<Action>();
            db.CreateTable<Condition>();
            db.CreateTable<Action>();
        }
    }
}