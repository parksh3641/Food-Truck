using UnityEngine;
using System.Collections.Generic;
using System;
using Assets.SimpleAndroidNotifications;
using Unity.Notifications.Android;

#if UNITY_IOS
using NotificationServices = UnityEngine.iOS.NotificationServices;
using NotificationType = UnityEngine.iOS.NotificationType;
using LocalNotification = UnityEngine.iOS.LocalNotification;
#endif

public class LocalNotificationManager : MonoBehaviour
{
    string title;
    string content;

    void Start()
    {
#if UNITY_IOS
        NotificationServices.RegisterForNotifications(NotificationType.Alert | NotificationType.Badge | NotificationType.Sound);
#endif
        AddLocalNotification();
    }

    void DeleteNotification() //알람 초기화
    {
#if UNITY_ANDROID
        AndroidNotificationCenter.CancelAllNotifications();
#elif UNITY_IOS
        NotificationServices.ClearLocalNotifications();
        NotificationServices.CancelAllLocalNotifications();
#endif
    }

    void AddLocalNotification() //알람 추가
    {
        DeleteNotification();

        DateTime dtToday = DateTime.Today;  // 오늘

        //매일 0시에 알림
        DateTime notify1 = DateTime.Now;
        TimeSpan time1 = DateTime.Today.AddDays(1) - notify1;

        //어플 종료 후 15분 후에 알림
        DateTime notify2 = DateTime.Now.AddMinutes(1);
        TimeSpan time2 = notify2 - DateTime.Now;

        //어플 종료 후 알림(점심)
        DateTime notify3 = Convert.ToDateTime(dtToday.ToString("yyyy/MM/dd") + " " + "12:30:00 PM");
        TimeSpan time3 = notify3 - DateTime.Now;

        //어플 종료 후 알림(저녁)
        DateTime notify4 = Convert.ToDateTime(dtToday.ToString("yyyy/MM/dd") + " " + "7:30:00 PM");
        TimeSpan time4 = notify4 - DateTime.Now;

        //어플 종료 후 특정 요일에 등록 알림
        DateTime dtNow = dtToday.AddDays(Convert.ToInt32(DayOfWeek.Friday) - Convert.ToInt32(dtToday.DayOfWeek));
        DateTime notify5 = Convert.ToDateTime(dtNow.ToString("yyyy/MM/dd") + " " + "9:00:00 PM");
        TimeSpan time5 = notify5 - DateTime.Now;

        title = LocalizationManager.instance.GetString("YummyRush");
        content = LocalizationManager.instance.GetString("YummyRushNoti");

#if UNITY_ANDROID
        //NotificationManager.SendWithAppIcon(time1, title, content, Color.gray, NotificationIcon.Bell);

        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = content;
        notification.FireTime = notify4;

        notification.SmallIcon = "icon_1";
        notification.LargeIcon = "icon_0";
        notification.ShowInForeground = false;
        string channelId = "my_channel_id";

        AndroidNotificationCenter.SendNotification(notification, channelId);

        Debug.Log("Set Android Notification");
#elif UNITY_IOS
        if (time4.Ticks > 0)
        {
            LocalNotification noti = new LocalNotification();
            noti.alertTitle = title;
            noti.alertBody = content;
            noti.soundName = LocalNotification.defaultSoundName;
            noti.applicationIconBadgeNumber = 1;
            noti.fireDate = notify1;
            NotificationServices.ScheduleLocalNotification(noti);
        }

        Debug.Log("Set IOS Notification");
#endif
    }
}