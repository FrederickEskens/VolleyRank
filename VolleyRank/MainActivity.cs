using System;

using Android.App;
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            //var stream = Assets.Open("testdata.xml");
            //var result = XmlDeserializer.DeserialzeStanding(stream);

            data = DataImport.GetStandingFromLeague("H1GH");

            rankingAdapter = new ExpandableListAdapter(this, data.Rankings);
            rankingListView = FindViewById<ExpandableListView>(Resource.Id.ranking_list);
            rankingListView.SetAdapter(rankingAdapter);

            swipeLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_container);
            swipeLayout.SetColorSchemeColors(Resource.Color.volleyrank_primary, Resource.Color.volleyrank_primarydark);
            swipeLayout.Refresh += HandleRefresh;
        }

        private async void HandleRefresh(object sender, EventArgs e)
        {
            data = await DataImport.GetStandingFromLeagueAsync("H1GH");
            swipeLayout.Refreshing = false;
        }
    }
}

