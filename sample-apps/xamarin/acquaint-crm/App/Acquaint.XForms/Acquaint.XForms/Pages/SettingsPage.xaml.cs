using System;
using System.Collections.Generic;
using FormsToolkit;
using Xamarin.Forms;

namespace Acquaint.XForms
{
	public partial class SettingsPage : ContentPage
	{
		protected SettingsViewModel ViewModel => BindingContext as SettingsViewModel;

		public SettingsPage()
		{
			InitializeComponent();

			MessagingService.Current.Subscribe(MessageKeys.DataPartitionPhraseValidation, (service) => {
				DataPartitionPhraseEntry.PlaceholderColor = Color.Red;
				DataPartitionPhraseEntry.Focus();
			});

			MessagingService.Current.Subscribe(MessageKeys.BackendUrlValidation, (service) => {
				BackendServiceUrlEntry.PlaceholderColor = Color.Red;
			});
		}
	}
}

