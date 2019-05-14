using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acquaint.Abstractions;
using Acquaint.Models;
using Foundation;
using Microsoft.Practices.ServiceLocation;
using UIKit;

namespace Acquaint.Native.iOS
{
	public class AcquaintanceTableViewSource : UITableViewSource
	{
		/// <summary>
		/// The acquaintance data source.
		/// </summary>
		IDataSource<Acquaintance> _DataSource;

		/// <summary>
		/// Gets the acquaintances.
		/// </summary>
		/// <value>The acquaintances.</value>
		public List<Acquaintance> Acquaintances { get; private set; }

		public AcquaintanceTableViewSource()
		{
			SetDataSource();
		}

		void SetDataSource()
		{
			_DataSource = ServiceLocator.Current.GetInstance<IDataSource<Acquaintance>>();
		}

		// <summary>
		// Loads the acquaintances.
		// </summary>
		// <returns>The acquaintances.</returns>
		public async Task LoadAcquaintances()
		{
			SetDataSource();

			Acquaintances = (await _DataSource.GetItems()).ToList();
		}

		#region implemented abstract members of UITableViewSource

		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// try to get a cell that's currently off-screen, not being displayed
			var cell = tableView.DequeueReusableCell("AcquaintanceCell", indexPath) as AcquaintanceCell;

			// get an item from the collection, using the table row index as the collection index
			var acquaintance = Acquaintances[indexPath.Row];

			cell.Update(acquaintance);

			return cell;
		}

		/// <summary>
		/// Gets the number of rows in a table section.
		/// </summary>
		/// <returns>The number of items in the section.</returns>
		/// <param name="tableview">Tableview.</param>
		/// <param name="section">Section.</param>
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			if (Acquaintances != null)
				return Acquaintances.Count;

			return 0;
		}

		#endregion
	}
}

