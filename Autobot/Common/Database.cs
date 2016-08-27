using Autobot.Model;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public async Task<Rule> GetRuleAsync(string id)
        {
            return await connection.Table<Rule>().Where(rule => rule.PrimaryKey == id).FirstOrDefaultAsync();
        }

        public async Task<List<Rule>> GetRulesAsync(string id)
        {
            return await connection.Table<Rule>().Where(rule => rule.RuleId == id).ToListAsync();
        }

        public async Task<List<Rule>> GetRulesAsync(params string[] ids)
        {
            return await connection.Table<Rule>().Where(rule => ids.Any(id => id == rule.PrimaryKey)).ToListAsync();
        }

        public async Task<List<Rule>> GetRulesAsync()
        {
            return await connection.Table<Rule>().ToListAsync();
        }

        public async Task InitializeAsync(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);

            await Task.WhenAll(
                connection.CreateTableAsync<Model.Action>(),
                connection.CreateTableAsync<Condition>(),
                connection.CreateTableAsync<Trigger>(),
                connection.CreateTableAsync<Rule>()
            );
        }

        public async Task DeleteAsync(object entity)
        {
            await connection.DeleteAsync(entity);
        }

        public async Task LoadAsync(Rule rule)
        {
            Trigger trigger = await connection.Table<Trigger>().Where(tri => tri.Rule == rule.PrimaryKey).FirstOrDefaultAsync();
            List<Condition> conditions = await connection.Table<Condition>().Where(condition => condition.Rule == rule.PrimaryKey).ToListAsync();
            List<Action> actions = await connection.Table<Action>().Where(action => action.Rule == rule.PrimaryKey).ToListAsync();

            rule.Trigger = trigger;
            rule.Conditions = new ObservableCollection<Condition>(conditions);
            rule.Actions = new ObservableCollection<Action>(actions);
        }

        public async Task SaveAsync(object entity)
        {
            await connection.InsertAsync(entity);
        }
    }
}