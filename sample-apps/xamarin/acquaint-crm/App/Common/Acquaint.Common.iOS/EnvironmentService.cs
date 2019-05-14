using Acquaint.Abstractions;
using ObjCRuntime;

namespace Acquaint.Common.iOS
{
    public class EnvironmentService : IEnvironmentService
    {
        #region IEnvironmentService implementation

        public bool IsRealDevice => Runtime.Arch == Arch.DEVICE;

        #endregion
    }
}

