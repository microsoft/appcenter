using System;
using Acquaint.XForms;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TableView), typeof(Acquaint.XForms.iOS.StandardTableViewRenderer))]

namespace Acquaint.XForms.iOS
{
	public class StandardTableViewRenderer : TableViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<TableView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
				return;

			if ((e.NewElement != null && e.NewElement.StyleId == "NoSeparator") || (e.OldElement != null && e.OldElement.StyleId == "NoSeparator"))
			{ 
				(Control as UITableView).SeparatorStyle = UITableViewCellSeparatorStyle.None;
			}
		}
	}
}

