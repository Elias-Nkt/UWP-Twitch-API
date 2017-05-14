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
    public sealed partial class Streams : Page
    {
        Twitch twitch = new Twitch("l0h8dwzkv4cf9gejv1ru661c6dvjj9");

        async void WriteTable(Task<Twitch.Stream[]> tsk)
        {
            Twitch.Stream[] streams = tsk.Result;
            int i = 0;

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if (i >= streams.Length) break;

                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
                    {
                        Grid g1 = new Grid();
                        g1.Width = 165;
                        g1.Height = 165;
                        g1.Margin = new Thickness(175 * x, y * 175, 0, 0);
                        g1.HorizontalAlignment = HorizontalAlignment.Left;
                        g1.VerticalAlignment = VerticalAlignment.Top;
                        GridStream.Children.Add(g1);

                        BitmapImage bmp = new BitmapImage();


                        Image img1 = new Image();
                        //  img1.Source = ;
                        img1.Width = 160;
                        img1.Height = 90;
                        GridStream.Children.Add(img1);

                        TextBlock txtb1 = new TextBlock();
                        // txtb1.
                        // tapped - вот оно божественное событие
                        txtb1.Height = 40;
                        txtb1.Width = 160;
                        if (streams[i].channel.name.Length > 20)
                        {
                            streams[i].channel.name = streams[i].channel.name.Substring(0, 18);
                        }

                        txtb1.Text = streams[i].channel.name;


                        g1.Children.Add(txtb1);
                        i++;
                    });
                }
            }
        }

        public Streams()
        {
            
            Task<Twitch.Stream[]> tskGames = twitch.GetStreams("overwatch");
            tskGames.ContinueWith(tsk => WriteTable(tsk));
            int i = 0;
            // Тут генерим стримы по игре
            this.InitializeComponent();
        }
    }
}
