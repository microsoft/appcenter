# App Center Window’s Proposal 

This plan details the proposed improvements for App Center’s Windows offering along with a comparison of App Center and HockeyApp. This is work in progress and will be iterated upon based on any internal updates and feedback from the community. We expect to start work on the planned items in the second quarter of 2019. 


## App Center’s current Windows Offering 
### Diagnostics

- Crash reporting for UWP (.NET native) apps distributed through the Windows store 
> Note: there are some known limitations and missing features for UWP apps. Find more details in our [Windows documentation](https://docs.microsoft.com/en-us/appcenter/diagnostics/windows-support). 

### Distribution

- Distributing UWP and Unity UWP apps using the APPX or MSIX package format
- Unofficial support for distributing WPF (.NET Framework) applications 

### Analytics 

- Analytics SDK for UWP apps and all of its functionalities listed in App Center's [analytics documentation](https://docs.microsoft.com/en-us/appcenter/analytics/).  

### Build
- Build support for UWP apps

## Planned Improvements 

### Diagnostics 

- Crash reporting capabilities for store and sideloaded UWP and WPF apps aligned with other platforms supported in App Center. Find more details on App Center Diagnostics features in our documentation.  

> Note: this does not include handled exceptions and custom properties.  

### Distribution 

- Official support for WPF applications 
 
> Note: this does not include in app updates through App Center.  
### Analytics
- Analytics SDK for WPF apps and all of its functionalities as listed in App Center's [analytics documentation](https://docs.microsoft.com/en-us/appcenter/analytics/).  

## HockeyApp Comparison (Windows Specific) 
### Diagnostics 
Note: “full support” means supporting all App Center crashes features as indicated in App Center's [diagnostics documenation](https://docs.microsoft.com/en-us/appcenter/diagnostics/features), not including handled exceptions and custom key value pairs.  

|    | HockeyApp | App Center Today | Proposal | 
| ---| --------- |:----------------:| ------- |
| UWP| Yes       | Partial support | Full support for crashes |
| WPF| Yes       | None | Full support for crashes |
| WinRT / Silverlight | Yes       | None | None |
| WinForms | (Open Source)    | None | Stretch goal |
| Win32 | None | None | None |


### Distribution  

Note: HockeyApp supports over the air app updates for WPF, WinRT, and Silverlight applications. App Center will consider supporting this scenario for distributing Windows apps using the .AppInstaller file (#52) 

|    | HockeyApp | App Center Today | Proposal | 
| ---| --------- |:----------------:| ------- |
| UWP| Yes       | Yes | None (already supported) |
| WPF| Yes       | None | Official support for .NET framework |
| WinRT / Silverlight | Yes       | None | None |
| WinForms | (Open Source)    | None | Stretch goal |
| Win32 | None | None | None |

### Analytics 

Note: HockeyApp has user & event tracking in it’s analytics today. Full support in App Center means having a working App Center Analytics SDK with the full capabilities listed in App Center's [analytics documentation](https://docs.microsoft.com/en-us/appcenter/analytics/). 

|    | HockeyApp | App Center Today | Proposal | 
| ---| --------- |:----------------:| ------- |
| UWP| Yes       | Yes | None (already supported) |
| WPF| None       | None | Full support for .NET framework |
| WinRT / Silverlight | Yes     | None | None |
| WinForms | (Open Source)    | None | Stretch goal |
| Win32 | None | None | None |


## Scenarios for consideration: 

Our team is considering support other features including, but not limited to the following: 

- Distributing Windows apps using .AppInstaller files (this will allow developers to release over the air updates) [(#52)](https://github.com/Microsoft/appcenter/issues/52) 
- Handled exceptions for UWP apps [(#150)](https://github.com/Microsoft/appcenter/issues/150)
- Distribution, analytics and crash reporting for .NET Core 3 [(#53)](https://github.com/Microsoft/appcenter/issues/53)
- Distribution, analytics and crash reporting for WinForms 
- Build support for WPF apps [(#85)](https://github.com/Microsoft/appcenter/issues/85)

 Please give existing feature request a +1 or create a new request specifying the Windows platform to help our team better prioritize.  
 
 ## Resources 

- [HockeyApp Windows documentation](https://support.hockeyapp.net/kb/client-integration-windows-and-windows-phone/hockeyapp-for-applications-on-windows) 
- [App Center Windows Diagnostics documentation](https://docs.microsoft.com/en-us/appcenter/diagnostics/windows-support)
- [App Center Distribution documentation](https://docs.microsoft.com/en-us/appcenter/distribution/)
- [App Center Analytics documentation](https://docs.microsoft.com/en-us/appcenter/analytics/) 

