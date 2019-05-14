//
//  AppCenterSampleUITests.m
//  AppCenterSampleUITests
//
//  Created by Akvelon on 4/13/18.
//  Copyright © 2018 Facebook. All rights reserved.
//

#import <XCTest/XCTest.h>
#import <AppCenterXCUITestExtensions/AppCenterXCUITestExtensions.h>

@interface AppCenterSampleUITests : XCTestCase

@end

@implementation AppCenterSampleUITests

- (void)setUp {
    [super setUp];
    self.continueAfterFailure = NO;
}

- (void)tearDown {
    [super tearDown];
}

- (void)testNavigation {
  XCUIApplication *app =  [ACTLaunch launch];
  
  [[app.otherElements matchingIdentifier:@"welcomeScreen"].element.firstMatch swipeLeft];
  [[app.otherElements matchingIdentifier:@"buildScreen"].element.firstMatch swipeLeft];
  [[app.otherElements matchingIdentifier:@"testScreen"].element.firstMatch swipeLeft];
  [[app.otherElements matchingIdentifier:@"codePushScreen"].element.firstMatch swipeLeft];
  [[app.otherElements matchingIdentifier:@"distributeScreen"].element.firstMatch swipeLeft];
  [[app.otherElements matchingIdentifier:@"crashesScreen"].element.firstMatch swipeLeft];
  [[app.otherElements matchingIdentifier:@"analyticsScreen"].element.firstMatch swipeLeft];
  [app terminate];
}

@end
