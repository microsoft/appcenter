namespace Acquaint.Abstractions
{
	/// <summary>
	/// A service that exposes whether or not certain device capabilities are available.
	/// </summary>
    public interface ICapabilityService
    {
		/// <summary>
		/// Gets a bool representing whether or not the device can make calls.
		/// </summary>
		/// <value>A bool representing whether or not the device can make calls.</value>
		bool CanMakeCalls { get; }

		/// <summary>
		/// Gets a bool representing whether or not the device can send messages.
		/// </summary>
		/// <value>A bool representing whether or not the device can send messages.</value>
		bool CanSendMessages { get; }

		/// <summary>
		/// Gets a bool representing whether or not the device cansend email.
		/// </summary>
		/// <value>A bool representing whether or not the device cansend email.</value>
		bool CanSendEmail { get; }
    }
}

