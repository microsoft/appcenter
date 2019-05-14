using System;
using Acquaint.ModelContracts;

namespace Acquaint.Abstractions
{
	/// <summary>
	/// A generically-typed EventArgs class. 
	/// Contains the local item queued to be pushed, and the backend service item that the local item is in conflict with.
	/// </summary>
	public class DataSyncErrorEventArgs<T> : EventArgs
	{
		public DataSyncErrorEventArgs(T localQueuedItem, T conflictedServiceItem)
		{
			_LocalQueuedItem = localQueuedItem;
			_ConflictedServiceItem = conflictedServiceItem;
		}

		private T _LocalQueuedItem;
		public T LocalQueuedItem
		{
			get { return _LocalQueuedItem; }
		}

		private T _ConflictedServiceItem;
		public T ConflictedServiceItem
		{
			get { return _ConflictedServiceItem; }
		}
	}
}

