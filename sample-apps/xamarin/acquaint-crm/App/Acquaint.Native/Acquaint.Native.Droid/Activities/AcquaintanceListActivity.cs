using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acquaint.Abstractions;
using Acquaint.Models;
using Acquaint.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Transformations;
using FFImageLoading.Views;
using Microsoft.Practices.ServiceLocation;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Acquaint.Native.Droid
{
	/// <summary>
	/// Acquaintance list activity.
	/// </summary>
	[Activity]
	public class AcquaintanceListActivity : AppCompatActivity
	{
		AcquaintanceCollectionAdapter _Adapter;

		SwipeRefreshLayout _SwipeRefreshLayout;

		// This override is called only once during the activity's lifecycle, when it is created.
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// instantiate adapter
			_Adapter = new AcquaintanceCollectionAdapter();

			// instantiate the layout manager
			var layoutManager = new LinearLayoutManager(this);

			// set the content view
			SetContentView(Resource.Layout.AcquaintanceList);

			// setup the action bar
			SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));

			// ensure that the system bar color gets drawn
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			// set the title of both the activity and the action bar
			Title = SupportActionBar.Title = "Acquaintances";

			_SwipeRefreshLayout = (SwipeRefreshLayout)FindViewById(Resource.Id.acquaintanceListSwipeRefreshContainer);

			_SwipeRefreshLayout.Refresh += async (sender, e) => { await LoadAcquaintances(); };

			_SwipeRefreshLayout.Post(() => _SwipeRefreshLayout.Refreshing = true);

			// instantiate/inflate the RecyclerView
			var recyclerView = (RecyclerView)FindViewById(Resource.Id.acquaintanceRecyclerView);

			// set RecyclerView's layout manager 
			recyclerView.SetLayoutManager(layoutManager);

			// set RecyclerView's adapter
			recyclerView.SetAdapter(_Adapter);

			var addButton = (FloatingActionButton)FindViewById(Resource.Id.acquaintanceListFloatingActionButton);

			addButton.Click += (sender, e) => {
				StartActivity(new Intent(this, typeof(AquaintanceEditActivity)));
			};
		}

		protected override async void OnResume()
		{
			base.OnResume();

			if (string.IsNullOrWhiteSpace(Settings.DataPartitionPhrase))
			{
				StartActivity(new Intent(this, typeof(SetupActivity)));
			}
			else
			{
				await LoadAcquaintances();
			}
		}

		async Task LoadAcquaintances()
		{
			_SwipeRefreshLayout.Refreshing = true;

			try
			{
				// load the items
				await _Adapter.LoadAcquaintances();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Error getting acquaintances: {ex.Message}");

				//set alert for executing the task
				var alert = new Android.App.AlertDialog.Builder(this);

				alert.SetTitle("Error getting acquaintances");

				alert.SetMessage("Ensure you have a network connection, and that a valid backend service URL is present in the app settings.");

				alert.SetNegativeButton("OK", (senderAlert, args) => {
					// an empty delegate body, because we just want to close the dialog and not take any other action
				});

				//run the alert in UI thread to display in the screen
				RunOnUiThread(() => {
					alert.Show();
				});
			}
			finally
			{
				_SwipeRefreshLayout.Refreshing = false;
			}
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.AcquaintanceListMenu, menu);

			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item != null)
			{
				switch (item.ItemId)
				{
					case Resource.Id.settingsButton:
					StartActivity(new Intent(this, typeof(SettingsActivity)));
					break;
				}
			}

			return base.OnOptionsItemSelected(item);
		}
	}

	/// <summary>
	/// Acquaintance view holder. Used in conjunction with RecyclerView's view holder methods to improve performance by not re-inflating views each time they're needed.
	/// </summary>
	internal class AcquaintanceViewHolder : RecyclerView.ViewHolder
	{
		public View AcquaintanceRow { get; }

		public TextView NameTextView { get; }

		public TextView CompanyTextView { get; }

		public TextView JobTitleTextView { get; }

		public ImageViewAsync ProfilePhotoImageView { get; }

		public AcquaintanceViewHolder(View itemView) : base(itemView)
		{
			AcquaintanceRow = itemView;

			NameTextView = AcquaintanceRow.FindViewById<TextView>(Resource.Id.nameTextView);
			CompanyTextView = AcquaintanceRow.FindViewById<TextView>(Resource.Id.companyTextView);
			JobTitleTextView = AcquaintanceRow.FindViewById<TextView>(Resource.Id.jobTitleTextView);
			ProfilePhotoImageView = AcquaintanceRow.FindViewById<ImageViewAsync>(Resource.Id.profilePhotoImageView);
		}
	}

	/// <summary>
	/// Acquaintance collection adapter. Coordinates data the child views of RecyclerView.
	/// </summary>
	internal class AcquaintanceCollectionAdapter : RecyclerView.Adapter, View.IOnClickListener
	{
		IDataSource<Acquaintance> _DataSource;

		// the list of items that this adapter uses
		public List<Acquaintance> Acquaintances { get; private set; }

		public AcquaintanceCollectionAdapter()
		{
			Acquaintances = new List<Acquaintance>();

			SetDataSource();
		}

		void SetDataSource()
		{
			_DataSource = ServiceLocator.Current.GetInstance<IDataSource<Acquaintance>>();
		}

		/// <summary>
		/// Loads the acquaintances.
		/// </summary>
		/// <returns>Task.</returns>
		public async Task LoadAcquaintances()
		{
			SetDataSource();

			Acquaintances = (await _DataSource.GetItems()).ToList();

			NotifyDataSetChanged();

			Settings.ClearImageCacheIsRequested = false;
		}

		// when a RecyclerView itemView is requested, the OnCreateViewHolder() is called
		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			// instantiate/inflate a view
			var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AcquaintanceRow, parent, false);

			// Create a ViewHolder to find and hold these view references, and 
			// register OnClick with the view holder:
			var viewHolder = new AcquaintanceViewHolder(itemView);

			return viewHolder;
		}

		// populates the properties of the child views of the itemView
		public override async void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			// instantiate a new view holder
			var viewHolder = holder as AcquaintanceViewHolder;

			// get an item by position (index)
			var acquaintance = Acquaintances[position];

			// assign values to the views' text properties
			if (viewHolder == null) return;

			viewHolder.NameTextView.Text = acquaintance.DisplayLastNameFirst;
			viewHolder.CompanyTextView.Text = acquaintance.Company;
			viewHolder.JobTitleTextView.Text = acquaintance.JobTitle;

			// set the Tag property of the AcquaintanceRow view to the position (index) of the item that is currently being bound. We'll need it later in the OnLick() implementation.
			viewHolder.AcquaintanceRow.Tag = position;

			// set OnClickListener of AcquaintanceRow
			viewHolder.AcquaintanceRow.SetOnClickListener(this);

			if (string.IsNullOrWhiteSpace(acquaintance.SmallPhotoUrl))
				viewHolder.ProfilePhotoImageView.SetImageBitmap(null);
			else
				// use FFImageLoading library to asynchronously:
				await ImageService
					.Instance
					.LoadUrl(acquaintance.SmallPhotoUrl, TimeSpan.FromHours(Settings.ImageCacheDurationHours))  // get the image from a URL
					.LoadingPlaceholder("placeholderProfileImage.png")                                          // specify a placeholder image
					.Transform(new CircleTransformation())                                                      // transform the image to a circle
					.Error(e => System.Diagnostics.Debug.WriteLine(e.Message))
					.IntoAsync(viewHolder.ProfilePhotoImageView);         
		}

		public void OnClick(View view)
		{
			// setup an intent
			var detailIntent = new Intent(view.Context, typeof(AcquaintanceDetailActivity));

			// get an item by position (index)
			var acquaintance = Acquaintances[(int)view.Tag];

			// Add some identifying item data to the intent. In this case, the id of the acquaintance for which we're about to display the detail screen.
			detailIntent.PutExtra(view.Context.Resources.GetString(Resource.String.acquaintanceDetailIntentKey), acquaintance.Id);

			// get a referecne to the profileImageView
			var profileImageView = view.FindViewById(Resource.Id.profilePhotoImageView);

			// shared element transitions are only supported on Android 5.0+
			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
			{
				// define transitions 
				var transitions = new List<Android.Util.Pair>() {
					Android.Util.Pair.Create(profileImageView, view.Context.Resources.GetString(Resource.String.profilePhotoTransition)),
				};

				// create an activity options instance and bind the above-defined transitions to the current activity
				var transistionOptions = ActivityOptions.MakeSceneTransitionAnimation(view.Context as Activity, transitions.ToArray());

				// start (navigate to) the detail activity, passing in the activity transition options we just created
				view.Context.StartActivity(detailIntent, transistionOptions.ToBundle());
			}
			else
			{
				view.Context.StartActivity(detailIntent);
			}
		}

		// Return the number of items
		public override int ItemCount => Acquaintances.Count;
	}
}


