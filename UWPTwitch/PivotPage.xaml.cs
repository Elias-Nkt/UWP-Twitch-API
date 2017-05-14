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
        Twitch twitch = new Twitch("l0h8dwzkv4cf9gejv1ru661c6dvjj9");

        async void WriteTable(Task<Twitch.Game[]> tsk)
        {
            Twitch.Game[] games = tsk.Result;
            int i = 0;
            // int y = games.Length / 7 + 1;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    if (i >= games.Length) break;

                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Low, () =>
                    {
                        Grid g1 = new Grid();
                        g1.Width = 165;
                        g1.Height = 165;
                        g1.Margin = new Thickness(175 * x, y * 175, 0, 0);
                        g1.HorizontalAlignment = HorizontalAlignment.Left;
                        g1.VerticalAlignment = VerticalAlignment.Top;
                        GridGame.Children.Add(g1);

                        BitmapImage bmp = new BitmapImage();
                        

                        Image img1 = new Image();
                        //  img1.Source = ;
                        img1.Width = 160;
                        img1.Height = 90;
                        GridGame.Children.Add(img1);

                        TextBlock txtb1 = new TextBlock();
                       // txtb1.
                       // tapped - вот оно божественное событие
                        txtb1.Height = 40;
                        txtb1.Width = 160;
                        if(games[i].name.Length > 20)
                        {
                            games[i].name = games[i].name.Substring(0, 18);
                        }
                        
                        txtb1.Text = games[i].name;
                        
                        
                        g1.Children.Add(txtb1);
                        i++;
                    });
                }
            }
        }


        public PivotPage()
        {
            this.InitializeComponent();

            //txt1.Text = MyPivot.Width.ToString();
            string game = MainPage.RequestVar;

            Task<Twitch.Game[]> tskGames = twitch.SearchGames(game);
            tskGames.ContinueWith(tsk => WriteTable(tsk));

           /* Task<Twitch.Game[]> tskGames = twitch.SearchGames("over");
            tskGames.ContinueWith(tsk => WriteTable(tsk));
            */
        }

        private void AddEl_Click(object sender, RoutedEventArgs e)
        {

            Image img1 = new Image();
          //  img1.Source = ;
            img1.Width = 160;
            img1.Height = 90;
            GridGame.Children.Add(img1);

            Grid g1 = new Grid();
            g1.Width = 165;
            g1.Height = 135;
            GridGame.Children.Add(g1);
            // 160 90 генерим TextBlock H - 40.
            /* можно пропробовать генерить просто по маргинам, без ориентации. Выводить в том порядке в котором они будут
             * генерим сразу во все пивоты. 1 - GridGame поэтому в конструкторе обрабаываем
            Button button1 = new Button(); L T R B .Margin = new Thickness(420, -2980, 0, 0);
            button1.Content = "Новая кнопка";
            button1.Width = 120;
            button1.Height = 40;
            // добавление кнопки в грид
            layoutGrid.Children.Add(button1);
            */
        }
    }
}
