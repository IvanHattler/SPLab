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
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; OnPropertyChanged(); }
        }

        #region Commands
        private DelegateCommand myCommand;
        public DelegateCommand My
        {
            get
            {
                return myCommand ?? (myCommand = new DelegateCommand(obj =>
                {

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
