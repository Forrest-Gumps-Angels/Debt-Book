using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Debt_Book.ViewModels;

namespace Debt_Book
{
    public class MainWindowViewModel : BindableBase
    {
        ObservableCollection<Client> ClientList = new ObservableCollection<Client>();
        Client _currentClient = null;
        DebtHistory.DebtUnit _currentDebtUnit = null;
        private int _currentIndex = 0;

        public MainWindowViewModel()
        {
            ClientList.Add(new Client("Morten", -1000));
            ClientList.Add(new Client("Viktor", -200));
            ClientList.Add(new Client("Rasmus", 100000));
        }

        public Client CurrentClient
        {
            get => _currentClient;
            set => SetProperty(ref _currentClient, value);
        }

        public DebtHistory.DebtUnit CurrentDebtUnit
        {
            get => _currentDebtUnit;
            set => SetProperty(ref _currentDebtUnit, value);
        }

        public ObservableCollection<Client> ClientList_
        {
            get => ClientList;
            set => SetProperty(ref ClientList, value);
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetProperty(ref _currentIndex, value);
        }

        ICommand _addDebtor;

        public ICommand AddDebtor
        {
            get
            {
                return _addDebtor ??= new DelegateCommand(() =>
                {
                    ClientList.Add(new Client("Name", 0));
                    CurrentIndex = ClientList.Count - 1;
                });
            }
        }

        ICommand _newCommand;
        public ICommand AddNewClientCommand
        {
            get
            {
                return _newCommand ?? (_newCommand = new DelegateCommand(() =>
                {
                    var newClient = new Client();
                    var vm = new AddClientViewModel(newClient);
                    var dlg = new AddClientView
                    {
                        DataContext = vm
                    };
                    if (dlg.ShowDialog() == true)
                    {
                        ClientList.Add(newClient);
                        CurrentClient = newClient;
                    }
                }));
            }
        }

        ICommand _newDebtHistoryWindow;
        public ICommand AddNewDebtHistoryWindow
        {
            get
            {
                return _newDebtHistoryWindow ?? (_newDebtHistoryWindow = new DelegateCommand(() =>
                {
                    var newClient = new Client();
                    var vm = new AddClientViewModel(newClient);
                    var dlg = new DebtHistoryWindow
                    {
                        DataContext = vm
                    };
                    if (dlg.ShowDialog() == true)
                    {
                        ClientList.Add(newClient);
                        CurrentClient = newClient;
                    }
                }));
            }
        }
    }
}
