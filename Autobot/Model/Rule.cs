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
        private bool isEnabled;

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

        [PrimaryKey]
        public string Id { get; set; }
        public string Description { get; set; }
        public int Icon { get; set; }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; Database.Default.UpdateAsync(this); }
        }

        public string Tag { get; set; }
        public string Title { get; set; }
        public int TriggerIcon { get; set; }
        public int ConditionIcon { get; set; }
        public int ActionIcon { get; set; }

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
            Tag = Trigger.Tag;
            IsEnabled = true;

            if (Title == null)
            {
                Title = Trigger.Title + ", " + Actions?.FirstOrDefault().Title;
            }

            TriggerIcon = Trigger.Icon;
            ConditionIcon = Conditions == null || !Conditions.Any()? -1: Conditions.FirstOrDefault().Icon;
            ActionIcon = Actions == null || !Actions.Any() ? -1 : Actions.FirstOrDefault().Icon;

            await Database.Default.SaveAsync(this);
            await Trigger.SaveAsync(this);
            await Task.WhenAll(Conditions.Select(condition => condition.SaveAsync(this)));
            await Task.WhenAll(Actions.Select(action => action.SaveAsync(this)));
        }

        public async Task Delete()
        {
            await Database.Default.DeleteAsync(this);
            await Database.Default.DeleteAsync(Trigger);
            await Task.WhenAll(Conditions.Select(condition => Database.Default.DeleteAsync(condition)));
            await Task.WhenAll(Actions.Select(action => Database.Default.DeleteAsync(action)));
        }

        public override string ToString()
        {
            return Title + "-" + Conditions?.Count + "-" + Actions?.Count;
        }
    }
}