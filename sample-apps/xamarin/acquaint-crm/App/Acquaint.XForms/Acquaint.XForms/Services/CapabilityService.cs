using Xamarin.Forms;
using Acquaint.XForms;
using Acquaint.Abstractions;
using Microsoft.Practices.ServiceLocation;

[assembly: Dependency (typeof (CapabilityService))]

namespace Acquaint.XForms
{
    public class CapabilityService : ICapabilityService
    {
        readonly IEnvironmentService _EnvironmentService;

        public CapabilityService()
        {
			_EnvironmentService = ServiceLocator.Current.GetInstance<IEnvironmentService>();
        }

        #region ICapabilityService implementation

        public bool CanMakeCalls => _EnvironmentService.IsRealDevice || (Device.OS != TargetPlatform.iOS);

        public bool CanSendMessages => _EnvironmentService.IsRealDevice || (Device.OS != TargetPlatform.iOS);

        public bool CanSendEmail => _EnvironmentService.IsRealDevice || (Device.OS != TargetPlatform.iOS);

        #endregion
    }
}

