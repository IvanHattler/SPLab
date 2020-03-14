using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using SPLab.Commands;
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
        private bool isDoWhile = true;
        public ObservableCollection<string> Variants
        {
            get
            {
                return _Variants;
            }
            set
            {
                _Variants = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _Variants = new ObservableCollection<string>()
        { "Variant 1", "Variant 2"};
        public int SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                _SelectedIndex = value;
                OnPropertyChanged();
                ChangeVariant(value);
            }
        }
        private int _SelectedIndex = 0;
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
    i = i+1;
} 
while (i<5);"; 
        public MainViewModel()
        {
            logger = new Logger(LogMessages);
        }
        private void ChangeVariant(int ind)
        {
            switch (ind)
            {
                case 0:
                    Phrase = @"int i = 0;
do 
{
    i = i+1;
} 
while (i<5);";
                    isDoWhile = true;
                    break;
                case 1:
                    Phrase = @"int i = 0;
while (i<5) 
{
    i = i+1;
}; ";
                    isDoWhile = false;
                    break;
                default:
                    break;
            }
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
                        ResultOfDiv = DividerUint.Divider.Div(Dividend, Denominator);
                        logger.Log("Success",$"{ResultOfDiv}");
                    }
                    else
                        logger.Log("Error","Divided by zero");
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
                    //DoWhileAnalyzer doWhileAnalizer = new DoWhileAnalyzer(Phrase);
                    //bool? a = doWhileAnalizer.IsExecuteMoreOne;
                    DoWhileCompiler doWhileCompiler = new DoWhileCompiler(Phrase,isDoWhile);
                    logger.Log("", $"{doWhileCompiler.CheckCount()}");
                    //bool? a = doWhileCompiler.IsExecuteMoreOne;
                    //if (a == true)
                    //    logger.Log("Success","More than one execution");
                    //else if(a == false)
                    //    logger.Log("Success", "One execution");
                    //else
                    //    logger.Log("Error");
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
