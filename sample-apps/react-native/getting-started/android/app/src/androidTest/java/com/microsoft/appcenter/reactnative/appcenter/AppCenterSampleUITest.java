package com.microsoft.appcenter.reactnative.appcenter;

import android.support.test.espresso.ViewInteraction;
import android.support.test.espresso.core.deps.guava.collect.Lists;
import android.support.test.rule.ActivityTestRule;
import android.support.test.runner.AndroidJUnit4;
import android.test.suitebuilder.annotation.LargeTest;
import com.microsoft.appcenter.espresso.Factory;
import com.microsoft.appcenter.espresso.ReportHelper;
import com.appcentersample.MainActivity;

import org.junit.After;
import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import java.util.List;

import static android.support.test.espresso.Espresso.onView;
import static android.support.test.espresso.action.ViewActions.swipeLeft;
import static android.support.test.espresso.matcher.ViewMatchers.withTagValue;
import static org.hamcrest.Matchers.is;
import static org.hamcrest.core.AllOf.allOf;

@LargeTest
@RunWith(AndroidJUnit4.class)
public class AppCenterSampleUITest {

    @Rule
    public ReportHelper reportHelper = Factory.getReportHelper();

    @Rule
    public ActivityTestRule<MainActivity> mActivityTestRule = new ActivityTestRule<>(MainActivity.class);

    @Test
    public void navigationTest() throws InterruptedException {
        Thread.sleep(1500);
        List<String> screens = Lists.newArrayList(
                "welcomeScreen",
                "buildScreen",
                "testScreen",
                "codePushScreen",
                "distributeScreen",
                "crashesScreen",
                "analyticsScreen");
        swipeScreens(screens);
    }

    private void swipeScreens(List<String> screens) throws InterruptedException {
        for (String screen : screens) {
            ViewInteraction viewInteraction = onView(
                    allOf(withTagValue(is((Object) screen))));
            viewInteraction.perform(swipeLeft());
            // wait for 5 seconds after swiping
            Thread.sleep(5000);
        }
    }

    @After
    public void tearDown(){
        reportHelper.label("Stopping App");
    }
}
