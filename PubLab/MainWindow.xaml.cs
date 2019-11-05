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
        UserPubSettings userPubSettings = new UserPubSettings();

        public MainWindow()
        {
            InitializeComponent();
            openBarButton.IsEnabled = false;
            closeBarButton.Visibility = Visibility.Hidden;
        }

        private void OpenBarButton_Click(object sender, RoutedEventArgs e)
        {
            pub.CreatePub(userPubSettings);
            CountdownTimer();
            openBarButton.IsEnabled = false;
            openBarButton.Visibility = Visibility.Hidden;
            closeBarButton.IsEnabled = true;
            closeBarButton.Visibility = Visibility.Visible;
            simulationChoiceMenu.Visibility = Visibility.Hidden;
            simulationSettingsLabel.Visibility = Visibility.Hidden;
            WaiterListBox.Items.Clear();
            BartenderListBox.Items.Clear();
            GuestListBox.Items.Clear();
            actionCount = 0;
            GuestListBox.Items.Insert(0, "BaBar has opened");
            userPubSettings.PubOpenButton = false;
            userPubSettings.OpenCountdown = userPubSettings.BarOpenDuration;            

            Bartender bartender = new Bartender();
            Waiter waiter = new Waiter();
            Bouncer bouncer = new Bouncer();
            Task.Run(() => { bouncer.BouncerActions(this.AddToLists, pub, (PubSettings)this.userPubSettings); });
            Task.Run(() => { bartender.BartenderActions(this.AddToLists, pub, (PubSettings)this.userPubSettings); });
            Task.Run(() => { waiter.WaiterActions(this.AddToLists, pub, (PubSettings)this.userPubSettings); });
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
                    labelClosingTime.Content = $"Time to closing: {this.userPubSettings.OpenCountdown}";
                    labelAvailableChairs.Content = $"Available Chairs: {pub.chairs.itemBag.Count}";
                    labelAvailableGlasses.Content = $"Number of clean glasses: {pub.cleanGlasses.itemBag.Count}";

                    if (this.userPubSettings.PubOpenButton == true)
                        {
                        openBarButton.IsEnabled = true;
                        }
                    if (userPubSettings.OpenCountdown == 0 && openBarButton.IsEnabled == true)
                    {
                        simulationChoiceMenu.Visibility = Visibility.Visible;
                        simulationSettingsLabel.Visibility = Visibility.Visible;
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
            userPubSettings.OpenCountdown = 1;
            openBarButton.Visibility = Visibility.Visible;
        }

        private void CountdownTimer()
        {
            Task.Run((Action)(() =>
            {
                while (this.userPubSettings.OpenCountdown > 0)
                {
                    Guest.TimeToWait(1, userPubSettings.PubSimulationSpeed);
                    this.userPubSettings.OpenCountdown--;
                }                
            }));            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (simulationChoiceMenu.SelectedIndex)
            {
                case 0:
                    openBarButton.IsEnabled = true;
                    break;
                case 1:
                    userPubSettings.TwentyGlassesThreeChairs();
                    openBarButton.IsEnabled = true;
                    break;
                case 2:
                    userPubSettings.FiveGlassesTwentyChairs();
                    openBarButton.IsEnabled = true;
                    break;
                case 3:
                    userPubSettings.SlowGuests();
                    openBarButton.IsEnabled = true;
                    break;
                case 4:
                    userPubSettings.WaiterOnSpeed();
                    openBarButton.IsEnabled = true;
                    break;
                case 5:
                    userPubSettings.BarDoubleOpenTime();
                    openBarButton.IsEnabled = true;
                    break;
                case 6:
                    userPubSettings.CouplesNight();
                    openBarButton.IsEnabled = true;
                    break;
                case 7:
                    userPubSettings.BusOfGuests();
                    openBarButton.IsEnabled = true;
                    break;
            }               
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            userPubSettings.PubSimulationSpeed = Convert.ToInt32(simulationSpeedSlider.Value);
        }
    }
}
