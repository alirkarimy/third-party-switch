using System.Collections.Generic;

public interface IAnalytics
{
    void Initialize();

    void CustomEvent(string eventName, Dictionary<string, object> parameters);

    void SetUserProperty(string propertyName, string propertyValue);
   
    void SetScreenVisit(string screenName, string screenClass);
}