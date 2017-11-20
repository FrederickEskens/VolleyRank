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
    [Activity(Label = "VolleyRank", MainLauncher = true)]
    public class MainActivity : Activity
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

            SetContentView(Resource.Layout.Main);
            connectivityManager = (ConnectivityManager) GetSystemService(ConnectivityService);
            toast = Toast.MakeText(this, "", ToastLength.Long);

            data = GetStanding("H1GH");

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

            if (networkInfo == null || !networkInfo.IsConnected || NetworkUtilities.GetNetworkType(ApplicationContext) == "2G")
            {
                result = GetStandingFromCache(league);
            }
            else
            {
                result = DataImport.GetStandingFromWebService(league);
            }

            return result;
        }

        private async Task<Standing> GetStandingAsync(string league)
        {
            var networkInfo = connectivityManager.ActiveNetworkInfo;
            Standing result;

            if (networkInfo == null || !networkInfo.IsConnected || NetworkUtilities.GetNetworkType(ApplicationContext) == "2G")
            {
                result = GetStandingFromCache(league);
            }
            else
            {
                result = await DataImport.GetStandingFromWebServiceAsync("H1GH");
            }

            return result;
        }

        private Standing GetStandingFromCache(string league)
        {
            var result = DataImport.GetStandingFromCache(league, out var timeStamp);
            var age = DateTime.Now - timeStamp;
            toast.SetText($"Geen verbinding. Laatste data van {age.ToTimeString()} geleden.");
            toast.Show();

            return result;
        }
    }
}