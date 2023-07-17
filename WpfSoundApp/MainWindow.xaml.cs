using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Google.Cloud.Firestore;


using C1.WPF.Carousel;
using WpfSoundApp.Core;

namespace WpfSoundApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                Point newPoint = e.GetPosition(this);
                if (newPoint.Y < 40)
                    DragMove();
            }
        }

      

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TelegramButton_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://t.me/hazker_aruk"; // Замініть це на ваше власне посилання
            System.Diagnostics.Process.Start(url);
        }
        private void TikTokButton_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://www.tiktok.com/@arukdelon?is_from_webapp=1&sender_device=pc"; // Замініть це на ваше власне посилання
            System.Diagnostics.Process.Start(url);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
 

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InfoPanel.Visibility = Visibility.Hidden;
            verionLabel.Content = Config.Version;
            CheckUpdate();
            SetPanValue();
        }


        private async void CheckUpdate()
        {
            await FirebaseManager.LoadAppData();

            if(!Config.Version.Equals(FirebaseManager.GetCurrentAppVerion()))
            {
                verionLabel.Content = "Update";
            }
        
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
   
           
        }
        public static List<Dictionary<string, object>> resultEvent;
        public static List<Dictionary<string, object>> resultUpdate;
        public static List<Dictionary<string, object>> resultFAQ;
        




        private async void SetPanValue()
        {
            await FirebaseManager.LoadTableData("Events");
          
            resultEvent = FirebaseManager.GetCurrentPanTableData();

            await FirebaseManager.LoadTableData("Updates");

            resultUpdate = FirebaseManager.GetCurrentPanTableData();

            await FirebaseManager.LoadTableData("FAQ");

            resultFAQ = FirebaseManager.GetCurrentPanTableData();

            setTextValue();
        }

        private void setTextValue()
        {
            if (resultEvent.Count > 0)
            {
                if (resultEvent[0].Last().Value.ToString().Length > 39)
                    eventTabContent1.Content = resultEvent[0].Last().Value.ToString().Substring(0, 39) + "...";
                else
                    eventTabContent1.Content = resultEvent[0].Last().Value.ToString();

                eventTabDate1.Text = resultEvent[0].First().Value.ToString().Substring(16, 2) + "/" +
                    resultEvent[0].First().Value.ToString().Substring(19, 2);
            }
            if (resultEvent.Count > 1)
            {
                if (resultEvent[1].Last().Value.ToString().Length > 39)
                    eventTabContent2.Content = resultEvent[1].Last().Value.ToString().Substring(0, 39) + "...";
                else
                    eventTabContent2.Content = resultEvent[1].Last().Value.ToString();

                eventTabDate2.Text = resultEvent[1].First().Value.ToString().Substring(16, 2) + "/" +
                    resultEvent[1].First().Value.ToString().Substring(19, 2);
            }

            if (resultUpdate.Count > 0)
            {
                if (resultUpdate[0].Last().Value.ToString().Length > 39)
                    updateTabContent1.Content = resultUpdate[0].Last().Value.ToString().Substring(0, 39) + "...";
                else
                    updateTabContent1.Content = resultUpdate[0].Last().Value.ToString();

                updateTabDate1.Text = resultUpdate[0].First().Value.ToString().Substring(16, 2) + "/" +
                    resultUpdate[0].First().Value.ToString().Substring(19, 2);
            }
            if (resultUpdate.Count > 1)
            {
                if (resultUpdate[1].Last().Value.ToString().Length > 39)
                    updateTabContent2.Content = resultUpdate[1].Last().Value.ToString().Substring(0, 39) + "...";
                else
                    updateTabContent2.Content = resultUpdate[1].Last().Value.ToString();

                updateTabDate2.Text = resultUpdate[1].First().Value.ToString().Substring(16, 2) + "/" +
                    resultUpdate[1].First().Value.ToString().Substring(19, 2);
            }

            if (resultFAQ.Count > 0)
            {
                if (resultFAQ[0].Last().Value.ToString().Length > 39)
                    faqTabContent1.Content = resultFAQ[0].Last().Value.ToString().Substring(0, 39) + "...";
                else
                    faqTabContent1.Content = resultFAQ[0].Last().Value.ToString();

                faqTabDate1.Text = resultFAQ[0].First().Value.ToString().Substring(16, 2) + "/" +
                    resultFAQ[0].First().Value.ToString().Substring(19, 2);
            }
            if (resultFAQ.Count > 1)
            {
                if (resultFAQ[1].Last().Value.ToString().Length > 39)
                    faqTabContent2.Content = resultFAQ[1].Last().Value.ToString().Substring(0, 39) + "...";
                else
                    faqTabContent2.Content = resultFAQ[1].Last().Value.ToString();

                faqTabDate2.Text = resultFAQ[1].First().Value.ToString().Substring(16, 2) + "/" +
                    resultFAQ[1].First().Value.ToString().Substring(19, 2);
            }




            InfoPanel.Visibility = Visibility.Visible;
        }
    }
}
