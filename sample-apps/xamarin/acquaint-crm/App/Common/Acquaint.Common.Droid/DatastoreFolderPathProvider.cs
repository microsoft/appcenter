using System;
using Acquaint.Abstractions;

namespace Acquaint.Common.Droid
{
	public class DatastoreFolderPathProvider : IDatastoreFolderPathProvider
	{
		public string GetPath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		}
	}
}

