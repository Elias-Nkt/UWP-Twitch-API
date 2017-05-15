using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class PivotPage : Page
    {

        private void Table_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Streams.Game = (sender as Image).Name;
            this.Frame.Navigate(typeof(Streams));
        }

        Twitch twitch = new Twitch("l0h8dwzkv4cf9gejv1ru661c6dvjj9");

        async void WriteTable(Task<Twitch.Game[]> tsk)
        {
            Twitch.Game[] games = tsk.Result;
            int i = 0;
            if (games == null) return;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (i >= games.Length) break;

                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
                    {
                        Grid g1 = new Grid();
                        g1.Width = 165;
                        g1.Height = 185;
                        g1.Margin = new Thickness(175 * x, 10 + y * 175, 0, 0);
                        g1.HorizontalAlignment = HorizontalAlignment.Left;
                        g1.VerticalAlignment = VerticalAlignment.Top;
                        GridGame.Children.Add(g1);

                        BitmapImage bmp = new BitmapImage();
                        bmp.UriSource = new Uri(games[i].box.medium);
                      

                        Image img1 = new Image();
                        img1.Source = bmp;
                        img1.Width = 100;
                        img1.Height = 160;
                        img1.Name = games[i].name;
                        img1.Tapped += Table_Tapped;
                        
                        img1.VerticalAlignment = VerticalAlignment.Top;
                        g1.Children.Add(img1);

                        TextBlock txtb1 = new TextBlock();
                       
                        txtb1.Height = 40;
                        txtb1.Width = 160;
                        txtb1.VerticalAlignment = VerticalAlignment.Bottom;
                        txtb1.TextAlignment = TextAlignment.Center;
                        if (games[i].name.Length > 18)
                        {
                            games[i].name = games[i].name.Substring(0, 18) + "...";
                        }
                        
                        txtb1.Text = games[i].name;
                        
                        
                        g1.Children.Add(txtb1);
                        i++;
                    });
                }
            }
        }

        async void WriteTableCh(Task<Twitch.Channel[]> tsk)
        {
            Twitch.Channel[] channels = tsk.Result;
            int i = 0;

            if (channels == null) return;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    if (i >= channels.Length) break;

                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
                    {
                        Grid g1 = new Grid();
                        g1.Width = 165;
                        g1.Height = 165;
                        g1.Margin = new Thickness(250 * x, 10 + y * 250, 0, 0);
                        g1.HorizontalAlignment = HorizontalAlignment.Left;
                        g1.VerticalAlignment = VerticalAlignment.Top;
                        GridChannels.Children.Add(g1);

                        if (channels[i].logo != null)
                        {
                            BitmapImage bmp = new BitmapImage();
                            bmp.UriSource = new Uri(channels[i].logo);


                            Image img1 = new Image();
                            img1.Source = bmp;
                            img1.Width = 100;
                            img1.Height = 160;
                            img1.Name = channels[i].name;
                            img1.Tapped += Table_Tapped;

                            img1.VerticalAlignment = VerticalAlignment.Top;
                            g1.Children.Add(img1);
                        }

                        TextBlock txtb1 = new TextBlock();
                        txtb1.Height = 40;
                        txtb1.Width = 160;
                        txtb1.VerticalAlignment = VerticalAlignment.Bottom;
                        txtb1.TextAlignment = TextAlignment.Center;
                        if (channels[i].name.Length > 18)
                        {
                            channels[i].name = channels[i].name.Substring(0, 18) + "...";
                        }

                        txtb1.Text = channels[i].name;


                        g1.Children.Add(txtb1);
                        i++;
                    });
                }
            }
        }

        async void WriteTableSt(Task<Twitch.Stream[]> tsk)
        {
            Twitch.Stream[] streams = tsk.Result;
            int i = 0;

            if (streams == null) return;

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    if (i >= streams.Length) break;

                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
                    {
                        Grid g1 = new Grid();
                        g1.Width = 165;
                        g1.Height = 100;
                        g1.Margin = new Thickness(250 * x, 10 + y * 250, 0, 0);
                        g1.HorizontalAlignment = HorizontalAlignment.Left;
                        g1.VerticalAlignment = VerticalAlignment.Top;
                        GridVideo.Children.Add(g1);

                        BitmapImage bmp = new BitmapImage();
                        bmp.UriSource = new Uri(streams[i].preview.medium);


                        Image img1 = new Image();
                        img1.Source = bmp;
                        img1.Width = 100;
                        img1.Height = 100;
                        img1.Name = streams[i].channel.name;
                        img1.Tapped += Table_Tapped;

                        img1.VerticalAlignment = VerticalAlignment.Top;
                        g1.Children.Add(img1);

                        TextBlock txtb1 = new TextBlock();

                        txtb1.Height = 40;
                        txtb1.Width = 160;
                        txtb1.VerticalAlignment = VerticalAlignment.Bottom;
                        txtb1.TextAlignment = TextAlignment.Center;
                        if (streams[i].channel.name.Length > 18)
                        {
                            streams[i].channel.name = streams[i].channel.name.Substring(0, 18) + "...";
                        }

                        txtb1.Text = streams[i].channel.name;


                        g1.Children.Add(txtb1);
                        i++;
                    });
                }
            }
        }

        public PivotPage()
        {
            this.InitializeComponent();

            string game = MainPage.RequestVar;

            Task<Twitch.Game[]> tskGames = twitch.SearchGames(game);
            tskGames.ContinueWith(tsk => WriteTable(tsk));

            Task<Twitch.Channel[]> tskChannels = twitch.SearchChannels(game);
            tskChannels.ContinueWith(tsk => WriteTableCh(tsk)); 

            Task<Twitch.Stream[]> tskStreams = twitch.SearchStreams(game);
            tskStreams.ContinueWith(tsk => WriteTableSt(tsk));
         
        }

        private void txtbutton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

    }
}
