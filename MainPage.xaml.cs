using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
using Windows.Storage;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Core;
using Windows.Media.Playback;
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
        int MaxMonkeys = 10;
        MediaPlayer music = new MediaPlayer();
        MediaPlayer effect = new MediaPlayer();
        public int num;
        ContentControl selectedObject;

        public DispatcherTimer GameTimer;
        private Gamepad mando;
        public GamepadReading estadoActual;
        double X = 0, Y = 0;
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            music.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/music.mp3"));
            music.Play();
            BananaCount.Text = num.ToString();
            music.IsLoopingEnabled = true;
            music.Volume = 0.0;

            effect.Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/music.mp3"));
            effect.Volume = 30.0;

            SPANISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            Gamepad.GamepadAdded += OnGamepadAdded;
            Gamepad.GamepadRemoved += OnGamepadRemoved;
            GameTimerSetup();
        }

        private void Timer_Tick(object sender, object e)
        {
            int currentBananas = int.Parse(BananaCount.Text);
            currentBananas += int.Parse(BananasPerSec.Text);
            BananaCount.Text = currentBananas.ToString();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BananaCount.Text = num.ToString();
        }

        private void IncreaseBanana_Click(object sender, RoutedEventArgs e)
        {
            num = int.Parse(BananaCount.Text);
            BananaCount.Text = (num + 1).ToString();
            ClickHere.Visibility = Visibility.Collapsed;
        }

        #region MENUS
        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            OPCIONES.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            INFO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            SKINS.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            GLOSARIO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));

            options.Visibility = Visibility.Visible;
            info.Visibility = Visibility.Collapsed;
            game.Opacity = 0.01;
            MiCanvas.Opacity = 0.01;
            skinsButtons.Visibility = Visibility.Collapsed;
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
            game.Opacity = 0.01;
            MiCanvas.Opacity = 0.01;
            skinsButtons.Visibility = Visibility.Collapsed;
            simiopedia.Visibility = Visibility.Collapsed;
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            INFO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            OPCIONES.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            SKINS.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            GLOSARIO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));

            game.Opacity = 1;
            MiCanvas.Opacity = 1;
            options.Visibility = Visibility.Collapsed;
            info.Visibility = Visibility.Collapsed;
            skinsButtons.Visibility = Visibility.Collapsed;
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
            options.Visibility = Visibility.Collapsed;
            info.Visibility = Visibility.Collapsed;
            game.Opacity = 0.01;
            MiCanvas.Opacity = 0.01;
            simiopedia.Visibility = Visibility.Collapsed;
        }

        private void GlosaryButton_Click(object sender, RoutedEventArgs e)
        {
            GLOSARIO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            INFO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            JUEGO.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            SKINS.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            OPCIONES.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));

            backBImage.Opacity = 0.3;
            nextBImage.Opacity = 1;

            monete.Visibility = Visibility.Visible;
            orangutan.Visibility = Visibility.Collapsed;
            simiopedia.Visibility = Visibility.Visible;
            options.Visibility = Visibility.Collapsed;
            info.Visibility = Visibility.Collapsed;
            game.Opacity = 0.01;
            MiCanvas.Opacity = 0.01;
            skinsButtons.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region OPCIONES
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
        #endregion

        #region INFO
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
            num = int.Parse(BananaCount.Text);
            Frame.Navigate(typeof(IntialPage), this);

            GERMAN.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));
            SPANISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            FRENCH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
            ENGLISH.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
        }

        private async void GoGithub_Click(object sender, RoutedEventArgs e)// ir a GitHub
        {
            music.Pause();
            Uri uri = new Uri("https://github.com/albpenal/goBanana");
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        #endregion

        #region TIENDA
        private void BuyCursor_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(CursorPrice.Text) <= int.Parse(BananaCount.Text))
            {
                NumberOfCursors.Text = (int.Parse(NumberOfCursors.Text) + 1).ToString();
                BananaCount.Text = (int.Parse(BananaCount.Text) - int.Parse(CursorPrice.Text)).ToString();
                CursorPrice.Text = (int.Parse(CursorPrice.Text) + 10).ToString();
                BananasPerSec.Text = (int.Parse(BananasPerSec.Text) + 1).ToString();
            }
        }

        private void BuyMonkey_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(MonkeyPrice.Text) <= int.Parse(BananaCount.Text) && int.Parse(NumberOfMonkeys.Text) < MaxMonkeys)
            {
                // actualizaciones de numeros
                NumberOfMonkeys.Text = (int.Parse(NumberOfMonkeys.Text) + 1).ToString();
                BananaCount.Text = (int.Parse(BananaCount.Text) - int.Parse(MonkeyPrice.Text)).ToString();
                MonkeyPrice.Text = (int.Parse(MonkeyPrice.Text) + 50).ToString();
                BananasPerSec.Text = (int.Parse(BananasPerSec.Text) + 10).ToString();

                // creacion de mono en el canvas
                Image myImage = new Image();
                BitmapImage bi = new BitmapImage();
                string s = System.IO.Directory.GetCurrentDirectory() + "\\" + "Assets\\mono1.png";
                bi.UriSource = new Uri(s);
                myImage.Source = bi;
                Random rnd = new Random();
                int x = rnd.Next(0, (int)MiCanvas.ActualWidth - 50);
                int y = rnd.Next(0, (int)MiCanvas.ActualHeight - 125);

                myImage.MaxWidth = 50; // Establece un ancho fijo para la imagen
                myImage.SetValue(Canvas.LeftProperty, rnd.NextDouble() * (MiCanvas.ActualWidth - myImage.Width)); // Posición aleatoria en el eje X
                myImage.SetValue(Canvas.TopProperty, rnd.NextDouble() * (MiCanvas.ActualHeight - myImage.Height)); // Posición aleatoria en el eje Y

                ContentControl c = new ContentControl();
                c.Content = myImage;
                c.IsTabStop = true;
                c.IsFocusEngagementEnabled = true;
                c.UseSystemFocusVisuals = true;
                c.PointerPressed += ItemClick;
                MiCanvas.Children.Add(c);

                CompositeTransform Transformacion = new CompositeTransform();
                Transformacion.TranslateX = x;
                Transformacion.TranslateY = y;
                Transformacion.Rotation = 0;
                c.RenderTransform = Transformacion;
                c.ManipulationMode = ManipulationModes.All;
                c.ManipulationDelta += NewImg_ManipulationDelta;

                selectedObject = c;
            }
        }
        private void BuyTractor_Click(object sender, RoutedEventArgs e)// comprar tractor
        {
            if (int.Parse(TractorPrice.Text) <= int.Parse(BananaCount.Text))
            {
                NumberOfTractors.Text = (int.Parse(NumberOfTractors.Text) + 1).ToString();
                BananaCount.Text = (int.Parse(BananaCount.Text) - int.Parse(TractorPrice.Text)).ToString();
                TractorPrice.Text = (int.Parse(TractorPrice.Text) + 2000).ToString();
            }
        }
        #endregion

        #region MOVIMIENTO MONOS
        private void NewImg_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            CompositeTransform Transformacion = ((sender as ContentControl).RenderTransform as CompositeTransform);
            if (Transformacion.TranslateX + e.Delta.Translation.X >= (int)MiCanvas.ActualWidth - 50 || Transformacion.TranslateX + e.Delta.Translation.X <= 0) Transformacion.TranslateX += 0;
            else if (Transformacion.TranslateY + e.Delta.Translation.Y >= (int)MiCanvas.ActualHeight - 50 || Transformacion.TranslateY + e.Delta.Translation.Y <= 0) Transformacion.TranslateY += 0;
            else
            {
                Transformacion.TranslateX += e.Delta.Translation.X;
                Transformacion.TranslateY += e.Delta.Translation.Y;
                Transformacion.Rotation += e.Delta.Rotation;
            }
    (sender as ContentControl).RenderTransform = Transformacion;
        }

        private void ItemClick(object sender, PointerRoutedEventArgs e)
        {
            if (e.OriginalSource != null)
            {
                selectedObject = e.OriginalSource as ContentControl;
            }
        }

        #endregion

        #region MANDO
        private void OnGamepadAdded(object sender, Gamepad e)
        {
            if (mando == null)
            {
                mando = e;
            }
        }

        private void OnGamepadRemoved(object sender, Gamepad e)
        {
            if (mando == e)
            {
                mando = null;
            }
        }
        public void ZMMando()
        {
            if (estadoActual.RightThumbstickX < -0.1) estadoActual.RightThumbstickX += 0.1;
            else if (estadoActual.RightThumbstickX > 0.1) estadoActual.RightThumbstickX -= 0.1;
            else estadoActual.RightThumbstickX = 0;

            if (estadoActual.RightThumbstickY < -0.1) estadoActual.RightThumbstickY += 0.1;
            else if (estadoActual.RightThumbstickY > 0.1) estadoActual.RightThumbstickY -= 0.1;
            else estadoActual.RightThumbstickY = 0;
        }

        public void ActualizarEstado()
        {
            double rightStickX = estadoActual.RightThumbstickX; // returns a value between -1.0 and +1.0
            estadoActual = mando.GetCurrentReading();
        }

        public void GameTimerSetup()
        {
            GameTimer = new DispatcherTimer();
            GameTimer.Tick += GameTimer_Tick;// dispatcherTimer_Tick;
            GameTimer.Interval = new TimeSpan(100); //100000*10^-7s=1cs;
            GameTimer.Start();

        }

        void GameTimer_Tick(object sender, object e)
        {   //Los Drones se simulan a nivel de aplicación cada 0.1s
            if (mando != null)
            {
                ActualizarEstado();
                ActualizaIU();
                ZMMando();
            }

        }

        private void ActualizaIU()// actualiza movimiento de monos con mando
        {
            if (mando != null)
            {
                X = Y = 0;
                selectedObject = FocusManager.GetFocusedElement() as ContentControl;
                if (selectedObject != null && selectedObject.Parent == MiCanvas)
                {
                    X = (double)(X + 2 * estadoActual.RightThumbstickX);
                    Y = (double)(Y - 2 * estadoActual.RightThumbstickY);

                    CompositeTransform Transformacion = selectedObject.RenderTransform as CompositeTransform;

                    if (Transformacion.TranslateX + X >= (int)MiCanvas.ActualWidth - 50 || Transformacion.TranslateX + X <= 0) Transformacion.TranslateX += 0;
                    else if (Transformacion.TranslateY + Y >= (int)MiCanvas.ActualHeight - 50 || Transformacion.TranslateY + Y <= 0) Transformacion.TranslateY += 0;
                    else
                    {
                        Transformacion.TranslateX += X;
                        Transformacion.TranslateY += Y;
                    }

                    selectedObject.RenderTransform = Transformacion;
                }
            }
        }
        #endregion

        #region SKINS
        private void Clown_Click(object sender, RoutedEventArgs e)// equipar/comprar payaso
        {
            if (Clowns.Text == "COMPRAR" && 10 <= int.Parse(BananaCount.Text))
            {
                BananaCount.Text = (int.Parse(BananaCount.Text) - 10).ToString();
                ClownPrice.Text = "Equipar";
                Clowns.Text = "COMPRADO";
            }
            else if (Clowns.Text == "COMPRADO" && ClownPrice.Text == "Equipar")
            {
                Desequipar.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xDD, 0x5F));
                CurrentSkin.Text = "PAYASO";
                ClownPrice.Text = "Equipado";
                if (VikingPrice.Text == "Equipado") VikingPrice.Text = "Equipar";
            }
        }

        private void Viking_Click(object sender, RoutedEventArgs e)// equipar/comprar vikingo
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

        private void Desequipar_Click(object sender, RoutedEventArgs e) // boton desequipar
        {
            Desequipar.Background = new SolidColorBrush(Color.FromArgb(0xFE, 0xFE, 0xCD, 0x13));

            CurrentSkin.Text = "NINGUNO";

            if (ClownPrice.Text == "Equipado") ClownPrice.Text = "Equipar";
            else if (VikingPrice.Text == "Equipado") VikingPrice.Text = "Equipar";
        }
        #endregion

        #region GLOSARIO
        private void next_Click(object sender, RoutedEventArgs e)// boton ir hacia delante
        {
            monete.Visibility = Visibility.Collapsed;
            orangutan.Visibility = Visibility.Visible;

            nextBImage.Opacity = 0.3;
            backBImage.Opacity = 1;

            next.IsEnabled = false;
            back.IsEnabled = true;
        }

        private void back_Click(object sender, RoutedEventArgs e)// boton ir hacia atras glosario
        {
            monete.Visibility = Visibility.Visible;
            orangutan.Visibility = Visibility.Collapsed;

            nextBImage.Opacity = 1;
            backBImage.Opacity = 0.3;

            next.IsEnabled = true;
            back.IsEnabled = false;
        }
        #endregion
    }
}
