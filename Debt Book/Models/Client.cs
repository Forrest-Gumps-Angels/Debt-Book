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
            set 
            { 
                SetProperty(ref DebtHistory_, value);
                AccumulatedValue = accumulatevalue();
            }
        }

        public double AccumulatedValue
        {
            get
            {
                //foreach (var unit in DebtHistory.Debts)
                //{
                //    accumulatedValue += unit.Debt;
                //}

                for(int i = 0; i < DebtHistory.Debts.Count; i++)
                {
                    accumulatedValue += DebtHistory.Debts[i].Debt;
                }

                return accumulatedValue;
            }
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
