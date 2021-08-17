using Android.App;
using Android.Content;
using System;

namespace Task2
{
    [BroadcastReceiver]
    [IntentFilter(new[] { "MY_ALARM_RECEIVED" })]
    class Alarm : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action != null)
            {
                if (intent.Action == "MY_ALARM_RECEIVED")
                {
                    if (!ForegroundService._globalService.CLOSE_CONNECTION)
                    {
                        DateTime dt = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff"), "dd/MM/yyyy HH:mm:ss:fff", null);
                        if ((dt - DateTime.ParseExact(ForegroundService._globalService.pingStatus, "dd/MM/yyyy HH:mm:ss:fff", null)).Seconds >= 15)
                        {
                            System.Diagnostics.Debug.WriteLine("Connection timed out (15 second), reconnecting to server..");
                            ForegroundService._globalService.StopServerSession(ForegroundService._globalService.globalMemoryStream);
                            if (!ForegroundService._globalService.CLOSE_CONNECTION)
                            {
                                ForegroundService._globalService.Baglanti_Kur();
                                System.Threading.Tasks.Task.Delay(6000).Wait();
                            }
                        }
                        ForegroundService._globalService.setAlarm(context);
                    }
                    else
                    {
                        ForegroundService._globalService.cancelAlarm(context);
                    }                    
                }
            }
            
        }
    }
}