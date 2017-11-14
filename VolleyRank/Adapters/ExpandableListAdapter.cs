using System.Collections.Generic;
using Android.App;
using Android.Content;
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
        private IList<Ranking> rankingList;

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
            var view = convertView;

            if (view == null)
            {
                var inflater = context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                view = inflater.Inflate(Resource.Layout.RankingListExpandItem, null);
            }

            var child = rankingList[groupPosition].ExtraInfo[childPosition];
            var label = child.Split('_')[0];
            var value = child.Split('_')[1];

            view.FindViewById<TextView>(Resource.Id.child_label).Text = label;
            view.FindViewById<TextView>(Resource.Id.child_value).Text = value;

            return view;
        }

        public override Object GetGroup(int groupPosition)
        {
            return null;
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                var inflater = context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                view = inflater.Inflate(Resource.Layout.RankingListItem, null);
            }

            var ranking = rankingList[groupPosition];

            view.FindViewById<TextView>(Resource.Id.team).Text = $"{ranking.Position} {ranking.TeamName.ToTitleCase()}";
            view.FindViewById<TextView>(Resource.Id.points).Text = ranking.TotalPoints.ToString();

            return view;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

        public override int GroupCount => rankingList.Count;

        public override bool HasStableIds
        {
            get { return true; }
        }
    }
}