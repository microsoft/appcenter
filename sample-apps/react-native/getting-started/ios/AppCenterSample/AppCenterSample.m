#import <Foundation/Foundation.h>
#import "AppCenterSample.h"
#import <React/RCTUtils.h>
@import AppCenterAnalytics;

// AppCenterSample.m
@implementation AppCenterSample

RCT_EXPORT_MODULE();

RCT_EXPORT_METHOD(showColoredEventsDialog)
{
  UIViewController *presentingController = RCTPresentedViewController();
  if (presentingController == nil) {
    return;
  }
  
  UIAlertController *alert = [UIAlertController
                                        alertControllerWithTitle:@"Choose a color"
                                        message:nil
                                        preferredStyle:UIAlertControllerStyleAlert];
  
  UIAlertAction *yellow = [UIAlertAction
                           actionWithTitle:@"💛 Yellow"
                           style:UIAlertActionStyleDefault
                           handler:^(UIAlertAction * action)
                           {
                             [MSAnalytics trackEvent:@"Color event" withProperties: @{@"Color": @"Yellow"}];
                           }];
  [alert addAction:(yellow)];
  
  UIAlertAction *blue = [UIAlertAction
                           actionWithTitle:@"💙 Blue"
                           style:UIAlertActionStyleDefault
                           handler:^(UIAlertAction * action)
                           {
                             [MSAnalytics trackEvent:@"Color event" withProperties: @{@"Color": @"Blue"}];
                           }];
  [alert addAction:(blue)];
  
  UIAlertAction *red = [UIAlertAction
                           actionWithTitle:@"❤️ Red"
                           style:UIAlertActionStyleDefault
                           handler:^(UIAlertAction * action)
                           {
                             [MSAnalytics trackEvent:@"Color event" withProperties: @{@"Color": @"Red"}];
                           }];
  [alert addAction:(red)];

  [presentingController presentViewController:alert animated:YES completion:nil];
}

@end
