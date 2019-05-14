using System;
using System.Threading;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace Acquaint.Native.UITest.Android
{
	[TestFixture]
	public class UpdateDataTests
	{
		AndroidApp app;

		[SetUp]
		public void BeforeEachTest()
		{
			// TODO: If the Android app being tested is included in the solution then open
			// the Unit Tests window, right click Test Apps, select Add App Project
			// and select the app projects that should be tested.
			//
			// ^^^ THIS IS THE WAY THE ACQUAINT APP IS SETUP FOR UITESTS ^^^

			app = ConfigureApp
				.Android

				// TODO: Update this path to point to your Android app and uncomment the
				// code if the app is not included in the solution.
				//.ApkFile ("../../../Android/bin/Debug/UITestsAndroid.apk")
				//
				// ^^^ THIS IS *NOT* THE WAY THE ACQUAINT APP IS SETUP FOR UITESTS ^^^
				.StartApp();
		}

		[Test]
		public void UpdateFirstName()
		{
			app.Screenshot("App Started");
			app.Tap(x => x.Id("setupDataPartitionPhraseField"));
			app.EnterText("UseLocalDataSource");
			app.Screenshot("Entered data parition phrase");
			app.Tap(x => x.Id("setupContinueButton"));
			app.WaitForElement(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait a few seconds for list images to fully load
			app.Screenshot("Display list");
			app.Tap(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Detail screen");
			app.Tap(x => x.Id("acquaintanceEditButton"));
			app.Screenshot("Edit screen");
			app.ScrollDownTo("First");
			app.Tap(x => x.Id("firstNameField"));
			app.ClearText();
			app.Screenshot("Cleared first name field");
			app.EnterText("Jonathan");
			app.DismissKeyboard();
			app.Screenshot("Altered value of first name field");
			app.Tap(x => x.Id("acquaintanceSaveButton"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Saved changes, navigated to detail screen, first name updated");
			app.Tap(x => x.Marked("Navigate up"));
			app.Screenshot("First name updated on list screen");
		}

		[Test]
		public void UpdateLastName()
		{
			app.Screenshot("App Started");
			app.Tap(x => x.Id("setupDataPartitionPhraseField"));
			app.EnterText("UseLocalDataSource");
			app.Screenshot("Entered data parition phrase");
			app.Tap(x => x.Id("setupContinueButton"));
			app.WaitForElement(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait a few seconds for list images to fully load
			app.Screenshot("Display list");
			app.Tap(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Detail screen");
			app.Tap(x => x.Id("acquaintanceEditButton"));
			app.Screenshot("Edit screen");
			app.ScrollDownTo("Last");
			app.Tap(x => x.Id("lastNameField"));
			app.ClearText();
			app.Screenshot("Cleared last name field");
			app.EnterText("DuBois");
			app.DismissKeyboard();
			app.Screenshot("Altered value of last name field");
			app.Tap(x => x.Id("acquaintanceSaveButton"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Saved changes, navigated to detail screen, last name updated");
			app.Tap(x => x.Marked("Navigate up"));
			app.Screenshot("Last name updated on list screen, last name updated");
		}

		[Test]
		public void UpdateCompanyName()
		{
			app.Screenshot("App Started");
			app.Tap(x => x.Id("setupDataPartitionPhraseField"));
			app.EnterText("UseLocalDataSource");
			app.Screenshot("Entered data parition phrase");
			app.Tap(x => x.Id("setupContinueButton"));
			app.WaitForElement(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait a few seconds for list images to fully load
			app.Screenshot("Display list");
			app.Tap(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Detail screen");
			app.Tap(x => x.Id("acquaintanceEditButton"));
			app.Screenshot("Edit screen");
			app.Screenshot("Edit screen");
			app.ScrollDownTo("Company");
			app.Tap(x => x.Id("companyField"));
			app.ClearText();
			app.Screenshot("Cleared company name field");
			app.EnterText("Bay Shipping Inc");
			app.DismissKeyboard();
			app.Screenshot("Altered value of company name field");
			app.Tap(x => x.Id("acquaintanceSaveButton"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Saved changes, navigated to detail screen, company updated");
			app.Tap(x => x.Marked("Navigate up"));
			app.Screenshot("Company name updated on list screen, company updated");
		}

		[Test]
		public void UpdateTitle()
		{
			app.Screenshot("App Started");
			app.Tap(x => x.Id("setupDataPartitionPhraseField"));
			app.EnterText("UseLocalDataSource");
			app.Screenshot("Entered data parition phrase");
			app.Tap(x => x.Id("setupContinueButton"));
			app.WaitForElement(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait a few seconds for list images to fully load
			app.Screenshot("Display list");
			app.Tap(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Detail screen");
			app.Tap(x => x.Id("acquaintanceEditButton"));
			app.Screenshot("Edit screen");
			app.ScrollDownTo("Title");
			app.Tap(x => x.Id("jobTitleField"));
			app.ClearText();
			app.Screenshot("Cleared title field");
			app.EnterText("COO");
			app.DismissKeyboard();
			app.Screenshot("Altered value of title field");
			app.Tap(x => x.Id("acquaintanceSaveButton"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Saved changes, navigated to detail screen, title updated");
		}

		[Test]
		public void UpdatePhoneNumber()
		{
			app.Screenshot("App Started");
			app.Tap(x => x.Id("setupDataPartitionPhraseField"));
			app.EnterText("UseLocalDataSource");
			app.Screenshot("Entered data parition phrase");
			app.Tap(x => x.Id("setupContinueButton"));
			app.WaitForElement(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait a few seconds for list images to fully load
			app.Screenshot("Display list");
			app.Tap(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Detail screen");
			app.Tap(x => x.Id("acquaintanceEditButton"));
			app.Screenshot("Edit screen");
			app.ScrollDownTo("Phone");
			app.Tap(x => x.Id("phoneNumberField"));
			app.ClearText();
			app.Screenshot("Cleared phone number field");
			app.EnterText("9257878888");
			app.DismissKeyboard();
			app.Screenshot("Altered value of phone number field");
			app.Tap(x => x.Id("acquaintanceSaveButton"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Saved changes, navigated to detail screen, phone updated");
		}

		[Test]
		public void UpdateEmailAddress()
		{
			app.Screenshot("App Started");
			app.Tap(x => x.Id("setupDataPartitionPhraseField"));
			app.EnterText("UseLocalDataSource");
			app.Screenshot("Entered data parition phrase");
			app.Tap(x => x.Id("setupContinueButton"));
			app.WaitForElement(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait a few seconds for list images to fully load
			app.Screenshot("Display list");
			app.Tap(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Detail screen");
			app.Tap(x => x.Id("acquaintanceEditButton"));
			app.Screenshot("Edit screen");
			app.ScrollDownTo("Email");
			app.Tap(x => x.Id("emailField"));
			app.ClearText();
			app.Screenshot("Cleared email field");
			app.EnterText("earmstead@bayshipping.com");
			app.DismissKeyboard();
			app.Screenshot("Altered value of email field");
			app.Tap(x => x.Id("acquaintanceSaveButton"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Saved changes, navigated to detail screen, email updated");
		}

		[Test]
		public void UpdateMailingAddress()
		{
			app.Screenshot("App Started");
			app.Tap(x => x.Id("setupDataPartitionPhraseField"));
			app.EnterText("UseLocalDataSource");
			app.Screenshot("Entered data parition phrase");
			app.Tap(x => x.Id("setupContinueButton"));
			app.WaitForElement(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait a few seconds for list images to fully load
			app.Screenshot("Display list");
			app.Tap(x => x.Marked("Armstead, Evan"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Detail screen");
			app.Tap(x => x.Id("acquaintanceEditButton"));
			app.Screenshot("Edit screen");

			app.ScrollDownTo("Street");
			app.Tap(x => x.Id("streetField"));
			app.ClearText();
			app.Screenshot("Cleared street field");
			app.EnterText("1395 Middle Harbor Rd");
			app.DismissKeyboard();
			app.Screenshot("Altered value of street field");

			app.ScrollDownTo("City");
			app.Tap(x => x.Id("cityField"));
			app.ClearText();
			app.Screenshot("Cleared city field");
			app.EnterText("Oakland");
			app.DismissKeyboard();
			app.Screenshot("Altered value of city field");

			app.ScrollDownTo("ZIP");
			app.Tap(x => x.Id("zipField"));
			app.ClearText();
			app.Screenshot("Cleared ZIP field");
			app.EnterText("94612");
			app.DismissKeyboard();
			app.Screenshot("Altered value of ZIP field");

			app.Tap(x => x.Id("acquaintanceSaveButton"));
			Thread.Sleep(3000); // wait 3 seconds to give map time to fully render
			app.Screenshot("Saved changes, navigated to detail screen, address updated");
		}
	}
}

