using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Duplicate_File_Detector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainViewModel();
            this.Loaded += (s, e) => DataContext = vm;
        }

        private void StartSearch(object sender, RoutedEventArgs e)
        {
           
            Button btn = sender as Button;
            if (vm.ButtonContent.Equals("Search"))
            {
                //Clear the Previous search list

                vm.ButtonContent = "Stop";

                vm.SearchDuplicateFile();

            }
            else
            {
                vm.StopSearching();
                vm.ButtonContent = "Search";
            }
            
        }

        private void txtFilePath_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFilePath.Text.Length == 0)
                vm.EnableSearchBtn = false;
            else
                vm.EnableSearchBtn = true;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            string filename = (sender as ListView).SelectedItem.ToString();
            MessageBoxResult result = MessageBox.Show("Do you want to open this file " + filename, "Open File", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                // file will not open
            }
            else
            {

                System.Diagnostics.Process.Start("explorer.exe", $@"/select, {filename}");
            }
            
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        //Important
        private void Notify(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        // File Path
        private string _folderName;
        public string FolderName
        {
            get { return _folderName; }
            set
            {
                _folderName = value;
                Notify("FolderName");
            }
        }


        private string _buttonContent;
        public string ButtonContent
        {
            get { return _buttonContent; }
            set
            {
                _buttonContent = value;
                Notify("ButtonContent");
            }
        }


        private ObservableCollection<DataItem> _listOfDupFile;
        public ObservableCollection<DataItem> ListOfDupFile
        {
            get { 
                return _listOfDupFile; }
            set
            {
                _listOfDupFile = value;
                Notify("ListOfDupFile");
            }
        }

        public ObservableCollection<DataItem> ListOfDupFile1
        {
            get
            {
                return _listOfDupFile;
            }
            set
            {
                Notify("ListOfDupFile");
            }
        }



        private bool _enableSearchBtn;
        public bool EnableSearchBtn
        {
            get { return _enableSearchBtn; }
            set
            {
                _enableSearchBtn = value;
                Notify("EnableSearchBtn");
            }
        }


        public Dispatcher _current;
        public Helper hObj;
        public Thread thread;
        public Dictionary<string,string> CheckSumList;

        public MainViewModel()
        {
            _current = Dispatcher.CurrentDispatcher;
            hObj = new Helper();
            ListOfDupFile = new ObservableCollection<DataItem>();
            CheckSumList = new Dictionary<string, string>();
            FolderName = "";
            ButtonContent = "Search";
            EnableSearchBtn = false;
        }
        

        public void SearchDuplicateFile()
        {
            ListOfDupFile.Clear();
            CheckSumList.Clear();
            DirectoryInfo di = new DirectoryInfo($@"{FolderName}");
            if (di.Exists)
            {

                _current.BeginInvoke(new Action(() =>
                {
                    Search($@"{FolderName}");
                }));
            }
            else
            {
                MessageBox.Show("No Suck File Exist");
            }

            ButtonContent = "Search";
        }

        public void Search(string FName)
        {
            DirectoryInfo di = new DirectoryInfo($@"{FName}");
            try
            {
                foreach (var directory in di.GetDirectories())
                {
                    if (!di.Name.Equals("$RECYCLE.BIN"))
                        Search(directory.FullName);
                }

                foreach (var file in di.GetFiles())
                {

                    string checksm = md5Checksum(file.FullName);
                    
                    if(CheckSumList.ContainsKey(checksm))
                    {
                        string fileName;
                        CheckSumList.TryGetValue(checksm, out fileName);
                        AddToDuplicateList(checksm,file.FullName , fileName);
                    }
                    else
                    {
                        // Add to list
                        CheckSumList.Add(checksm, file.FullName);
                    }
                    
                }

            }
            catch (UnauthorizedAccessException)
            { }
            catch (ThreadAbortException)
            { }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "The process failed");

            }
            finally { }
        }

        public void AddToDuplicateList(string checkSum , string current_fileName, string previous_fileName )
        {
            _current.BeginInvoke(new Action(() =>
            {
                DataItem dataItem = null;
                foreach (var item in ListOfDupFile)
                {
                    if(item.CheckSum.Equals(checkSum))
                    {
                        dataItem = item;
                        break;
                    }
                }

                if(dataItem == null)
                {
                    dataItem = new DataItem();
                    dataItem.CheckSum = checkSum;
                    dataItem.FilesPath.Add(previous_fileName);
                    dataItem.FilesPath.Add(current_fileName);
                    ListOfDupFile.Add(dataItem);
                }
                else
                {

                    dataItem.FilesPath.Add(current_fileName);
                    ListOfDupFile.Count();
                    Notify("ListOfDupFile");
                }
            }));
        }

        public void StopSearching()
        {
            //Stop the Thread
            if (thread.IsAlive)
                thread.Abort();
        }

        private string md5Checksum(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", String.Empty);
                }
            }
        }
    }

}

