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
        public void Log(string result, string info = "-", [CallerMemberName]string commandName = "")
        {
            string msgForLog = $"{DateTime.Now}) - Command: {commandName}, Result: {result}, Info: {info}";
            Messages.Add(msgForLog);
        }
    }
}
