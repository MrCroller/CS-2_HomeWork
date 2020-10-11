using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_2_HomeWork.Physics
{
    internal static class Logger
    {
        /// <summary>
        /// Имя файла лога
        /// </summary>
        public static string FileName { get; set; }

        /// <summary>
        /// Очередь записей
        /// </summary>
        public static Task Task { get; set; }

        /// <summary>
        /// Папка для файлов логов
        /// </summary>
        public static string LogFolder { get; set; }

        /// <summary>
        /// Коллекция записей
        /// </summary>
        public static BlockingCollection<string> BlockingCollection { get; set; }

        /// <summary>
        /// Статический конструктор логера
        /// </summary>
        static Logger()
        {
            LogFolder = Path.Combine(Environment.CurrentDirectory, "../../Logs");
            // Проверка на существвование каталога
            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, LogFolder)))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, LogFolder));
            }

            // Задает имя файлу в соответствии с текущей датой
            DateTime date = DateTime.Now.Date;
            FileName = date.Date.ToString("yy.MM.dd") + "_log.txt";

            BlockingCollection = new BlockingCollection<string>();

            Task = Task.Factory.StartNew(() =>
            {
                using (var streamWriter = new StreamWriter(Path.Combine(Environment.CurrentDirectory, LogFolder, FileName), true, Encoding.UTF8))
                {
                    streamWriter.AutoFlush = true;

                    foreach (var s in BlockingCollection.GetConsumingEnumerable())
                        streamWriter.WriteLine(s);
                }
            },
            TaskCreationOptions.LongRunning);
            BlockingCollection.Add("\n*************************************************************************************\n");
        }

        /// <summary>
        /// Формирует строку для записи в лог
        /// </summary>
        /// <param name="message"></param>
        public static void LogWriter(string message)
        {
            BlockingCollection.Add($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff")} действие: {message} ");
        }

        /// <summary>
        /// Задерживает закрытие до завершения всех записей в лог
        /// </summary>
        public static void Flush()
        {
            BlockingCollection.CompleteAdding();
            Task.Wait();
        }
    }
}

