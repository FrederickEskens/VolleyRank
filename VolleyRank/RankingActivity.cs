using System;
using System.Threading.Tasks;

using Android.App;
using Android.Net;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Widget;

using VolleyRank.Adapters;
using VolleyRank.Models;
using VolleyRank.Utilities;

namespace VolleyRank
{
    [Activity(Label = "VolleyRank")]
    public class RankingActivity : Activity
    {
        private ExpandableListAdapter rankingAdapter;
        private ExpandableListView rankingListView;

        private SwipeRefreshLayout swipeLayout;

        private Standing data;

        private ConnectivityManager connectivityManager;

        private Toast toast;
            
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Ranking);
            connectivityManager = (ConnectivityManager) GetSystemService(ConnectivityService);
            toast = Toast.MakeText(this, "", ToastLength.Long);

            var seriesCode = Intent.GetStringExtra("seriesCode") ?? "H1GH";

            data = GetStanding(seriesCode);

            rankingAdapter = new ExpandableListAdapter(this, data.Rankings);
            rankingListView = FindViewById<ExpandableListView>(Resource.Id.ranking_list);
            rankingListView.SetAdapter(rankingAdapter);

            swipeLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_container);
            swipeLayout.SetColorSchemeColors(Resource.Color.volleyrank_primary, Resource.Color.volleyrank_primarydark);
            swipeLayout.Refresh += HandleRefresh;
        }

        private async void HandleRefresh(object sender, EventArgs e)
        {
            data = await GetStandingAsync("H1GH");
            swipeLayout.Refreshing = false;
        }

        private Standing GetStanding(string league)
        {
            var networkInfo = connectivityManager.ActiveNetworkInfo;
            Standing result;

            if (networkInfo == null || !networkInfo.IsConnected)
            {
                result = GetStandingFromCache(league, "Geen verbinding");
            }
            else
            {
                try
                {
                    result = DataImport.GetStandingFromWebService(league);
                }
                catch (Exception)
                {
                    result = GetStandingFromCache(league, "Slechte verbinding");
                }
            }

            return result;
        }

        private async Task<Standing> GetStandingAsync(string league)
        {
            var networkInfo = connectivityManager.ActiveNetworkInfo;
            Standing result;

            if (networkInfo == null || !networkInfo.IsConnected)
            {
                result = GetStandingFromCache(league, "Geen verbinding");
            }
            else
            {
                try
                {
                    result = await DataImport.GetStandingFromWebServiceAsync(league);
                }
                catch (Exception)
                {
                    result = GetStandingFromCache(league, "Slechte verbinding");
                }
            }

            return result;
        }

        private Standing GetStandingFromCache(string league, string message)
        {
            var result = DataImport.GetStandingFromCache(league, out var timeStamp);
            var age = DateTime.Now - timeStamp;
            toast.SetText($"{message}. Laatste data van {age.ToTimeString()} geleden.");
            toast.Show();

            return result;
        }
    }
}