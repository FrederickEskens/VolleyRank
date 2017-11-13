using Android.App;
using Android.OS;

using VolleyRank.Utilities;

namespace VolleyRank
{
    [Activity(Label = "VolleyRank", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var stream = Assets.Open("testdata.xml");

            var result = XmlDeserializer.DeserialzeStanding(stream);
        }
    }
}

