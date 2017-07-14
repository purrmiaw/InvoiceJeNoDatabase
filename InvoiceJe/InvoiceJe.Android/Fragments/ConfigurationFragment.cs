using Android.OS;
using Android.Views;
using Android.Support.V4.App;

namespace InvoiceJe.Droid.Fragments
{
    public class ConfigurationFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.configuration_fragment, container, false);
            return view;
        }
    }
}