using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace ExamenFinal.Clases
{
    class ClsConsultaApps
    {
        private static TelegramBotClient Bot;
        public async Task IniciarTelegramApps()
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

            string respuesta = "°";
            string peticion = "Ingrese el nombre de la app que desea buscar";
            Console.WriteLine($"Recibiendo Mensaje del chat {ObjetoMensajeTelegram.Message.Chat.Id}.");
            Console.WriteLine($"Dice {ObjetoMensajeTelegram.Message.Text}.");

            await Bot.SendTextMessageAsync(
                chatId: messageEventArgumentos.Message.Chat.Id,
                text: "Aplicaciones abierto :)");

            string sql = $"SELECT * FROM aplicaciones_crack WHERE nombre LIKE '%{mensajeEntrante}%'";

            await Bot.SendTextMessageAsync(
                chatId: messageEventArgumentos.Message.Chat.Id,
                text: peticion);
            ClsConexionSQL conector = new ClsConexionSQL();
            DataTable dt = conector.consultaTablaDirecta(sql);
            foreach (DataRow dr in dt.Rows)
            {
                respuesta = "\n-Link: " + dr["link"].ToString() + "\n-Nombre: " + dr["nombre"].ToString() + "\n-Descripción: " + dr["descripcion"].ToString() + "\n-Tamaño: " + dr["tamaño"].ToString() + "\n-Servidor: " + dr["servidor"].ToString();
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
