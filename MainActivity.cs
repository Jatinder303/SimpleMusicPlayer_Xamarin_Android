using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Media;
using System.Timers;
using System;
using Android.Content;
using Android.Views;

namespace SimpleMusicPlayer_Xamarin_Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        int count = 1;
        ImageButton btnPlay;
        TextView songTotalDurationLabel;
        SeekBar songProgress;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Get our button from the layout resource,
            // and attach an event to it
            btnPlay = FindViewById<ImageButton>(Resource.Id.btnPlay);
            songTotalDurationLabel = FindViewById<TextView>(Resource.Id.songTotalDurationLabel);
            songProgress = FindViewById<SeekBar>(Resource.Id.songProgressBar);

            btnPlay.Click += BtnPlay_Click;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            MediaPlayer player;
            player = MediaPlayer.Create(this, Resource.Raw.Dancing);
            player.Start();


            double time, minutes;
            int minint, seconds, onlyseconds;

            time = player.Duration;



            minutes = (time / (1000 * 60));

            minint = (int)(minutes);

            seconds = (int)((time % (1000 * 60 * 60)) % (1000 * 60) / 1000);

            songTotalDurationLabel.Text = minint + "." + seconds;

            onlyseconds = (int)((time / (1000)));
            songProgress.Max = onlyseconds;

            Timer timer = new Timer(1000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            RunOnUiThread(() => songProgress.Progress += 1);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}