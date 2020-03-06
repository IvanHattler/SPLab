using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using SPLab.Commands;

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
        #region Commands
        private DelegateCommand myCommand;
        public DelegateCommand My
        {
            get
            {
                return myCommand ?? (myCommand = new DelegateCommand(obj =>
                {
                    ResultOfDiv = LLArithmetic.Divider.Div(Dividend, Denominator);
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
