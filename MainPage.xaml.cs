using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using System.Diagnostics;
using Windows.Devices.Enumeration;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace goBanana
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaPlayer music = new MediaPlayer();
        MediaPlayer effect = new MediaPlayer();
        public MainPage()
        {
            this.InitializeComponent();
            music.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/music.mp3"));
            music.Play();
            music.IsLoopingEnabled = true;
            music.Volume = 0.0;

            effect.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/music.mp3"));
            effect.Volume = 30.0;

            SPANISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
        }
        private void IncreaseBanana_Click(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(BananaCount.Text);
            BananaCount.Text = (num + 1).ToString();
            ClickHere.Visibility = Visibility.Collapsed;
        }

        private void music_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            music.Volume = e.NewValue / 100.0;
        }

        private void brightness_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            general.Opacity = e.NewValue / 100.0 + 0.1;
        }
        private void soundEffect_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            effect.Volume = e.NewValue / 100.0;
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            OPCIONES.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            INFO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            SKINS.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            GLOSARIO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));

            options.Visibility = Visibility.Visible;
            info.Visibility = Visibility.Collapsed;
            game.Visibility = Visibility.Collapsed;
            skinsButtons.Visibility = Visibility.Collapsed;
            skinsText.Visibility = Visibility.Collapsed;
            simiopedia.Visibility = Visibility.Collapsed;
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            INFO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            OPCIONES.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            SKINS.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            GLOSARIO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));

            info.Visibility = Visibility.Visible;
            options.Visibility = Visibility.Collapsed;
            game.Visibility = Visibility.Collapsed;
            skinsButtons.Visibility = Visibility.Collapsed;
            skinsText.Visibility = Visibility.Collapsed;
            simiopedia.Visibility = Visibility.Collapsed;
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            INFO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            OPCIONES.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            SKINS.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            GLOSARIO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));

            game.Visibility = Visibility.Visible;
            options.Visibility = Visibility.Collapsed;
            info.Visibility = Visibility.Collapsed;
            skinsButtons.Visibility = Visibility.Collapsed;
            skinsText.Visibility = Visibility.Collapsed;
            simiopedia.Visibility = Visibility.Collapsed;
        }

        private void SkinsButton_Click(object sender, RoutedEventArgs e)
        {
            SKINS.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            INFO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            OPCIONES.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            GLOSARIO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));

            skinsButtons.Visibility = Visibility.Visible;
            skinsText.Visibility = Visibility.Visible;
            options.Visibility = Visibility.Collapsed;
            info.Visibility = Visibility.Collapsed;
            game.Visibility = Visibility.Collapsed;
            simiopedia.Visibility = Visibility.Collapsed;
        }

        private void GlosaryButton_Click(object sender, RoutedEventArgs e)
        {
            GLOSARIO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            INFO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            SKINS.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            OPCIONES.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));

            simiopedia.Visibility = Visibility.Visible;
            options.Visibility = Visibility.Collapsed;
            info.Visibility = Visibility.Collapsed;
            game.Visibility = Visibility.Collapsed;
            skinsButtons.Visibility = Visibility.Collapsed;
            skinsText.Visibility = Visibility.Collapsed;
        }

        private void EnglishButton_Click(object sender, RoutedEventArgs e)
        {
            ENGLISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            SPANISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            FRENCH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            GERMAN.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
        }

        private void FrenchButton_Click(object sender, RoutedEventArgs e)
        {
            FRENCH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            SPANISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            ENGLISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            GERMAN.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
        }

        private void SpanishButton_Click(object sender, RoutedEventArgs e)
        {
            SPANISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            ENGLISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            FRENCH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            GERMAN.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
        }

        private void GermanButton_Click(object sender, RoutedEventArgs e)
        {
            GERMAN.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            SPANISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            FRENCH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            ENGLISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
        }

        private void AboutUsButton_Click(object sender, RoutedEventArgs e)
        {
            music.Pause();
            info.Visibility = Visibility.Collapsed;
            aboutUs.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            JUEGO.Visibility = Visibility.Collapsed;
            SKINS.Visibility = Visibility.Collapsed;
            GLOSARIO.Visibility = Visibility.Collapsed;
            INFO.Visibility = Visibility.Collapsed;
            OPCIONES.Visibility = Visibility.Collapsed;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            music.Play();
            info.Visibility = Visibility.Visible;
            aboutUs.Visibility = Visibility.Collapsed;
            BackButton.Visibility =Visibility.Collapsed;
            JUEGO.Visibility = Visibility.Visible;
            SKINS.Visibility = Visibility.Visible;
            GLOSARIO.Visibility = Visibility.Visible;
            INFO.Visibility = Visibility.Visible;
            OPCIONES.Visibility = Visibility.Visible;
        }

        private void BuyCursor_Click(object sender, RoutedEventArgs e)
        {
            if(int.Parse(CursorPrice.Text) <= int.Parse(BananaCount.Text))
            {
                NumberOfCursors.Text = (int.Parse(NumberOfCursors.Text) + 1).ToString();
                BananaCount.Text = (int.Parse(BananaCount.Text) - int.Parse(CursorPrice.Text)).ToString();
                CursorPrice.Text = (int.Parse(CursorPrice.Text) + 10).ToString();
                BananasPerSec.Text = (int.Parse(BananasPerSec.Text) + 1).ToString();
            }
        }

        private void BuyMonkey_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(MonkeyPrice.Text) <= int.Parse(BananaCount.Text))
            {
                NumberOfMonkeys.Text = (int.Parse(NumberOfMonkeys.Text) + 1).ToString();
                BananaCount.Text = (int.Parse(BananaCount.Text) - int.Parse(MonkeyPrice.Text)).ToString();
                MonkeyPrice.Text = (int.Parse(MonkeyPrice.Text) + 50).ToString();
                BananasPerSec.Text = (int.Parse(BananasPerSec.Text) + 10).ToString();
            }
        }

        private void BuyTractor_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(TractorPrice.Text) <= int.Parse(BananaCount.Text))
            {
                NumberOfTractors.Text = (int.Parse(NumberOfTractors.Text) + 1).ToString();
                BananaCount.Text = (int.Parse(BananaCount.Text) - int.Parse(TractorPrice.Text)).ToString();
                TractorPrice.Text = (int.Parse(TractorPrice.Text) + 2000).ToString();
            }
        }

        private void Clown_Click(object sender, RoutedEventArgs e)
        {
            if (Clowns.Text == "COMPRAR" && 10 <= int.Parse(BananaCount.Text))
            {
                BananaCount.Text = (int.Parse(BananaCount.Text) - 10).ToString();
                ClownPrice.Text = "Equipar";
                Clowns.Text = "COMPRADO";
            }
            else if(Clowns.Text == "COMPRADO" && ClownPrice.Text == "Equipar")
            {
                Desequipar.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
                CurrentSkin.Text = "PAYASO";
                ClownPrice.Text = "Equipado";
                if (VikingPrice.Text == "Equipado") VikingPrice.Text = "Equipar";
            }
        }

        private void Viking_Click(object sender, RoutedEventArgs e)
        {
            Desequipar.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            if (Vikings.Text == "COMPRAR" && 20 <= int.Parse(BananaCount.Text))
            {
                BananaCount.Text = (int.Parse(BananaCount.Text) - 20).ToString();
                VikingPrice.Text = "Equipar";
                Vikings.Text = "COMPRADO";
            }
            else if (Vikings.Text == "COMPRADO" && VikingPrice.Text == "Equipar")
            {
                CurrentSkin.Text = "VIKINGO";
                VikingPrice.Text = "Equipado";
                if (ClownPrice.Text == "Equipado") ClownPrice.Text = "Equipar";
            }
        }

        private void Desequipar_Click(object sender, RoutedEventArgs e) 
        { 
            Desequipar.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));

            CurrentSkin.Text = "NINGUNO";

            if(ClownPrice.Text == "Equipado") ClownPrice.Text = "Equipar";
            else if (VikingPrice.Text == "Equipado") VikingPrice.Text = "Equipar";
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            monete.Visibility = Visibility.Collapsed;
            orangutan.Visibility = Visibility.Visible;

            //string s = System.IO.Directory.GetCurrentDirectory() + "\\" + "Assets\\noflecha.jpg";
            //nextBImage.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));

            //s = System.IO.Directory.GetCurrentDirectory() + "\\" + "Assets\\flecha.jpg";
            //backBImage.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));

            //nextBImage.Source = new BitmapImage(new Uri("Assets/noflecha.jpg", UriKind.Relative));
            //backBImage.Source = new BitmapImage(new Uri("Assets/flecha.jpg", UriKind.Relative));
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            monete.Visibility = Visibility.Visible;
            orangutan.Visibility = Visibility.Collapsed;

            //string s = System.IO.Directory.GetCurrentDirectory() + "\\" + "Assets\\flecha.jpg";
            //nextBImage.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));

            //nextBImage.Source = new BitmapImage(new Uri("Assets/flecha.jpg", UriKind.Relative));
            //backBImage.Source = new BitmapImage(new Uri("Assets/noflecha.jpg", UriKind.Relative));

            //s = System.IO.Directory.GetCurrentDirectory() + "\\" + "Assets\\noflecha.jpg";
            //backBImage.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));
        }
        private async void GoGithub_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("https://github.com/albpenal/goBanana");
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
