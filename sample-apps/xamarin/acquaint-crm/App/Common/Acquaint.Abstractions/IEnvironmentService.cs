namespace Acquaint.Abstractions
{
	/// <summary>
	/// A service that exposes some platform environment characteristics.
	/// </summary>
    public interface IEnvironmentService
    {
		/// <summary>
		/// Gets a bool representing whether or not is the app is running on a simulator or device.
		/// </summary>
		/// <value>True if the device is real, false if a simulator/emulator.</value>
        bool IsRealDevice { get; }
    }
}

