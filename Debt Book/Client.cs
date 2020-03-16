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
        double accumulatedValue;
        DebtHistory DebtHistory_ = new DebtHistory();


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

            DebtHistory_.Debts.Add(new DebtHistory.DebtUnit(_initialValue, DateTime.Now));
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

        public double AccumulatedValue
        {
            get
            {
                foreach (var unit in DebtHistory.Debts)
                {
                    accumulatedValue += unit.Debt;
                }

                return accumulatedValue;
            }
            set {  }
        }
    }
}
