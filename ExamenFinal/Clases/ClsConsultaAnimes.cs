using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExamenFinal.Clases
{
    class ClsConsultaAnimes
    {
        private static TelegramBotClient Bot;
        public async Task IniciarTelegram()
        {
            Bot = new TelegramBotClient("1762722712:AAFHk0nrD9xh-l_awUyucM8JuHOkosgt9C4");

            var me = await Bot.GetMeAsync();
            Console.Title = me.Username;

            Bot.OnMessage += BotCuandoRecibeMensajes;
            Bot.OnMessageEdited += BotCuandoRecibeMensajes;
            Bot.OnReceiveError += BotOnReceiveError;
            //Método que se ejecuta cuando se recibe un callbackQuery
            //Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"escuchando solicitudes del BOT @{me.Username}");
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static async void BotCuandoRecibeMensajes(object sender, MessageEventArgs messageEventArgumentos)
        {
            var ObjetoMensajeTelegram = messageEventArgumentos;
            var mensajes = ObjetoMensajeTelegram.Message;
            string mensajeEntrante = mensajes.Text;

            string respuesta = "..";
            string peticion = "Ingrese el nombre del anime que desea buscar";
            Console.WriteLine($"Recibiendo Mensaje del chat {ObjetoMensajeTelegram.Message.Chat.Id}.");
            Console.WriteLine($"Dice {ObjetoMensajeTelegram.Message.Text}.");
            string sql = $"SELECT * FROM animes_disponibles WHERE nombre LIKE '%{mensajeEntrante}%'";

            await Bot.SendTextMessageAsync(
                chatId: messageEventArgumentos.Message.Chat.Id,
                text: peticion);
            ClsConexionSQL conector = new ClsConexionSQL();
            DataTable dt = conector.consultaTablaDirecta(sql);
            foreach (DataRow dr in dt.Rows)
            {
                respuesta = "\n-Link: " + dr["link"].ToString() + "\n-Nombre: " + dr["nombre"].ToString() + "\n-Autor: " + dr["autor"].ToString() + "\n-Año: " + dr["año"].ToString() + "\n-Estado: " + dr["estado"].ToString();
            }
            await Bot.SendTextMessageAsync(
                chatId: messageEventArgumentos.Message.Chat.Id,
                text: respuesta);
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message
            );
        }
    }
}
