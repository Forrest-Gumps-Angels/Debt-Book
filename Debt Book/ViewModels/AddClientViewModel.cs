using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using Debt_Book.ViewModels;
using System.Windows;
using System.Linq;

namespace Debt_Book.ViewModels
{
    public class AddClientViewModel : BindableBase
    {
        public AddClientViewModel()
        { }

        #region Properties

        string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
            }
        }

        double initialValue;
        public double InitialValue
        {
            get { return initialValue; }
            set
            {
                SetProperty(ref initialValue, value);
            }
        }

        public bool IsValid
        {
            get
            {
                bool isValid = true;
                if (string.IsNullOrWhiteSpace(Name))
                    isValid = false;
                if (double.IsNaN(InitialValue))
                    isValid = false;
                return isValid;
            }
        }
        #endregion

        #region Commands

        ICommand _saveBtnCommand;
        public ICommand SaveBtnCommand
        {
            get
            {
                return _saveBtnCommand ?? (_saveBtnCommand = new DelegateCommand(
                    SaveBtnCommand_Execute, SaveBtnCommand_CanExecute)
                  .ObservesProperty(() => Name)
                  .ObservesProperty(() => InitialValue));
            }
        }

        public void SaveBtnCommand_Execute() 
        {
            Client newClient;
            newClient = new Client(Name, InitialValue);
            MainWindow mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            MainWindowViewModel mvvm = (MainWindowViewModel)mw.DataContext;
            mvvm.ClientList_.Add(newClient);
        }

        private bool SaveBtnCommand_CanExecute()
        {
            return IsValid;
        }

        #endregion
    }
}
