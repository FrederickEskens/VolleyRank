using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using VolleyRank.Models;
using VolleyRank.Utilities;

namespace VolleyRank.Adapters
{
    public class ExpandableListAdapter : BaseExpandableListAdapter
    {
        private readonly Activity context;
        private readonly IList<Ranking> rankingList;

        public ExpandableListAdapter(Activity context, IList<Ranking> rankingList)
        {
            this.context = context;
            this.rankingList = rankingList;
        }

        public override Object GetChild(int groupPosition, int childPosition)
        {
            return rankingList[groupPosition].ExtraInfo[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return rankingList[groupPosition].ExtraInfo.Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            convertView = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.RankingListExpandItem, null);

            var child = rankingList[groupPosition].ExtraInfo[childPosition];
            var label = child.Split('_')[0];
            var value = child.Split('_')[1];

            convertView.FindViewById<TextView>(Resource.Id.child_label).Text = label;
            convertView.FindViewById<TextView>(Resource.Id.child_value).Text = value;

            return convertView;
        }

        public override Object GetGroup(int groupPosition)
        {
            var ranking = rankingList[groupPosition];
            return $"{ranking.Position} {ranking.TeamName.ToTitleCase()}";
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            convertView = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.RankingListItem, null);

            var ranking = rankingList[groupPosition];

            convertView.FindViewById<TextView>(Resource.Id.team).Text = $"{ranking.Position} {ranking.TeamName.ToTitleCase()}";
            convertView.FindViewById<TextView>(Resource.Id.points).Text = ranking.TotalPoints.ToString();

            return convertView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return false;
        }

        public override int GroupCount => rankingList.Count;

        public override bool HasStableIds => true;
    }
}