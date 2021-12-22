using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsTest : MonoBehaviour
{
    public void SendSampleEvent()
    {
        AnalyticsController.CustomEvent("test_event");
    }
    public void SendSampleProperty()
    {
        AnalyticsController.SetUserProperty("test_prop", "test_prop_val");
    }
    public void SetScreenVisit()
    {
        AnalyticsController.SetScreenVisit("test_screen_name", "test_screen_class");
    }
}
