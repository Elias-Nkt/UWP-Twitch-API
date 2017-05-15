using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPTwitch
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Streams : Page
    {
        


        public static string Game;

        Twitch twitch = new Twitch("l0h8dwzkv4cf9gejv1ru661c6dvjj9");

        async void ShowUrls(Task<Twitch.StreamQuality> tsk)
        {
            Twitch.StreamQuality quality = tsk.Result;
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
            {
                txtbl.Visibility = Visibility.Visible;
                
                txtbl.Text = "Source:  " + quality.source + "\n\n" + "High:  " + quality.high + "\n\n" + "Medium:  " + quality.medium + "\n\n" + "Low:  " + quality.low + "\n\n" + "Mobile:  " + quality.mobile;
                
            });
             /*MessageDialog dialog = new MessageDialog("Source: " + quality.source);
            await dialog.ShowAsync(); */
        }

        private void Stream_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Task<Twitch.StreamQuality> tskGames = twitch.GetQualities((sender as Image).Name);
            tskGames.ContinueWith(tsk => ShowUrls(tsk));

            
        }



        async void WriteTable(Task<Twitch.Stream[]> tsk)
        {
            Twitch.Stream[] streams = tsk.Result;
            int i = 0;

            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (i >= streams.Length) break;

                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
                    {
                        Grid g1 = new Grid();
                        g1.Width = 165;
                        g1.Height = 120;
                        g1.Margin = new Thickness(180 * x, 10 + y * 175, 0, 0);
                        g1.HorizontalAlignment = HorizontalAlignment.Left;
                        g1.VerticalAlignment = VerticalAlignment.Top;
                        GridStream.Children.Add(g1);

                        BitmapImage bmp = new BitmapImage();
                        bmp.UriSource = new Uri(streams[i].preview.large);


                        Image img1 = new Image();
                        img1.Source = bmp;
                        img1.Width = 100;
                        img1.Height = 120;
                        img1.Name = streams[i].channel.name;
                        img1.VerticalAlignment = VerticalAlignment.Top;
                        img1.Tapped += Stream_Tapped;
                        g1.Children.Add(img1);
                        

                        TextBlock txtb1 = new TextBlock();
                        
                        txtb1.Height = 40;
                        txtb1.Width = 160;
                        txtb1.VerticalAlignment = VerticalAlignment.Bottom;
                        txtbl.TextAlignment = TextAlignment.Center;
                        if (streams[i].channel.status.Length > 20)
                        {
                            streams[i].channel.status = streams[i].channel.status.Substring(0, 18) + "...";
                        }

                        txtb1.Text = streams[i].channel.status;


                        g1.Children.Add(txtb1);

                        i++;
                    });
                }
            }
        }

        public Streams()
        {
            Task<Twitch.Stream[]> tskGames = twitch.GetLiveStreams(Game);
            tskGames.ContinueWith(tsk => WriteTable(tsk));
            
            
            this.InitializeComponent();
        }

        private void txtbutton_Click(object sender, RoutedEventArgs e)
        {
            if(txtbl.Visibility == Visibility.Collapsed)
            {
                this.Frame.Navigate(typeof(PivotPage));
            }
            txtbl.Visibility = Visibility.Collapsed;
        }
    }
}
