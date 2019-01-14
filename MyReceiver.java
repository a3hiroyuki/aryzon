package aar.com.broadcastlib;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;


public class MyReceiver extends BroadcastReceiver {

    private static MyReceiver instance;
    public static String mText = "0";

    @Override
    public void onReceive(Context context, Intent intent) {
        String sentStr = intent.getStringExtra(Intent.EXTRA_TEXT);
        if (sentStr != null) {
            mText = sentStr;
        }
    }

    // static method to create our receiver object, it'll be Unity that will create ou receiver object (singleton)
    public static void createInstance() {
        if(instance ==  null) {
            instance = new MyReceiver();
        }
        Log.v("android", "cerate!");
    }
}

