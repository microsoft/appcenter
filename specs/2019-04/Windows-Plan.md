# App Center Windows Application Plan

This plan details the proposed improvements for App Center’s Windows application offering along with a comparison of App Center and HockeyApp. This is work in progress and will be iterated upon based on any internal updates and feedback from the community. We expect to start work on the planned items in the second quarter of 2019. 


## App Center’s Current Offering 
### Diagnostics

- Crash reporting for UWP (.NET native) apps distributed through the Windows store 
> Note: there are some known limitations and missing features for UWP apps. Find more details in our [Windows documentation](https://docs.microsoft.com/en-us/appcenter/diagnostics/windows-support). 

### Distribution

- Distributing UWP and Unity UWP apps in .appx, .appxbundle, .appxupload, .msix, .msixbundle, .msixupload or .zip file
- Unofficial support for distributing WPF and WinForms apps when packaged in the formats listed above using the [Windows Application Packaging](https://docs.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-packaging-dot-net)

### Analytics 

- Analytics SDK for UWP apps and all of its functionalities listed in App Center's [analytics documentation](https://docs.microsoft.com/en-us/appcenter/analytics/).  

### Build
- Build support for UWP apps

## Planned Improvements 

### Diagnostics 

- Crash reporting capabilities for store and sideloaded UWP (.NET Native), WPF and WinForms (.NET Framework) apps aligned with other platforms supported in App Center. Find more details on App Center Diagnostics features in our [documentation](https://docs.microsoft.com/en-us/appcenter/diagnostics/features).  

> Note: this does not include handled exceptions and [custom properties](https://docs.microsoft.com/en-us/appcenter/diagnostics/features#key-value-pairs).  
 

### Distribution 

- Official support for WPF and WinForms applications 
 
> Note: this does not include in app updates through App Center.  
### Analytics
- Analytics SDK for WPF and WinForms (.NET Framework) apps and all of its functionalities as listed in App Center's [analytics documentation](https://docs.microsoft.com/en-us/appcenter/analytics/).  

## HockeyApp Comparison (Windows Specific) 
### Diagnostics 
Note: “full support” means supporting all App Center crashes features as indicated in App Center's [diagnostics documentation](https://docs.microsoft.com/en-us/appcenter/diagnostics/features), not including handled exceptions and custom properties. 


|    | HockeyApp | App Center Today | Proposal | 
| ---| --------- |:----------------:| ------- |
| UWP| Yes       | Partial support | Full support for crashes |
| WPF| Yes       | None | Full support for crashes |
| WinRT / Silverlight | Yes       | None | None |
| WinForms | (Open Source)    | None | Full support for crashes |
| Win32 | None | None | None |


### Distribution  

Note: HockeyApp supports over the air app updates for WPF, WinRT, and Silverlight applications. App Center will consider supporting these scenarios for distributing Windows apps using the .appInstaller file [#52](https://github.com/Microsoft/appcenter/issues/52)

|    | HockeyApp | App Center Today | Proposal | 
| ---| --------- |:----------------:| ------- |
| UWP| Yes       | Yes | None (already supported) |
| WPF| Yes       | None | Official support for .NET framework |
| WinRT / Silverlight | Yes       | None | None |
| WinForms | (Open Source)    | None | Full support for .NET framework |
| Win32 | None | None | None |

### Analytics 

Note: HockeyApp has basic user metrics & event tracking in its analytics service. Full support in App Center means having a working App Center Analytics SDK with the full capabilities listed in App Center's [analytics documentation](https://docs.microsoft.com/en-us/appcenter/analytics/). 

|    | HockeyApp | App Center Today | Proposal | 
| ---| --------- |:----------------:| ------- |
| UWP| Yes       | Yes | None (already supported) |
| WPF| None       | None | Full support for .NET framework |
| WinRT / Silverlight | Yes     | None | None |
| WinForms | (Open Source)    | None | Full support for .NET framework |
| Win32 | None | None | None |


## Scenarios for consideration: 

Our team is considering support other features including, but not limited to the following: 

- Handled exceptions for UWP apps [(#150)](https://github.com/Microsoft/appcenter/issues/150)
- Distributing Windows apps using .appinstaller files (this will allow developers to release over the air updates) [(#52)](https://github.com/Microsoft/appcenter/issues/52) 
- Distribution, analytics and crash reporting for .NET Core 3. This will include Console, WinForms and WPF apps. [(#53)](https://github.com/Microsoft/appcenter/issues/53)
- Build support for WPF apps [(#85)](https://github.com/Microsoft/appcenter/issues/85)

 Please give existing feature request a +1 or create a new request specifying the Windows platform to help our team better prioritize.  
 
 ## Resources 

- [HockeyApp Windows documentation](https://support.hockeyapp.net/kb/client-integration-windows-and-windows-phone/hockeyapp-for-applications-on-windows) 
- [App Center Windows Diagnostics documentation](https://docs.microsoft.com/en-us/appcenter/diagnostics/windows-support)
- [App Center Distribution documentation](https://docs.microsoft.com/en-us/appcenter/distribution/)
- [App Center Analytics documentation](https://docs.microsoft.com/en-us/appcenter/analytics/) 

