using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Widget;
using VolleyRank.Database;
using VolleyRank.Models;
using VolleyRank.Utilities;

namespace VolleyRank
{
    [Activity(Label = "VolleyRank", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private string selectedSerieCode;
        private IList<string> seriesNames;
        private IList<Serie> series;
        private VolleyRankDatabase database;

        private ConnectivityManager connectivityManager;

        private Spinner spinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            database = new VolleyRankDatabase();

            spinner = FindViewById<Spinner>(Resource.Id.spinner);

            var button = FindViewById<Button>(Resource.Id.button);

            series = GetSeries("AH-2180").SerieList.Where(x => x.Type == "punten").ToList();
            seriesNames = series.OrderBy(x => x.Name).Select(x => x.Name).ToList();

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, seriesNames);
            spinner.Adapter = adapter;
            SetPreselectedSerie();

            spinner.ItemSelected += spinner_ItemSelected;
            button.Click += button_Clicked;
        }

        private void SetPreselectedSerie()
        {
            var lastSelectedSerie = database.GetPreference("last_selected_serie");

            if (!string.IsNullOrEmpty(lastSelectedSerie))
            {
                var serie = series.FirstOrDefault(x => x.Code == lastSelectedSerie)?.Name;
                var index = seriesNames.IndexOf(serie);
                spinner.SetSelection(index);
            }
        }

        private Series GetSeries(string clubId)
        {
            var networkInfo = connectivityManager.ActiveNetworkInfo;
            Series result;

            if (networkInfo == null || !networkInfo.IsConnected)
            {
                result = DataImport.GetSeriesFromCache(clubId, out var timeStamp);
            }
            else
            {
                try
                {
                    result = DataImport.GetSeriesFromWebService(clubId);
                }
                catch (Exception e)
                {
                    var message = e.Message;
                    result = DataImport.GetSeriesFromCache(clubId, out var timeStamp);
                }
            }

            return result;
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var selectedSerie = series.FirstOrDefault(x => x.Name == seriesNames[e.Position]);
            selectedSerieCode = selectedSerie?.Code;
        }

        private void button_Clicked(object sender, EventArgs e)
        {
            var rankingActivity = new Intent(this, typeof(RankingActivity));
            rankingActivity.PutExtra("seriesCode", selectedSerieCode);
            database.SavePreference("last_selected_serie", selectedSerieCode);
            StartActivity(rankingActivity);
        }
    }
}