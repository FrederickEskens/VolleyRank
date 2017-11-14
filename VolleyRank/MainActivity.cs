using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    public class MainActivity : Activity, SwipeRefreshLayout.IOnRefreshListener
    {
        ExpandableListAdapter rankingAdapter;
        ExpandableListView rankingListView;
        List<Ranking> profielen;
        private SwipeRefreshLayout swipeLayout;
        private Standing data;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //var stream = Assets.Open("testdata.xml");
            //var result = XmlDeserializer.DeserialzeStanding(stream);

            data = DataImport.GetStandingFromLeague("H1GH");

            rankingAdapter = new ExpandableListAdapter(this, data.Rankings);
            rankingListView = FindViewById<ExpandableListView>(Resource.Id.ranking_list);
            rankingListView.SetAdapter(rankingAdapter);

            swipeLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipe_container);
            swipeLayout.SetOnRefreshListener(this);
        }

        public void OnRefresh()
        {
            //TODO: look into animation (when slower)
            swipeLayout.Refreshing = true;

            Task.Factory.StartNew(() =>
            {
                data = DataImport.GetStandingFromLeague("H1GH");
                rankingAdapter.UpdateData(data.Rankings);
                rankingAdapter.NotifyDataSetChanged();
            });

            swipeLayout.Refreshing = false;
        }
    }
}

