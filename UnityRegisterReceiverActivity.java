package aar.com.broadcastlib;

import java.util.Calendar;

import android.app.Activity;
import android.app.AlarmManager;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.support.v4.content.LocalBroadcastManager;
import android.util.Log;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class UnityRegisterReceiverActivity extends UnityPlayerActivity{

    @Override
    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        Log.v("android", "Receiver create");
        registerReceiver(new MyReceiver(), new IntentFilter("IntentToUnity"));
    }
}
