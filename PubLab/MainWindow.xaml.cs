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

    public partial class MainWindow : Window
    {
        private int actionCount = 1;
        private static CancellationTokenSource cancelProgramTokenSource = new CancellationTokenSource();
        public CancellationToken cancelProgramToken = cancelProgramTokenSource.Token;
        Puben pub = new Puben();
        UserPubSettings userset = new UserPubSettings();

        public MainWindow()
        {
            InitializeComponent();
            closeBarButton.Visibility = Visibility.Hidden;
            userset.BusOfGuests();
            pub.CreatePub(userset);
        }

        private void OpenBarButton_Click(object sender, RoutedEventArgs e)
        {
            openBarButton.IsEnabled = false;
            openBarButton.Visibility = Visibility.Hidden;
            closeBarButton.IsEnabled = true;
            closeBarButton.Visibility = Visibility.Visible;
            userset.PubOpenButton = false;
            userset.OpenCountdown = userset.BarOpenDuration;            
            CountdownTimer();
            GuestListBox.Items.Insert(0, "BaBar has opened");

            Bartender bartender = new Bartender();
            Waiter waiter = new Waiter();
            Bouncer bouncer = new Bouncer();
            Task.Run((Action)(() => { bouncer.BouncerActions(this.AddToLists, pub, (PubSettings)this.userset); }));
            Task.Run((Action)(() => { bartender.BartenderActions(this.AddToLists, pub, (PubSettings)this.userset); }));
            Task.Run((Action)(() => { waiter.WaiterActions(this.AddToLists, pub, (PubSettings)this.userset); }));
            Task.Run(() => { UpdateLabels(); });           
        }
        
        private void AddToLists(string action, object sender)
        {
            action = $"{actionCount++}. {action}";
            Dispatcher.Invoke(() =>
            {
                switch (sender)
                {
                    case Guest guest:
                        GuestListBox.Items.Insert(0, action);                        
                        break;
                    case Bartender bartender:
                        BartenderListBox.Items.Insert(0, action);
                        break;
                    case Waiter waiter:
                        WaiterListBox.Items.Insert(0, action);
                        break;
                    case Bouncer bouncer:
                        GuestListBox.Items.Insert(0, action);
                        break;
                }
            });
        }

        public void UpdateLabels()
        {
            while (!cancelProgramToken.IsCancellationRequested)
            {
                Dispatcher.Invoke((Action)(() =>
                {
                    labelGuestsAtBar.Content = $"Number of guests in pub: {pub.guestsInPub}";
                    labelClosingTime.Content = $"Time to closing: {this.userset.OpenCountdown}";
                    labelAvailableChairs.Content = $"Available Chairs: {pub.chairs.itemBag.Count}";
                    labelAvailableGlasses.Content = $"Number of clean glasses: {pub.cleanGlasses.itemBag.Count}";

                    if (this.userset.PubOpenButton == true)
                        {
                        openBarButton.IsEnabled = true;
                        }
                }));
                Thread.Sleep(100);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) //Stopping code to run when pressing X in top corner. Will give error msg otherwise
        {
            cancelProgramTokenSource.Cancel();
        }

        private void CloseBarButton_Click(object sender, RoutedEventArgs e) //Using time to close bar
        {
            closeBarButton.IsEnabled = false;
            closeBarButton.Visibility = Visibility.Hidden;
            userset.OpenCountdown = 1;
            openBarButton.Visibility = Visibility.Visible;
        }

        private void CountdownTimer()
        {
            Task.Run((Action)(() =>
            {
                while (this.userset.OpenCountdown > 0)
                {
                    Guest.TimeToWait(1);
                    this.userset.OpenCountdown--;
                }                
            }));            
        }
    }
}
