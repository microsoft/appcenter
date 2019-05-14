using System;
using Acquaint.Abstractions;

namespace Acquaint.Common.iOS
{
	public class DatastoreFolderPathProvider : IDatastoreFolderPathProvider
	{
		public string GetPath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		}
	}
}

