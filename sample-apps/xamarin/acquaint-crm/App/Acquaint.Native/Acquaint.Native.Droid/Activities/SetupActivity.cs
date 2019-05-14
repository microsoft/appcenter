using System;
using Acquaint.Util;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Acquaint.Native.Droid
{
	[Activity(Label = "SetupActivity")]
	public class SetupActivity : AppCompatActivity
	{
		View _MainLayout;
		EditText _DataPartitionPhraseField;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			_MainLayout = LayoutInflater.Inflate(Resource.Layout.Setup, null);
				
			// set the content view
			SetContentView(_MainLayout);

			// setup the action bar
			SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));

			// ensure that the system bar color gets drawn
			Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

			// set the title of both the activity and the action bar
			Title = SupportActionBar.Title = "Setup";

			SetupViews();
		}

		void SetupViews()
		{
			// inflate the content layout
			var contentLayout = _MainLayout.FindViewById<LinearLayout>(Resource.Id.setupContentLayout);

			_DataPartitionPhraseField = contentLayout.InflateAndBindEditText(Resource.Id.setupDataPartitionPhraseField, Settings.DataPartitionPhrase);

			var continueButton = contentLayout.InflateAndBindButton(Resource.Id.setupContinueButton);

			contentLayout.InflateAndBindTextView(Resource.Id.setupInstructionsTextView, "The phrase is a key that will separate your data from everyone else's data. Make it unique.\n\nEnter this same phrase on any device running Acquaint that you'd like to sync data with.");

			continueButton.Click += ContinueButtonClick;
		}

		void ContinueButtonClick(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(_DataPartitionPhraseField.Text))
			{
				_DataPartitionPhraseField.SetHintTextColor(Color.Red);

				return;
			}

			Settings.DataPartitionPhrase = _DataPartitionPhraseField.Text;

			base.OnBackPressed();
		}

		public override void OnBackPressed()
		{
			// overriding and ommitting base call to prevent back navigation
		}
	}
}

