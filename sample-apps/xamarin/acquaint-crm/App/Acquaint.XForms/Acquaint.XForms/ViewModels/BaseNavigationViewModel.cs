using MvvmHelpers;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Acquaint.XForms
{
	/// <summary>
	/// Implements the INavigation interface on top of BaseViewModel.
	/// </summary>
	public abstract class BaseNavigationViewModel : BaseViewModel, INavigation
	{
		INavigation _Navigation => Application.Current?.MainPage?.Navigation;

	    #region INavigation implementation

		public void RemovePage(Page page)
		{
			_Navigation?.RemovePage(page);
		}

		public void InsertPageBefore(Page page, Page before)
		{
			_Navigation?.InsertPageBefore(page, before);
		}

		public async Task PushAsync(Page page)
		{
			var task = _Navigation?.PushAsync(page);
			if (task != null)
				await task;
		}

		public async Task<Page> PopAsync()
		{
			var task = _Navigation?.PopAsync();
			return task != null ? await task : await Task.FromResult(null as Page);
		}

		public async Task PopToRootAsync()
		{
			var task = _Navigation?.PopToRootAsync();
			if (task != null)
				await task;
		}

		public async Task PushModalAsync(Page page)
		{
			var task = _Navigation?.PushModalAsync(page);
			if (task != null)
				await task;
		}

		public async Task<Page> PopModalAsync()
		{
			var task = _Navigation?.PopModalAsync();
			return task != null ? await task : await Task.FromResult(null as Page);
		}

		public async Task PushAsync(Page page, bool animated)
		{
			var task = _Navigation?.PushAsync(page, animated);
			if (task != null)
				await task;
		}

		public async Task<Page> PopAsync(bool animated)
		{
			var task = _Navigation?.PopAsync(animated);
			return task != null ? await task : await Task.FromResult(null as Page);
		}

		public async Task PopToRootAsync(bool animated)
		{
			var task = _Navigation?.PopToRootAsync(animated);
			if (task != null)
				await task;
		}

		public async Task PushModalAsync(Page page, bool animated)
		{
			var task = _Navigation?.PushModalAsync(page, animated);
			if (task != null)
				await task;
		}

		public async Task<Page> PopModalAsync(bool animated)
		{
			var task = _Navigation?.PopModalAsync(animated);
			return task != null ? await task : await Task.FromResult(null as Page);
		}

		public IReadOnlyList<Page> NavigationStack => _Navigation?.NavigationStack;

	    public IReadOnlyList<Page> ModalStack => _Navigation?.ModalStack;

	    #endregion
	}
}

