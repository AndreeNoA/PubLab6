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
        private static CancellationTokenSource cancelProgramTokenSource = new CancellationTokenSource();
        public CancellationToken cancelProgramToken = cancelProgramTokenSource.Token;
        public static ItemsBag<Chair> chairs;
        public static ItemsBag<CleanGlass> cleanGlasses;
        public static ItemsCollection<DirtyGlass> dirtyGlasses;
        public static ItemsBag<GlassOnTray> trayOfDirtyGlasses;

        public MainWindow()
        {
            InitializeComponent();
            closeBarButton.Visibility = Visibility.Hidden;
            chairs = new ItemsBag<Chair>();
            cleanGlasses = new ItemsBag<CleanGlass>();
            dirtyGlasses = new ItemsCollection<DirtyGlass>();
            trayOfDirtyGlasses = new ItemsBag<GlassOnTray>();

            chairs.CreateItems(new Chair(), PubSettings.MyPub().numOfChairs);
            cleanGlasses.CreateItems(new CleanGlass(), PubSettings.MyPub().numOfGlasses);
        }

        private void OpenBarButton_Click(object sender, RoutedEventArgs e)
        {
            openBarButton.IsEnabled = false;
            openBarButton.Visibility = Visibility.Hidden;
            closeBarButton.IsEnabled = true;
            closeBarButton.Visibility = Visibility.Visible;
            PubSettings.MyPub().openCountdown = PubSettings.MyPub().openDuration;
            CountdownTimer();
            GuestListBox.Items.Insert(0, "Bar has opened");

            Bartender bartender = new Bartender();
            Waiter waiter = new Waiter();
            Bouncer bouncer = new Bouncer();
            Task.Run(() => { bouncer.BouncerActions(AddToLists); });
            Task.Run(() => { bartender.BartenderActions(AddToLists, cleanGlasses); });
            Task.Run(() => { waiter.WaiterActions(AddToLists, cleanGlasses, dirtyGlasses, trayOfDirtyGlasses); });
            Task.Run(() => { UpdateLabels(); });           
        }
        
        private void AddToLists(string action, object sender)
        {
            action = $"{actionCount++}. {action}";
            Dispatcher.Invoke(() =>
            {
                switch (sender)
                {
                    case Guest _:
                        GuestListBox.Items.Insert(0, action);                        
                        break;
                    case Bartender _:
                        BartenderListBox.Items.Insert(0, action);
                        break;
                    case Waiter _:
                        WaiterListBox.Items.Insert(0, action);
                        break;
                    case Bouncer _:
                        GuestListBox.Items.Insert(0, action);
                        break;
                }
            });
        }

        public void UpdateLabels()
        {
            while (!cancelProgramToken.IsCancellationRequested)
            {
                Dispatcher.Invoke(() =>
                {
                    labelGuestsAtBar.Content = $"Number of guests in pub: {Guest.guestsInPub}";
                    labelClosingTime.Content = $"Time to closing: {PubSettings.MyPub().openCountdown}";
                    labelAvailableChairs.Content = $"Available Chairs: {chairs.itemBag.Count}";
                    labelAvailableGlasses.Content = $"Number of clean glasses: {cleanGlasses.itemBag.Count}";

                    if (PubSettings.MyPub().pubOpenButton == true)
                        {
                            openBarButton.IsEnabled = true;
                        }
                });
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
            PubSettings.MyPub().openCountdown = 1;
            openBarButton.Visibility = Visibility.Visible;
        }

        private void CountdownTimer()
        {
            Task.Run(() =>
            {
                while (PubSettings.MyPub().openCountdown > 0)
                {
                    Guest.TimeToWait(1);
                    PubSettings.MyPub().openCountdown--;
                }                
            });            
        }
    }
}
