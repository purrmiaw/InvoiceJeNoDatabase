using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Content;
using InvoiceJe.Droid.Fragments;
using InvoiceJe.Droid.Activities;
using Android.Views;

namespace InvoiceJe.Droid
{
	[Activity (Label = "InvoiceJe.Android", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/MasterTheme")]
	public class MainActivity : AppCompatActivity
	{
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.main_activity);
          
            // Setup toolbar
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitle(Resource.String.applicationname); // Set toolbar title here

            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(false);
            }           

            // Setup FloatingActionButton click
            FloatingActionButton navigateToCreateInvoicesActivityFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.floatingactionbutton_navigatetoinvoicescreateviewmodel);
            navigateToCreateInvoicesActivityFloatingActionButton.Click +=
                delegate
                {
                    Intent intent = new Intent(this, typeof(InvoicesCreateActivity));
                    StartActivity(intent);
                };

            // Setup BottomNavigationView clicks 
            BottomNavigationView bottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            bottomNavigationView.NavigationItemSelected += (sender, e) =>
            {
                Android.Support.V4.App.FragmentTransaction fragmentTransaction =
                    this.SupportFragmentManager.BeginTransaction();
                fragmentTransaction.SetCustomAnimations(Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out);

                switch (e.Item.ItemId)
                {
                    case Resource.Id.bottomnavigationview_invoices:
                        InvoicesFragment invoicesFragment = new InvoicesFragment();
                        fragmentTransaction.Replace(Resource.Id.main_container, invoicesFragment);
                        fragmentTransaction.Commit();

                        // show the floating action button
                        navigateToCreateInvoicesActivityFloatingActionButton.Show();
                        break;

                    case Resource.Id.bottomnavigationview_configuration:
                        ConfigurationFragment configurationFragment = new ConfigurationFragment();
                        fragmentTransaction.Replace(Resource.Id.main_container, configurationFragment);
                        fragmentTransaction.Commit();

                        // hide the floating action button
                        navigateToCreateInvoicesActivityFloatingActionButton.Hide();

                        break;

                }
            };

            // OnCreate only run once when this activity is created. So let's set the default
            // fragment to show by 'clicking' on the bottomnavigationview_invoices button.
            View bottomNavigationViewInvoicesButton = bottomNavigationView.FindViewById(Resource.Id.bottomnavigationview_invoices);
            bottomNavigationViewInvoicesButton.PerformClick();

        }

    }
}