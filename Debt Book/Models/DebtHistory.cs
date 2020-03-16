using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Prism.Commands;
using Prism.Mvvm;

namespace Debt_Book
{
    public class DebtHistoryClass : BindableBase
    {
        ObservableCollection<DebtUnit> debts_ = new ObservableCollection<DebtUnit>();

        


        public ObservableCollection<DebtUnit> Debts
        {
            get => debts_;
            set {
                SetProperty(ref debts_, value);
            }
        }

        public class DebtUnit : BindableBase
        {
            DateTime date_;
            double debt_;

            public event EventHandler DebtsChanged;

            public DebtUnit(double value, DateTime date)
            {
                Date = date;
                Debt = value;
            }
        
            public DebtUnit()
            { }

            public DateTime Date 
            {
                get => date_;
                set => SetProperty(ref date_, value);
            }

            public double Debt
            {
                get => debt_;
                set
                {
                    SetProperty(ref debt_, value);
                    notifyDebtChanged();
                }
                
            }

            public virtual void notifyDebtChanged()
            {
                DebtsChanged?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}
