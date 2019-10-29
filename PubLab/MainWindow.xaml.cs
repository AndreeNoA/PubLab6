using PubLab.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PubLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int actionCount = 1;
        static Random random = new Random();
        private static CancellationTokenSource cancelProgramTokenSource = new CancellationTokenSource();
        public CancellationToken cancelProgramToken = cancelProgramTokenSource.Token;
        public static int timeToClose { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            closeBarButton.Visibility = Visibility.Hidden;
        }

        private void OpenBarButton_Click(object sender, RoutedEventArgs e)
        {
            openBarButton.IsEnabled = false;
            openBarButton.Visibility = Visibility.Hidden;
            closeBarButton.IsEnabled = true;
            closeBarButton.Visibility = Visibility.Visible;
            CountdownTimer();
            CustomersListBox.Items.Insert(0, "Bar has opened");
            Task.Run(() => { Bouncer(AddToLists); });
            Task.Run(() => { UpdateLabels(); });
        }
        
        public void Bouncer(Action<string, object> logText)
        {            
            while (timeToClose > 0)
            {
                Guest.TimeToWait(random.Next(1, 5));
                CreateGuest(logText);               
            }
        }
        private void AddToLists(string action, object sender)
        {
            action = $"{actionCount++}. {action}";
            Dispatcher.Invoke(() =>
            {
                switch (sender)
                {
                    case Guest _:
                        CustomersListBox.Items.Insert(0, action);                        
                        break;
                    //case Bartender _:
                    //    BartenderListBox.Items.Insert(0, action);
                    //    break;
                    //case Waitress _:
                    //    WaiterListBox.Items.Insert(0, action);
                    //    break;
                    //case Bouncer _:
                    //    GuestListBox.Items.Insert(0, action);
                    //    break;
                }
            });
        }
        public void CreateGuest(Action<string, object> logText)
        {
            Task.Run(() =>
            {
                Guest guest = new Guest();          
                guest.GuestActions(logText);
            });
        }

        public void UpdateLabels()
        {
            while (!cancelProgramToken.IsCancellationRequested)
            {
                Dispatcher.Invoke(() =>
                {
                    labelGuestsAtBar.Content = $"Number of guests in pub: {Guest.guestsInPub}";
                    labelClosingTime.Content = $"Time to closing: {timeToClose}";
                });
                Thread.Sleep(10);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) //Stopping code to run when pressing X in top corner. Will give error msg otherwise
        {
            cancelProgramTokenSource.Cancel();
        }

        private void CloseBarButton_Click(object sender, RoutedEventArgs e) //Using time to close bar instead of cancellationToken, so that number of guests still count down when closing the bar with button
        {
            closeBarButton.IsEnabled = false;
            closeBarButton.Visibility = Visibility.Hidden;
            timeToClose = 1;
            openBarButton.IsEnabled = true;
            openBarButton.Visibility = Visibility.Visible;
        }

        private void CountdownTimer()
        {
            timeToClose = 120;
            Task.Run(() =>
            {
                while (timeToClose > 0)
                {
                    Guest.TimeToWait(1);
                    timeToClose--;
                }
            });

        }
    }
}
