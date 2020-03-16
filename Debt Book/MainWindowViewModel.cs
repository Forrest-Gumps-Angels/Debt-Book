using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Enumeration;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Serialization;
using Prism.Commands;
using Debt_Book.ViewModels;
using Microsoft.Win32;

namespace Debt_Book
{
    public class MainWindowViewModel : BindableBase
    {
        ObservableCollection<Client> ClientList = new ObservableCollection<Client>();
        Client _currentClient = null;
        DebtHistory.DebtUnit _currentDebtUnit = null;
        private int _currentIndex = 0;
        string availableFileTypes = "xml files (*.xml)|*.xml";
        string _filename;

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

        public string FileName
        {
            get => _filename;
            set => SetProperty(ref _filename, value);
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

        ICommand _saveAs;

        public ICommand SaveAs
        {
            get
            {
                return _saveAs ?? (_saveAs = new DelegateCommand(() => { executeSaveAs(); }));
            }
        }

        public void executeSaveAs()
        {
            XmlSerializer XML_serial = new XmlSerializer(typeof(ObservableCollection<Client>));
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = availableFileTypes;
            saveFileDialog.ShowDialog();
            FileName = saveFileDialog.FileName;

            TextWriter writer = new StreamWriter(FileName);

            XML_serial.Serialize(writer, ClientList);
            writer.Close();
        }

        ICommand _open;

        public ICommand Open
        {
            get
            {
                return _open ??  (_open = new DelegateCommand(() =>
                {
                    executeOpen();

                }));
            }
        }

        public void executeOpen()
        {
            XmlSerializer XML_serial = new XmlSerializer(typeof(ObservableCollection<Client>));
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = availableFileTypes;
            FileName = openFileDialog.FileName;
            FileStream fs = new FileStream(FileName, FileMode.Open);
            ClientList_ = (ObservableCollection<Client>)XML_serial.Deserialize(fs);
            fs.Close();
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
