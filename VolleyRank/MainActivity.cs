using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;

using VolleyRank.Adapters;
using VolleyRank.Models;
using VolleyRank.Utilities;

namespace VolleyRank
{
    [Activity(Label = "VolleyRank", MainLauncher = true)]
    public class MainActivity : Activity
    {
        ExpandableListAdapter rankingAdapter;
        ExpandableListView rankingListView;
        List<Ranking> profielen;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //var stream = Assets.Open("testdata.xml");
            //var result = XmlDeserializer.DeserialzeStanding(stream);

            var result = DataImport.GetStandingFromLeague("H1GH");

            rankingAdapter = new ExpandableListAdapter(this, result.Rankings);
            rankingListView = FindViewById<ExpandableListView>(Resource.Id.ranking_list);
            rankingListView.SetAdapter(rankingAdapter);
        }
    }
}

