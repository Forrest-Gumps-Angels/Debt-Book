using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Debt_Book
{
    public class DebtHistory : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void notify([CallerMemberName]string propname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}
