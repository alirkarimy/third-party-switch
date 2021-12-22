
using System;
using System.Collections.Generic;
#if WebEngage
using WebEngageBridge;
#endif

public class WebEngageAnalytics : AnalyticsBase
{
    private static WebEngageAnalytics webengageAnaltics;

    private void Awake()
    {
        if (webengageAnaltics == null)
        {
            webengageAnaltics = this;
            Initialize();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public override void Initialize()
    {
        #if WebEngage
        WebEngage.Engage();
        AnalyticsController.SubscribeAnalytics(this);
#endif
    }

    public override void CustomEvent(string eventName, Dictionary<string, object> parameters)
    {
        #if WebEngage
        UnityEngine.Debug.Log($"WebEngage CustomEvent ( {eventName} ,{parameters})");
        WebEngage.TrackEvent(eventName, parameters);
#endif
       
    }

    public override void SetUserProperty(string propertyName, string propertyValue)
    {
#if WebEngage
        UnityEngine.Debug.Log($"WebEngage SetUserProperty ( {propertyName} ,{propertyValue})");
#endif

    }

    public override void SetScreenVisit(string screenName, string screenClass)
    {
#if WebEngage
        UnityEngine.Debug.Log($"WebEngage SetScreenVisit ( {screenName} ,{screenClass})");
#endif

    }
}