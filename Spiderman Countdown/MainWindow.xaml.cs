using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Spiderman_Countdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private TimeSpan _timeRemaining;
        private TimeSpan _defaultTime = TimeSpan.FromMinutes(25);
        private bool isRunning = false;
        private bool isPlayingAlarm = false;
        int view;

        private MediaPlayer _player = new MediaPlayer();
        public MainWindow()
        {
            InitializeComponent();
            _timeRemaining = _defaultTime;
            updateDisplay();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += timeTick;
        }

        private void PlayAlarm()
        {
            try {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Alarm.mp3");
                _player.Open(new Uri(path));
                _player.MediaEnded += (s, e) =>
                {
                    if (isPlayingAlarm) { 
                        _player.Position = TimeSpan.Zero;
                        _player.Play();
                    }
                };
                isPlayingAlarm = true;
                _player.Play();
            }
            catch(Exception ex) {
                MessageBox.Show("Cannot play alarm: " + ex.Message);
            }
        }

        private void timeTick(object? sender, EventArgs e)
        {
            if (_timeRemaining > TimeSpan.Zero)
            {
                _timeRemaining = _timeRemaining.Subtract(TimeSpan.FromSeconds(1));
                updateDisplay();
            }
            else {
                _timer.Stop();
                PlayAlarm();
                this.Topmost = true;
                this.Activate();
                this.WindowState = WindowState.Normal;
                this.Topmost = false;
            }
        }

        public void updateDisplay() {
            int totalMinutes = (int)_timeRemaining.TotalMinutes;
            int seconds = _timeRemaining.Seconds;
            txtTimer.Text = $"{totalMinutes:D2}:{seconds:D2}";
        }

        public void timeReset() {
            _timer.Stop();
            _timeRemaining = _defaultTime;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            timeReset();
            isRunning = false;
            isPlayingAlarm = false;
            btnStart.Content = "Start";
            updateDisplay();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!isRunning) {
                _timer.Start();
                isRunning = true;
                btnStart.Content = "Pause";
            }
            else{ 
                _timer.Stop();
                btnStart.Content = "Continue";
                isRunning = false;
            }
        }

        private void btnsetTime_Click(object sender, RoutedEventArgs e)
        {
            setTimeWindow setTime = new setTimeWindow();
            bool? result = setTime.ShowDialog();
            if (result == true)
            {
                _timeRemaining = TimeSpan.FromMinutes(setTime.selectedMinutes);
                this.view = setTime.selectedMinutes;
                updateDisplay();
            }
        }
    }
}