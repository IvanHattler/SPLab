using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using SPLab.Commands;
using LLArithmetic;
using BuisnessLogic;
using SPLab.Models;

namespace SPLab.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> LogMessages
        {
            get
            {
                return _LogMessages;
            }
            set
            {
                _LogMessages = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _LogMessages = new ObservableCollection<string>();
        private Logger logger;
        public uint Denominator
        {
            get
            {
                return _Denominator;
            }
            set
            {
                _Denominator = value;
                OnPropertyChanged();
            }
        }
        private uint _Denominator = 4;
        public uint Dividend
        {
            get
            {
                return _Dividend;
            }
            set
            {
                _Dividend = value;
                OnPropertyChanged();
            }
        }
        private uint _Dividend = 15;
        public uint ResultOfDiv
        {
            get
            {
                return _ResultOfDiv;
            }
            set
            {
                _ResultOfDiv = value;
                OnPropertyChanged();
            }
        }
        private uint _ResultOfDiv = 0;
        public string Phrase
        {
            get
            {
                return _Phrase;
            }
            set
            {
                _Phrase = value;
                OnPropertyChanged();
            }
        }
        private string _Phrase = @"int i = 0;
do 
{
    i++;j++;a = a-- - --a;
} 
while (i<5);";
        public MainViewModel()
        {
            logger = new Logger(LogMessages);
        }
        #region Commands
        private DelegateCommand divCommand;
        public DelegateCommand Div
        {
            get
            {
                return divCommand ?? (divCommand = new DelegateCommand(obj =>
                {
                    if (Denominator != 0)
                    {
                        ResultOfDiv = Divider.Div(Dividend, Denominator);
                        logger.Log("Sucsess");
                    }
                    else
                        logger.Log("Error");
                }));
            }
        }
        private DelegateCommand checkCommand;
        public DelegateCommand Check
        {
            get
            {
                return checkCommand ?? (checkCommand = new DelegateCommand(obj =>
                {
                    DoWhileAnalizer doWhileAnalizer = new DoWhileAnalizer(Phrase);
                    bool? a = doWhileAnalizer.IsExecute;
                    if (a == true)
                        logger.Log("Sucsess");
                    else
                        logger.Log("Error");
                }));
            }
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
