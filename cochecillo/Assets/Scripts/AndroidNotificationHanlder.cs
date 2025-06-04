using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class AndroidNotificationHanlder : MonoBehaviour
{
    private const string CHANNEL_ID = "default_channel";

    public void ScheduleNotification(DateTime dateTime)
    {
#if UNITY_ANDROID
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
        {
            Id = CHANNEL_ID,
            Name = "Notification Channel",
            Importance = Importance.Default,
            Description = "Generic notifications"
        };

        //una vez creado el tipo de canal, hay que registrarlo
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        //y ahora podemos crear la notificacion
        AndroidNotification notification = new AndroidNotification
        {
            Title = "Energia del coche recargada!",
            Text = "El coche te esta esperando.",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = dateTime // La hora en la que se disparara la notificacion
        };

        //ahora lanzamos la notificacion
        AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
    }
#endif
}
