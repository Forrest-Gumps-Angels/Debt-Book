using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        DebtHistoryClass DebtHistory_ = new DebtHistoryClass();

        public Client()
        { }

        public Client(string _name, double _initialValue)
        {
            Name = _name;
            DebtHistory_.Debts.Add(new DebtHistoryClass.DebtUnit(_initialValue, DateTime.Now));
            DebtHistory.Debts.CollectionChanged += handlepropchange;
            foreach(var debt in DebtHistory.Debts)
            {
                debt.DebtsChanged += handlepropchange_;
            }
        }

        private void handlepropchange_(object sender, EventArgs e)
        {
            AccumulatedValue = accumulatevalue();
        }

        private void handlepropchange(object sender, NotifyCollectionChangedEventArgs e)
        {
            AccumulatedValue = accumulatevalue();
            foreach (DebtHistoryClass.DebtUnit debt in e.NewItems)
            {
                debt.DebtsChanged += handlepropchange_;
            }
        }




        public string Name
        {
            get => name;
            set { SetProperty(ref name, value); }
        }

        public DebtHistoryClass DebtHistory
        {
            get => DebtHistory_;
            set 
            { 
                SetProperty(ref DebtHistory_, value);
            }
        }

        public double AccumulatedValue
        {
            get => accumulatevalue();
            set 
            {
                SetProperty(ref accumulatedValue, value);
            }
        }
        
        public double accumulatevalue()
        {
            double x = 0;

            for (int i = 0; i < DebtHistory.Debts.Count; i++)
            {
                x += DebtHistory.Debts[i].Debt;
            }

            return x;
        }
    }
}
