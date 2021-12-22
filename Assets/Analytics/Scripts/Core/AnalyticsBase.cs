using System.Collections.Generic;
using UnityEngine;

public abstract class AnalyticsBase : MonoBehaviour, IAnalytics
{  
    public abstract void Initialize();

    public abstract void CustomEvent(string eventName, Dictionary<string, object> parameters);

    public abstract void SetUserProperty(string propertyName, string propertyValue);

    public abstract void SetScreenVisit(string screenName, string screenClass);

    public void OnDestroy()
    {
        AnalyticsController.UnSubscribeAnalytics(this);
    }
}
