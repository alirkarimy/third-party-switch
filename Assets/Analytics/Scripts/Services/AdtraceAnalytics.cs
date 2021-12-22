#if AdTrace
using com.adtrace.sdk;
#endif
using System.Collections.Generic;

public class AdtraceAnalytics : AnalyticsBase
{
    private static AdtraceAnalytics adtraceAnalytics;
  #if AdTrace
    private AdTrace adTrace;
#endif
    private void Awake()
    {
#if AdTrace
        if (adtraceAnalytics == null)
        {
            adtraceAnalytics = this;
            if (adTrace == null)
            {
                adTrace = gameObject.GetComponent<AdTrace>() ?? gameObject.AddComponent<AdTrace>();
            }

            //@Warning : Call "AnalyticsController.SubscribeAnalytics(this)" in case of adTrace.startManually is false
            if (adTrace.startManually) Initialize();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
#endif

    }

    public override void Initialize()
    {
#if AdTrace
        AdTraceConfig adtraceConfig = new AdTraceConfig(string.Equals(adTrace.appToken, "{Your App Token}") ? "0var7lga5xsj" : adTrace.appToken, adTrace.environment);
        adtraceConfig.setLogLevel(adTrace.logLevel);
        adtraceConfig.setSendInBackground(adTrace.sendInBackground);
        adtraceConfig.setEventBufferingEnabled(adTrace.eventBuffering);
        adtraceConfig.setLaunchDeferredDeeplink(adTrace.launchDeferredDeeplink);
        adtraceConfig.setEnableSendInstalledApps(adTrace.enableSendInstalledApps);

        //adtraceConfig.setEventSuccessDelegate(EventSuccessCallback);
        //adtraceConfig.setEventFailureDelegate(EventFailureCallback);
        //adtraceConfig.setSessionSuccessDelegate(SessionSuccessCallback);
        //adtraceConfig.setSessionFailureDelegate(SessionFailureCallback);
        //adtraceConfig.setDeferredDeeplinkDelegate(DeferredDeeplinkCallback);
        //adtraceConfig.setAttributionChangedDelegate(AttributionChangedCallback);

        //adtraceConfig.setAppSecret(secretId, info1, info2, info3, info4);

        AdTrace.start(adtraceConfig);

        AnalyticsController.SubscribeAnalytics(this);
#endif

    }

    public override void CustomEvent(string eventName, Dictionary<string, object> parameters)
    {
#if AdTrace
        UnityEngine.Debug.Log($"Adtrace CustomEvent ( {eventName} ,{parameters})");
        AdTraceEvent adtraceEvent = new AdTraceEvent(eventName);
        AdTrace.trackEvent(adtraceEvent);
#endif

    }

    public override void SetUserProperty(string propertyName, string propertyValue)
    {
#if AdTrace
        UnityEngine.Debug.Log($"Adtrace SetUserProperty ( {propertyName} ,{propertyValue})");
#endif

    }

    public override void SetScreenVisit(string screenName, string screenClass)
    {
#if AdTrace
        UnityEngine.Debug.Log($"Adtrace SetScreenVisit ( {screenName} ,{screenClass})");
#endif

    }
}
