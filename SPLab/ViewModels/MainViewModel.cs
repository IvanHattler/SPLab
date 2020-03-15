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
using System.Diagnostics;
using BuisnessLogic.Infrastructure;

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
        public string Denominator
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
        private string _Denominator = "4";
        public string Dividend
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
        private string _Dividend = "15";
        public string ResultOfDiv
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
        private string _ResultOfDiv = "0";
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
        public string CompilationError
        {
            get
            {
                return _CompilationError;
            }
            set
            {
                _CompilationError = value;
                OnPropertyChanged();
            }
        }
        private string _CompilationError;
        public MainViewModel()
        {
            logger = new Logger(LogMessages);
        }
        /// <summary>
        /// Метод для изменения варианта задания
        /// </summary>
        /// <param name="ind"></param>
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
                    if (isDoWhile)
                    {
                        uint denom = Convert.ToUInt32(Denominator);
                        uint div = Convert.ToUInt32(Dividend);
                        if (denom != 0)
                        {
                            ResultOfDiv = DividerUint.Divider.Div(div, denom).ToString();
                            logger.Log("Success", ResultOfDiv);
                        }
                        else
                            logger.Log("Error", "Divided by zero");
                    }
                    else
                    {
                        double denom = Convert.ToDouble(Denominator);
                        double div = Convert.ToDouble(Dividend);
                        ResultOfDiv = DividerFloat64.Divider.Div(div, denom).ToString();
                        logger.Log("Success", ResultOfDiv);
                    }
                    
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
                    if (isDoWhile)
                    {
                        DoWhileCompiler doWhileCompiler = new DoWhileCompiler(Phrase, isDoWhile);
                        try
                        {
                            bool? a = doWhileCompiler.CheckPhrase();
                            if (a == true)
                                logger.Log("Success", "More than one execution");
                            else if (a == false)
                                logger.Log("Success", "One execution");
                            else
                                logger.Log("Error");
                            CompilationError = "No compilation errors";
                        }
                        catch (CompilationException ex)
                        {
                            logger.Log(ex);
                            CompilationError = ex.Message;
                        }
                    }
                    else
                    {
                        try
                        {
                            DoWhileCompiler doWhileCompiler = new DoWhileCompiler(Phrase, isDoWhile);
                            logger.Log("", $"{doWhileCompiler.CheckCount()}");
                            CompilationError = "No compilation errors";
                        }
                        catch(CompilationException ex)
                        {
                            logger.Log(ex);
                            CompilationError = ex.Message;
                        }
                    }
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
