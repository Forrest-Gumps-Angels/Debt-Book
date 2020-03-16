using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Debt_Book
{
    public class Client : INotifyPropertyChanged
    {
        string name;
        double initialValue;
        double accumulatedDebt;
        DebtHistory DebtHistory_;


        public event PropertyChangedEventHandler PropertyChanged;

        protected void notify([CallerMemberName]string propname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        public Client()
        { }

        public Client(string _name, double _initialValue)
        {
            name = _name;
            initialValue = _initialValue;
        }

        public string Name
        {
            get => name;
            set { name = value; notify(); }
        }

        public double InitialValue
        {
            get => initialValue;
            set { initialValue = value; notify(); }
        }

        public DebtHistory DebtHistory
        {
            get => DebtHistory_;
            set { DebtHistory_ = value; notify(); }
        }
    }
}
