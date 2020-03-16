using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Debt_Book.ViewModels
{
    public class AddDeptorViewModel : BindableBase
    {
        public AddDeptorViewModel(Client client)
        {
            CurrentClient = client;
        } 

        #region Properties

        Client currentClient;

        public Client CurrentClient
        {
            get { return currentClient; }
            set { SetProperty(ref currentClient, value); }
        }

        //bool isValid;

        public bool IsValid
        {
            get
            {
                bool isValid = true;
                if (string.IsNullOrWhiteSpace(CurrentClient.Name))
                    isValid = false;
                return isValid;
            }
            //set
            //{
            //    SetProperty(ref isValid, value);
            //}
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
                  .ObservesProperty(() => CurrentClient.Name)
                  .ObservesProperty(() => CurrentClient.InitialValue));
            }
        }

        private void SaveBtnCommand_Execute() {}

        private bool SaveBtnCommand_CanExecute()
        {
            return IsValid;
        }

        #endregion
    }
}
