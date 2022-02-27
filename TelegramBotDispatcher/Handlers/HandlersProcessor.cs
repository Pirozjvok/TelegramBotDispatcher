using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotDispatcher.Filters;
using TelegramBotDispatcher.FSM;

namespace TelegramBotDispatcher.Handlers
{
    public class HandlersProcessor
    {
        public List<FilterableHandler> handlers { get; }
        public Handler? OnNotFound { get; set; }
        public HandlersProcessor(Handler? OnNotFound = null)
        {
            handlers = new List<FilterableHandler>();
            this.OnNotFound = OnNotFound;
        }

        public async Task Execute(ITelegramBotClient client, Update update, State state, Data data)
        {
            string stateName = await state.GetState();
            IEnumerable<FilterableHandler> operations = handlers.Where(x => x.Test(update, stateName)).OrderByDescending(x => x.priority);
            bool test = false;
            foreach (FilterableHandler operation in operations)
            {
                test = true;
                await operation.Execute(client, update, state, data);
                if (!operation.chain) break;
            }
            if (!test && OnNotFound != null) await OnNotFound.Execute(client, update, state, data);
        }
        public void Register(Handler handler, string states, bool chain = true, int priority = 0)
        {
            handlers.Add(new FilterableHandler(handler, new CustomUpdateFilter(x => true)) { States = states, chain = chain, priority = priority });
        }
        public void Register(Handler handler, IFilter<Update> filter, string states = "*", bool chain = true, int priority = 0)
        {
            handlers.Add(new FilterableHandler(handler, filter) { States = states, chain = chain, priority = priority });
        }
        public void Register(Handler handler, Func<string, bool> filter, string states = "*", bool chain = true, int priority = 0)
        {
            handlers.Add(new FilterableHandler(handler, filter) { States = states, chain = chain, priority = priority });
        }
        public void Register(Handler handler, Func<Update, bool> filter, string states = "*", bool chain = true, int priority = 0)
        { 
            handlers.Add(new FilterableHandler(handler, filter) { States = states, chain = chain, priority = priority });
        }

    }

    public class FilterableHandler : Handler
    {
        public Handler BaseHandler { get; }
        public IFilter<Update> UpdateFilter { get; }
        public string States { get; set; } = "*";

        public bool chain = true;

        public int priority = 0;
        public FilterableHandler(Handler BaseHandler, IFilter<Update> Filter)
        {
            this.BaseHandler = BaseHandler;
            this.UpdateFilter = Filter;
        }
        public FilterableHandler(Handler BaseHandler, Func<string, bool> filter)
        {
            this.BaseHandler = BaseHandler;
            this.UpdateFilter = new CustomTextFilter(filter);
        }

        public FilterableHandler(Handler BaseHandler, Func<Update, bool> filter)
        {
            this.BaseHandler = BaseHandler;
            this.UpdateFilter = new CustomUpdateFilter(filter);
        }

        public bool Test(Update update, string state)
        {
            return StateTester(States, state) && UpdateFilter.Test(update);
        }

        public override async Task Execute(ITelegramBotClient client, Update update, State state, Data data)
        {
            await BaseHandler.Execute(client, update, state, data);
        }

        private bool StateTester(string states, string test_state)
        {
            return StateTester(states.Split(','), test_state);
        }
        private bool StateTester(string[] states, string test_state)
        {
            if (states.Length == 0)
            {
                return false;
            }
            foreach (var permission in states)
            {
                string[] user_perms = permission.Split('.');
                string[] cmd_perms = test_state.Split('.');
                int max = Math.Min(cmd_perms.Length, user_perms.Length);

                bool access = true;
                for (int i = 0; i < max; i++)
                {
                    if (!(user_perms[i] == "*" || user_perms[i].Equals(cmd_perms[i], StringComparison.OrdinalIgnoreCase)))
                    {
                        access = false;
                        break;
                    }
                }
                if (access) return true;
            }
            return false;
        }
    }
   
}
    