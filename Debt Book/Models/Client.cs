using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;

namespace Debt_Book
{
    public class Client : BindableBase
    {
        string name;
        double accumulatedValue;
        DebtHistory DebtHistory_ = new DebtHistory();

        public Client()
        { }

        public Client(string _name, double _initialValue)
        {
            Name = _name;
            DebtHistory_.Debts.Add(new DebtHistory.DebtUnit(_initialValue, DateTime.Now));
        }

        public string Name
        {
            get => name;
            set { SetProperty(ref name, value); }
        }

        public DebtHistory DebtHistory
        {
            get => DebtHistory_;
            set { SetProperty(ref DebtHistory_, value); }
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
