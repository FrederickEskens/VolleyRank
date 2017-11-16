using System;

using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Widget;

using VolleyRank.Adapters;
using VolleyRank.Database;
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

        private VolleyRankDatabase db;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            db = new VolleyRankDatabase();

            //var stream = Assets.Open("testdata.xml");
            //var result = XmlConvert.DeserialzeStanding(stream);

            //data = DataImport.GetStandingFromWebService("H1GH");
            data = DataImport.GetStandingFromCache("H1GH");
                
            rankingAdapter = new ExpandableListAdapter(this, data.Rankings);
            rankingListView = FindViewById<ExpandableListView>(Resource.Id.ranking_list);
            rankingListView.SetAdapter(rankingAdapter);
            //rankingListView.SetChildDivider();

            swipeLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_container);
            swipeLayout.SetColorSchemeColors(Resource.Color.volleyrank_primary, Resource.Color.volleyrank_primarydark);
            swipeLayout.Refresh += HandleRefresh;
        }

        private async void HandleRefresh(object sender, EventArgs e)
        {
            data = await DataImport.GetStandingFromWebServiceAsync("H1GH");
            swipeLayout.Refreshing = false;
        }
    }
}

