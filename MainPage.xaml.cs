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
            music.Volume = 5.0;

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
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            INFO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            OPCIONES.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            SKINS.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            GLOSARIO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));

            options.Visibility = Visibility.Collapsed;
            info.Visibility = Visibility.Collapsed;
        }

        private void SkinsButton_Click(object sender, RoutedEventArgs e)
        {
            SKINS.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            INFO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            OPCIONES.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            GLOSARIO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));

            options.Visibility = Visibility.Collapsed;
            info.Visibility = Visibility.Collapsed;
        }

        private void GlosaryButton_Click(object sender, RoutedEventArgs e)
        {
            GLOSARIO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            INFO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            SKINS.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            OPCIONES.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));

            options.Visibility = Visibility.Collapsed;
            info.Visibility = Visibility.Collapsed;
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
        }
    }
}
