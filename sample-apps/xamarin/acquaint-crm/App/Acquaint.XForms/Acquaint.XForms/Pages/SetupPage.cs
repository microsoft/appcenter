using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acquaint.Util;
using Xamarin.Forms;

namespace Acquaint.XForms
{
    public partial class SetupPage : ContentPage
    {
        public SetupPage()
        {
            BindingContext = this; // No need for all the ceremony of a viewmodel in this case. Just bind to ourself.

            InitializeComponent();
        }

        Command _ContinueCommand;

        public Command ContinueCommand => _ContinueCommand ?? (_ContinueCommand = new Command(async () => await ExecuteContinueCommand()));

        async Task ExecuteContinueCommand()
        {
            if (string.IsNullOrWhiteSpace(DataPartitionPhraseEntry.Text))
            {
                DataPartitionPhraseEntry.PlaceholderColor = Color.Red;

                DataPartitionPhraseEntry.Focus();

                return;
            }

            Settings.DataPartitionPhrase = DataPartitionPhraseEntry.Text;

			await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            // disable back button, so that the user is forced to enter a DataPartitionPhrase
            return true;
        }
    }
}

