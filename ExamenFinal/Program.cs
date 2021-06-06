using ExamenFinal.Clases;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExamenFinal
{
    class Program
    {
        private static TelegramBotClient Bot;
        public static async Task Main()
        {
            Console.WriteLine("¿Qué deseas buscar? 1-Apps  2-animes");
            string r = Console.ReadLine();
            if (r == "apps")
            {
                await new ClsConsultaApps().IniciarTelegramApps();
            }
            else
            {
                if (r == "animes")
                {
                    await new ClsConsultaAnimes().IniciarTelegram();
                }
            }

        }      
    }
}
