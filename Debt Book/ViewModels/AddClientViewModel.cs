using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace Debt_Book.ViewModels
{
    public class AddClientViewModel : BindableBase
    {
        public AddClientViewModel(Client client)
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
            var newClient = new Client(Name, InitialValue);
            //ClientList_.Add(newClient);
        }

        private bool SaveBtnCommand_CanExecute()
        {
            return IsValid;
        }

        #endregion
    }
}
