using Android.App;
using Android.Content;
using Android.Telephony;
using System;

namespace Autobot.Droid.Platform
{
    [BroadcastReceiver]
    [IntentFilter(new[] { TelephonyManager.ActionPhoneStateChanged, Intent.ActionNewOutgoingCall })]
    public abstract class PhonecallReceiver : BroadcastReceiver
    {
        private static DateTime callStartTime;

        private static bool isIncoming;

        //The receiver will be recreated whenever android feels like it.  We need a static variable to remember data between instantiations
        private static CallState lastState = CallState.Idle;

        private static string savedNumber;  //because the passed incoming is only valid in ringing

        public void OnCallStateChanged(Context context, CallState state, string number)
        {
            if (lastState == state)
            {
                //No change, debounce extras
                return;
            }
            switch (state)
            {
                case CallState.Ringing:
                    isIncoming = true;
                    callStartTime = DateTime.Now;
                    savedNumber = number;
                    onIncomingCallStarted(context, number, callStartTime);
                    break;

                case CallState.Offhook:
                    //Transition of ringing->offhook are pickups of incoming calls.  Nothing done on them
                    if (lastState != CallState.Ringing)
                    {
                        isIncoming = false;
                        callStartTime = DateTime.Now;
                        onOutgoingCallStarted(context, savedNumber, callStartTime);
                    }
                    break;

                case CallState.Idle:
                    //Went to idle-  this is the end of a call.  What type depends on previous state(s)
                    if (lastState == CallState.Ringing)
                    {
                        //Ring but no pickup-  a miss
                        onMissedCall(context, savedNumber, callStartTime);
                    }
                    else if (isIncoming)
                    {
                        onIncomingCallEnded(context, savedNumber, callStartTime, DateTime.Now);
                    }
                    else {
                        onOutgoingCallEnded(context, savedNumber, callStartTime, DateTime.Now);
                    }
                    break;
            }
            lastState = state;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            //We listen to two intents.  The new outgoing call only tells us of an outgoing call.  We use it to get the number.
            if (intent.Action == Intent.ActionNewOutgoingCall)
            {
                savedNumber = intent.GetStringExtra(Intent.ExtraPhoneNumber);
            }
            else {
                CallState state = (CallState)intent.GetIntExtra(TelephonyManager.ExtraState, -1);
                string number = intent.GetStringExtra(TelephonyManager.ExtraIncomingNumber);

                OnCallStateChanged(context, state, number);
            }
        }

        protected void onIncomingCallEnded(Context ctx, String number, DateTime start, DateTime end)
        {
        }

        protected void onIncomingCallStarted(Context ctx, String number, DateTime start)
        {
        }

        protected void onMissedCall(Context ctx, String number, DateTime start)
        {
        }

        protected void onOutgoingCallEnded(Context ctx, String number, DateTime start, DateTime end)
        {
        }

        protected void onOutgoingCallStarted(Context ctx, String number, DateTime start)
        {
        }
    }
}