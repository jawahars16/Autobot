using Autobot.Common;
using PropertyChanged;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Autobot.Model
{
    [ImplementPropertyChanged]
    public class Rule
    {
        public Rule()
        {
            Conditions = new ObservableCollection<Condition>();
            Actions = new ObservableCollection<Action>();
            AllTriggers = new ObservableCollection<Trigger>();
        }

        [Ignore]
        public ObservableCollection<Action> Actions { get; set; }

        [Ignore]
        public ObservableCollection<Condition> Conditions { get; set; }

        [Ignore]
        public ObservableCollection<Trigger> AllTriggers { get; set; }

        [Ignore]
        public Trigger Trigger
        {
            get
            {
                return AllTriggers.FirstOrDefault();
            }

            set
            {
                AllTriggers.Clear();
                AllTriggers.Add(value);
            }
        }

        #region Serializable

        public string Description { get; set; }
        public int Icon { get; set; }

        [PrimaryKey]
        public string Id { get; set; }

        public string RuleId { get; set; }
        public string Title { get; set; }

        #endregion Serializable

        public async Task Load()
        {
            await Database.Default.LoadAsync(this);
            foreach (var action in Actions)
            {
                action.Load();
            }
        }

        public async Task Run()
        {
            await Load();
            if (Conditions.All(condition => condition.IsSatisfied()))
            {
                foreach (var action in Actions)
                {
                    action.Fire();
                }
            }
        }

        public async Task SaveAsync()
        {
            Id = Guid.NewGuid().ToString();
            RuleId = Trigger.Id;

            if (Title == null)
            {
                Title = Trigger.Title + ", " + Actions?.FirstOrDefault().Title;
            }

            await Database.Default.SaveAsync(this);

            await Trigger.SaveAsync(this);
            await Task.WhenAll(Conditions.Select(condition => condition.SaveAsync(this)));
            await Task.WhenAll(Actions.Select(action => action.SaveAsync(this)));
        }

        public override string ToString()
        {
            return Title + "-" + Conditions?.Count + "-" + Actions?.Count;
        }
    }
}