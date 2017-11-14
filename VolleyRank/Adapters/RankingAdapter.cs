using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

using VolleyRank.Models;
using VolleyRank.Utilities;

namespace VolleyRank.Adapters
{
    public class RankingAdapter : BaseAdapter<Ranking>
    {
        private Activity context;
        private IList<Ranking> rankingList;

        public RankingAdapter(Activity context, IList<Ranking> rankingList)
        {
            this.context = context;
            this.rankingList = rankingList;
        }

        public void UpdateData(IList<Ranking> rankingList)
        {
            this.rankingList = rankingList;
        }

        public override Ranking this[int index] => rankingList[index];

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.RankingListItem, null);

            var ranking = rankingList[position];

            view.FindViewById<TextView>(Resource.Id.team).Text = $"{ranking.Position} {ranking.TeamName.ToTitleCase()}";
            view.FindViewById<TextView>(Resource.Id.points).Text = ranking.TotalPoints.ToString();

            return view;
        }

        public override int Count => rankingList.Count;

        public Ranking GetItemAtPosition(int position)
        {
            return rankingList[position];
        }
    }
}