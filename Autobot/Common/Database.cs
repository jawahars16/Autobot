using Autobot.Infrastructure;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autobot.Common
{
    public class Database
    {
        private static Database instance;

        private SQLiteAsyncConnection connection;

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

        public async Task<List<Rule>> GetRulesAsync()
        {
            return await connection.Table<Rule>().ToListAsync();
        }

        public async Task InitializeAsync(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);

            await Task.WhenAll(
                connection.CreateTableAsync<Action>(),
                connection.CreateTableAsync<Condition>(),
                connection.CreateTableAsync<Trigger>(),
                connection.CreateTableAsync<Rule>()
            );
        }

        public async Task SaveAsync(object entity)
        {
            await connection.InsertAsync(entity);
        }
    }
}