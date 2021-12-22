using UnityEngine;
using System.Collections.Generic;
using System;

public partial class AnalyticsController
{
    private static Dictionary<Type, IAnalytics> _analyticsServices = new Dictionary<Type, IAnalytics>();
    private static event Action<string, Dictionary<string, object>> EventTrackers;
    private static event Action<string, string> UserPropertiesTracker;
    private static event Action<string, string> ScreenVisitTracker;

    /// <summary>
    /// You can Subscribe Analytics Services after successfull initialization to AnalyticsController.
    /// By Sybscribing an Analytics Service to AnalyticsController, it will be capable to Track Events, Properties and ScreenVisits
    /// </summary>
    /// <param name="analytics">
    /// Predefined abstract class named AnalyticsBase.cs is a sample implementation of IAnalytics.
    /// Derive analytics service implementation from AnalyticsBase.cs and Subscibe after initializing service successfully
    /// </param>
    internal static void SubscribeAnalytics(IAnalytics analytics)
    {

        if (_analyticsServices.ContainsKey(analytics.GetType()))
        {
            Debug.Log($"{analytics.GetType()} already subscribed !");
            return;
        }
        else
            _analyticsServices.Add(analytics.GetType(), analytics);


        EventTrackers += analytics.CustomEvent;
        UserPropertiesTracker += analytics.SetUserProperty;
        ScreenVisitTracker += analytics.SetScreenVisit;
    }

    /// <summary>
    /// You can unsubscribe Analytics services from tracking events, properties and screen visits by passing them to this function
    /// </summary>
    /// <param name="analytics"></param>
    internal static void UnSubscribeAnalytics(IAnalytics analytics)
    {
        if (_analyticsServices.ContainsKey(analytics.GetType()))
        {
            EventTrackers -= analytics.CustomEvent;
            UserPropertiesTracker -= analytics.SetUserProperty;
            ScreenVisitTracker -= analytics.SetScreenVisit;
            _analyticsServices.Remove(analytics.GetType());
        }
    }

    /// <summary>
    /// Events provide insight on what is happening in your app, such as user actions, system events, or errors.
    /// Analytics Services usually automatically logs some events for you; you don't need to add any code to receive them. 
    /// If your app needs to collect additional data, you can log different Analytics event types in your app. 
    /// Note that event names are case-sensitive and that logging two events whose names differ only in case will result in two distinct events.
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="parameters"></param>
    public static void CustomEvent(string eventName, Dictionary<string, object> parameters = default)
    {
        EventTrackers?.SafeInvoke(eventName, parameters);
    }

    /// <summary>
    /// User properties are attributes you define to describe segments of your user base, such as language preference or geographic location.
    /// Analytics automatically logs some user properties; 
    /// you don't need to add any code to enable them. 
    /// If your app needs to collect additional data, you can set different Analytics user properties in your app.
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="propertyValue"></param>
    public static void SetUserProperty(string propertyName, string propertyValue)
    {
        UserPropertiesTracker?.SafeInvoke(propertyName, propertyValue);
    }

    /// <summary>
    /// Analytics Services tracks screen transitions and attaches information about the current screen to events,
    /// enabling you to track metrics such as user engagement or user behavior per screen.
    /// Much of this data collection happens automatically, but you can also manually log screenviews.
    /// Manually tracking screens is useful for each screen you may wish to track, such as in a game.
    /// </summary>
    /// <param name="screenName"></param>
    /// <param name="screenClass"></param>
    public static void SetScreenVisit(string screenName, string screenClass)
    {
        ScreenVisitTracker?.SafeInvoke(screenName, screenClass);
    }

}
