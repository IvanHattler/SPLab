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

namespace SPLab.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
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
                    DoWhileAnalizer doWhileAnalizer = new DoWhileAnalizer(Phrase);
                    bool? a = doWhileAnalizer.IsExecute;
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
