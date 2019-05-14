package com.example.appcenter.sampleapp_android;

import android.app.Dialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.Fragment;
import android.support.v7.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;

public class CrashesActivity extends Fragment implements OnClickListener {
    private static final String pageName = "Crashes";

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        ViewGroup rootView = (ViewGroup) inflater.inflate(
                R.layout.crashes_root, container, false);

        Button crashButton = (Button) rootView.findViewById(R.id.crashButton);
        crashButton.setOnClickListener(this);

        return rootView;
    }

    public static CrashesActivity newInstance() {
        Bundle args = new Bundle();
        CrashesActivity fragment = new CrashesActivity();
        fragment.setArguments(args);
        return fragment;
    }

    public static CharSequence getPageName() {
        return pageName;
    }

    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.crashButton:
                DialogFragment crashDialog = new CrashDialog();
                crashDialog.show(getFragmentManager(), "crashDialog");
        }
    }

    public static class CrashDialog extends DialogFragment {
        public Dialog onCreateDialog(Bundle savedInstanceState) {
            AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
            builder.setMessage("A crash report will be sent when you reopen the app.")
                    .setPositiveButton("Crash app", new DialogInterface.OnClickListener() {
                        public void onClick(DialogInterface dialog, int id) {
                            throw new RuntimeException("crashing");
                        }
                    }).setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                public void onClick(DialogInterface dialog, int id) {
                }
            });
            return builder.create();
        }
    }
}
