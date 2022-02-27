using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotDispatcher.Handlers
{
    public class UserHandlers
    {
        public List<HandlerInfo> handlers { get; set; }

        public Handler defaultHandler { get; set; }

        public UserHandlers(Handler defaulHandler)
        {
            this.defaultHandler = defaulHandler;
            handlers = new List<HandlerInfo>();
        }
        public Handler GetHandler(long user_id)
        {
            HandlerInfo? handler_info = handlers.FirstOrDefault(x => x.user_id == user_id);
            if (handler_info == null) handlers.Add(new HandlerInfo(user_id, defaultHandler));
            return handler_info?.handler ?? defaultHandler;
        }

        public void SetHandler(long user_id, Handler operation)
        {
            HandlerInfo? handler_info = handlers.FirstOrDefault(x => x.user_id == user_id);
            if (handler_info != null)
                handler_info.handler = operation;
            else
                handlers.Add(new HandlerInfo(user_id, operation));
        }
    }

    public class HandlerInfo
    {
        public long user_id;
        public Handler handler;

        public HandlerInfo(long user_id, Handler handler)
        {
            this.user_id = user_id;
            this.handler = handler;

        }
    }
}