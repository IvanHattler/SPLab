using BuisnessLogic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SPLab.Models
{
    public class Logger
    {
        public ObservableCollection<string> Messages { get; set; }
        public Logger() { }
        public Logger(ObservableCollection<string> logMessages)
        {
            Messages = logMessages;
        }
        /// <summary>
        /// Метод для логгирования команд
        /// </summary>
        /// <param name="result">Успешное выполнение или нет</param>
        /// <param name="info">Доп информация</param>
        /// <param name="commandName"></param>
        public void Log(string result, string info = "-", [CallerMemberName]string commandName = "")
        {
            string msgForLog = $"{DateTime.Now}) - Command: {commandName}, Result: {result}, Info: {info}";
            Messages.Add(msgForLog);
        }
        /// <summary>
        /// Метод для логгирования исключений
        /// </summary>
        /// <param name="ex">Исключение</param>
        public void Log(CompilationException ex)
        {
            string msgForLog = $"{DateTime.Now}) - Exception, Info: {ex.Message}";
            Messages.Add(msgForLog);
        }
    }
}
