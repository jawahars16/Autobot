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
            return await connection.Table<Rule>().Where(rule => rule.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Rule>> GetRulesAsync(string tag)
        {
            return await connection.Table<Rule>().Where(rule => rule.Tag == tag).ToListAsync();
        }

        public async Task<List<Rule>> GetRulesAsync(params string[] ids)
        {
            return await connection.Table<Rule>().Where(rule => ids.Any(id => id == rule.Id)).ToListAsync();
        }

        public async Task<List<Rule>> GetRulesAsync()
        {
            return await connection.Table<Rule>().ToListAsync();
        }

        public async Task<Geofence> GetGeofence(string id)
        {
            return await connection.Table<Geofence>().Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task InitializeAsync(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);

            await Task.WhenAll(
                connection.CreateTableAsync<Model.Action>(),
                connection.CreateTableAsync<Condition>(),
                connection.CreateTableAsync<Trigger>(),
                connection.CreateTableAsync<Rule>(),
                connection.CreateTableAsync<Geofence>()
            );
        }

        public async Task<List<Geofence>> GetGeofenceList()
        {
            return await connection.Table<Geofence>().ToListAsync();
        }

        public async Task UpdateAsync(object entity)
        {
            await connection.UpdateAsync(entity);
        }

        public async Task DeleteAsync(object entity)
        {
            await connection.DeleteAsync(entity);
        }

        public async Task LoadAsync(Rule rule)
        {
            Trigger trigger = await connection.Table<Trigger>().Where(tri => tri.Rule == rule.Id).FirstOrDefaultAsync();
            List<Condition> conditions = await connection.Table<Condition>().Where(condition => condition.Rule == rule.Id).ToListAsync();
            List<Action> actions = await connection.Table<Action>().Where(action => action.Rule == rule.Id).ToListAsync();

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