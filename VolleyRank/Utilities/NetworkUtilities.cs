using Android.Content;
using Android.Telephony;

namespace VolleyRank.Utilities
{
    public static class NetworkUtilities
    {
        public static string GetNetworkType(Context context)
        {
            var telephonyManager = (TelephonyManager) context.GetSystemService(Context.TelephonyService);
            var networkType = telephonyManager.NetworkType;
            switch (networkType)
            {
                case NetworkType.Gprs:
                case NetworkType.Edge:
                case NetworkType.Cdma:
                case NetworkType.OneXrtt:
                case NetworkType.Iden:
                    return "2G";
                case NetworkType.Umts:
                case NetworkType.Evdo0:
                case NetworkType.EvdoA:
                case NetworkType.Hsdpa:
                case NetworkType.Hsupa:
                case NetworkType.EvdoB:
                case NetworkType.Ehrpd:
                case NetworkType.Hspap:
                    return "3G";
                case NetworkType.Lte:
                    return "4G";
                default:
                    return "Unknown";
            }
        }
    }
}