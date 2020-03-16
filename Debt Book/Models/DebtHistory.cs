using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Debt_Book
{
    public class DebtHistory : INotifyPropertyChanged
    {
        List<DebtUnit> debts_ = new List<DebtUnit>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void notify([CallerMemberName]string propname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        public List<DebtUnit> Debts
        {
            get => debts_;
            set { debts_ = value; notify(); }
        }

        public class DebtUnit : INotifyPropertyChanged
        {
            DateTime date_;
            double debt_;

            public event PropertyChangedEventHandler PropertyChanged;
            protected void notify([CallerMemberName]string propname = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
            }

            public DebtUnit(double value, DateTime date)
            {
                Date = date;
                Debt = value;
            }

            public DateTime Date 
            {
                get => date_;
                set { date_ = value; notify(); }
            }

            public double Debt
            {
                get => debt_;
                set { debt_ = value; notify(); }
            }
        }
    }
}
