using Android.Views;
using Android.Widget;
using FFImageLoading.Views;
using FFImageLoading;
using FFImageLoading.Transformations;
using Android.App;
using Acquaint.Util;
using System;

namespace Acquaint.Native.Droid
{
	public static class ViewExtensions
	{
		public static TextView InflateAndBindTextView(this View parentView, int textViewResourceId, string text = null)
		{
			TextView textView = null;

			if (parentView != null)
			{
				textView = parentView.FindViewById<TextView>(textViewResourceId);

				if (textView != null && text != null)
				{
					textView.Text = text;
				}
			}

			return textView;
		}

		public static EditText InflateAndBindEditText(this View parentView, int textViewResourceId, string text = null)
		{
			EditText editText = null;

			if (parentView != null)
			{
				editText = parentView.FindViewById<EditText>(textViewResourceId);

				if (editText != null && text != null)
				{
					editText.Text = text;
				}
			}

			return editText;
		}

		public static ImageViewAsync InflateAndBindRemoteImageView(this View parentView, int imageViewResourceId, string imageUrl, bool async = true)
		{
			ImageViewAsync imageView = null;

			if (parentView != null)
			{
				imageView = parentView.FindViewById<ImageViewAsync>(imageViewResourceId);

				if (imageView != null)
				{
					if (async)
						((Activity)parentView.Context).RunOnUiThread(async () => 
						{
							await ImageService
								.Instance
								.LoadUrl(imageUrl, TimeSpan.FromHours(Settings.ImageCacheDurationHours))
								.LoadingPlaceholder("placeholderProfileImage.png")
								.Transform(new CircleTransformation())
								.Error(e => System.Diagnostics.Debug.WriteLine(e.Message))
								.IntoAsync(imageView);
						});
					else
						ImageService.Instance.LoadUrl(imageUrl).Transform(new CircleTransformation()).Into(imageView);
				}
			}

			return imageView;
		}

		public static ImageView InflateAndBindLocalImageViewByResource(this View parentView, int imageViewResourceId, int resourceId)
		{
			ImageView imageView = null;

			if (parentView != null)
			{
				imageView = parentView.FindViewById<ImageView>(imageViewResourceId);

				imageView.SetImageResource(resourceId);
			}

			return imageView;
		}

		public static Switch InflateAndBindSwitch(this View parentView, int switchResourceId, bool value)
		{
			Switch _switch = null;

			if (parentView != null)
			{
				_switch = parentView.FindViewById<Switch>(switchResourceId);

				if (_switch != null)
				{
					_switch.Checked = value;
				}
			}

			return _switch;
		}

		public static Button InflateAndBindButton(this View parentView, int buttonResourceId, string text = null)
		{
			Button button = null;

			if (parentView != null)
			{
				button = parentView.FindViewById<Button>(buttonResourceId);

				if (button != null && text != null)
				{
					button.Text = text;
				}
			}

			return button;
		}
	}
}

