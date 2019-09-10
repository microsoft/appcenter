package com.example.appcenter.sampleapp_android;

import android.os.Bundle;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentPagerAdapter;
import androidx.viewpager.widget.ViewPager;
import androidx.appcompat.app.AppCompatActivity;

import com.microsoft.appcenter.AppCenter;
import com.microsoft.appcenter.analytics.Analytics;
import com.microsoft.appcenter.crashes.Crashes;


public class MainActivity extends AppCompatActivity {

    private SectionsPagerAdapter mSectionsPagerAdapter;
    private ViewPager mViewPager;
    private final int PAGECOUNT = 7;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        AppCenter.start(getApplication(), "<APP SECRET HERE>",
                Analytics.class, Crashes.class);
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_root);

        mSectionsPagerAdapter = new SectionsPagerAdapter(getSupportFragmentManager());

        mViewPager = (ViewPager) findViewById(R.id.container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
    }

    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            switch (position) {
                case 0:
                    return WelcomeActivity.newInstance();
                case 1:
                    return BuildActivity.newInstance();
                case 2:
                    return TestActivity.newInstance();
                case 3:
                    return DistributeActivity.newInstance();
                case 4:
                    return CrashesActivity.newInstance();
                case 5:
                    return AnalyticsActivity.newInstance();
                case 6:
                    return PushActivity.newInstance();
                default:
                    return WelcomeActivity.newInstance();
            }
        }

        @Override
        public int getCount() {
            return PAGECOUNT;
        }

        @Override
        public CharSequence getPageTitle(int position) {
            switch (position) {
                case 0:
                    return WelcomeActivity.getPageName();
                case 1:
                    return BuildActivity.getPageName();
                case 2:
                    return TestActivity.getPageName();
                case 3:
                    return DistributeActivity.getPageName();
                case 4:
                    return CrashesActivity.getPageName();
                case 5:
                    return AnalyticsActivity.getPageName();
                case 6:
                    return PushActivity.getPageName();
                default:
                    return WelcomeActivity.getPageName();
            }
        }
    }
}