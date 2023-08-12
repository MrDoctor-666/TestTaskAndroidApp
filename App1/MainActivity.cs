using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Android.Widget;
using Java.Util;
using System.Collections.Generic;
using Android.Content;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ListView listView;
        private TextView textView;
        private DataLoader dataLoader;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            textView = FindViewById<TextView>(Resource.Id.textView1);
            textView.Visibility = ViewStates.Invisible;

            listView = FindViewById<ListView>(Resource.Id.listView1);
            dataLoader = new DataLoader();

            LoadDataToListView();

            listView.ItemClick += OnListViewClick;

        }

        public async void LoadDataToListView()
        {
            string xmlData = await WebLoader.get();
            List<string> data = dataLoader.LoadIDs(xmlData);

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, data);
            listView.Adapter = adapter;

        }

        public void OnListViewClick(object sender, AdapterView.ItemClickEventArgs args)
        {
            string data = dataLoader.FindDataByIndex(args.Position);

            textView.Visibility = ViewStates.Visible;
            textView.Text = data;
        }
	}
}
