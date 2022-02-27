using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Text;
using TelegramBotDispatcher.Handlers;
using TelegramBotDispatcher.Filters;
using TelegramBotDispatcher;
using TelegramBotDispatcher.FSM;
using TelegramBotExample.Handlers;

StateStorage statesStorage = new MemoryStateStorage(); //Хранилище состояний
DataStorage dataStorage = new MemoryDataStorage(); //Хранилице данных пользователя
HandlersProcessor processor = new HandlersProcessor();

//Обработчик SetState просто устанавливает нужное состояние и отправляет сообщение.
//Состояние "cancel.*" означает что это обработчик будет выполнятся в любои состоянии которое начинается с cancel.
//Chain : false означает что после этого обработчика другие обработчики выполнятся не будут т.к за раз могут выполнятся несколько обработчиков
//Priority это просто приоритет: по умолчанию он равен нулю.
processor.Register(new SetState("default", "Операция отменена!"), new CommandFilter("cancel"), "cancel.*", chain: false, priority: 1); 
//дДобавляем остальные обработчики
processor.Register(new Help(), new CommandFilter("help", "start"), "default");
processor.Register(new StateTest(), new CommandFilter("go"), "default");
processor.Register(new StateTest2(), "cancel.waitname");
processor.Register(new StateTest3(), "cancel.wait2");
processor.Register(new Result(), "cancel.wait3");


string token = "";

TelegramBotClient client = new TelegramBotClient(token);

var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = new UpdateType[] { UpdateType.Message } //Сделаем так чтобы бот принимал только сообщения
};

client.StartReceiving(OnMessageAsync, OnErrorAsync, receiverOptions, cts.Token);
Console.WriteLine("Бот вышел в онлайн");
Console.ReadLine();
cts.Cancel();

async Task OnMessageAsync(ITelegramBotClient client, Update update, CancellationToken token)
{
    if (update.Message?.From == null) return;
    try
    {
        await processor.Execute(client, update, new State(statesStorage, update.Message?.From?.Id ?? 0), new Data(dataStorage, update.Message?.From?.Id ?? 0));
    }
    catch
    {
        
    }
}

Task OnErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
{

    return Task.CompletedTask;
}