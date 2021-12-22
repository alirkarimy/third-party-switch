using System.Text;

public partial class AnalyticsController
{
   


    /// <summary>
    /// to track user experience and player behavior across five major areas:
    ///     Application: Track the usage of basic elements in your application user interface
    ///     Progression : Track player progress through your application or game
    ///     Onboarding: Track the earliest interactions players have with your application or game
    ///     Engagement: Track important actions related to social sharing, achievements
    ///     Monetization: Track revenue-related events as well as your in-game economies
    /// 
    /// The list of Standard Events covers a set of things that you might find useful to track in your games and applications.
    /// For example, most games have a concept of player progress, which could mean puzzles completed, matches played, RPG-type experience earned, or some other notion.
    /// By tracking standard progression events, you can better understand where players stop progressing and, ultimately, where they stop playing.
    /// Use Standard Events
    /// Most Standard Events define required and optional parameters that provide additional information about the state of your game when an Analytics event is sent.
    /// You can also define your own custom parameters.
    /// Setting parameters on your event allows you to filter data collected at the time the event occurred.
    /// Visualization tools for Standard and Custom Events can be viewed on the Analytics Dashboard, including Data Explorer, Funnel Analyzer, and Segment Builder.
    /// Unity Analytics limits the number of Standard and Custom Events it accepts to 100 per hour, per user. 
    /// In other words, if a single user playing your game manages to trigger more than 100 Standard or Custom Events within an hour, Analytics discards the additional events.
    /// 
    /// </summary>
#region Standard Events

    /// <summary>
    /// Application events track player interaction with the application outside of the game itself(for example, in the menu or cut-scene screens before or after a level).
    /// You can analyze application events to see whether players use the basic parts of your user interface as often as you expect.
    /// </summary>
    #region Application events

    //Player opened a screen in the UI
    //such as a high score or settings screen.
    public static void ScreenVisit(string screen_name,string screen_part)
    {

    }

    // Player began watching a cinematic cutscene.
    public static void CutSceneStarted()
    {

    }
    //Player skipped past a cinematic cutscene.
    public static void CuSceneSkipped()
    {

    }
    #endregion

    /// <summary>
    /// Progression events track players as they advance through your game.
    /// Analyze standard progression events to monitor player progress in your game.
    /// </summary>

    #region Progression events

    // Player began a new game(useful for games with a distinct beginning and ending).
    public static void GameStart()
    {

    }
    //Player ended the game(useful for games with a distinct beginning and ending).
    public static void GameOver()
    {

    }
    //Player started a level.
    public static void LevelStart(int index, string sub_world, string world)
    {

    }
    //Player successfully completed a level.
    public static void LevelComplete(int index, string sub_world, string world)
    {

    }
    // Player lost a level.
    public static void LevelFail(int index, string sub_world, string world)
    {

    }
    //Player quit out of a level.
    public static void LevelQuit(int index, string sub_world, string world)
    {

    }
    //Player skipped past a level.
    public static void LevelSkip(int index, string sub_world, string world)
    {

    }
    //Player increased in rank or RPG-style experience level.
    public static void LevelUp()
    {

    }
    #endregion
    /// <summary>
    ///Onboarding events track the first-time user experience (FTUE).
    ///Analyze standard onboarding events to monitor the effectiveness of your tutorials. 
    /// </summary>
    #region Onboarding events


    // Player completed any interaction after opening the game for the first time.
    public static void FirstInteraction(string result)
    {

    }
    //Player began a tutorial.
    public static void TutorialStart(string tutorial_ID)
    {

    }

    //Player completed a tutorial.
    public static void TutorialComplete(string tutorial_ID)
    {

    }
    //Player skipped a tutorial.
    public static void TutorialSkip(string tutorial_ID, string tutorial_step)
    {

    }
    #endregion

    /// <summary>
    /// Engagement events help you understand whether your players are engaging with your game, and whether they’re performing actions highly correlated with retention and virality.
    /// </summary>
    #region Engagement events


    //Player enabled push notifications.
    public static void PushNotificationEnable()
    {

    }
    //Player responded to a pushed message.
    public static void PushNotificationClick()
    {

    }

    //Player completed an achievement.
    public static void AchievementUnlock()
    {

    }
    //Player completed a milestone towards an achievement.
    public static void AchievementStep()
    {

    }

    //Player shared something such as an invite or gift through a social network.
    public static void SocialSharePlayer()
    {

    }
    //Player accepted something shared through a social network.
    public static void SocialAcceptPlayer()
    {

    }

    #endregion

    /// <summary>
    ///Monetization events track income and game economy to help you balance your resources and improve sources of revenue. 
    /// </summary>
    #region Monetization events



    // Player opened a store.
    public static void StoreOpened()
    {

    }
    //Player selected an item in a store.
    public static void StoreItemClicked(string item)
    {

    }
    //Player spent real-world money to make an in-app purchase.
    public static void IAP_Transaction(string item)
    {

    }
    //Player acquired a resource within the game.
    public static void ItemAcquired(string aquire_id, float amount, int level, int sub_world, int world)
    {

    }
    //Player expended an item within the game.
    public static void ItemSpent(string spent_id, float amount, int level, int sub_world, int world)
    {

    }
    //Player had an opportunity to watch an ad and get double reward in win dialog.
    public static void DoubleRewardAdOffer()
    {

    }
    //Player started watching an ad to get double reward in win dialog.
    public static void DoubleRewardAd()
    {

    }
    //Player had an opportunity to watch an ad after shop panel.
    public static void AdClick(string source)
    {
        string eventName = string.IsNullOrEmpty(source) ? "ad_click" : new StringBuilder(source).Append("_").Append("ad_click").ToString();
    }
    //Player had an opportunity to watch an ad.
    public static void AdOffer(string source)
    {
        string eventName = string.IsNullOrEmpty(source) ? "ad_offer" : new StringBuilder(source).Append("_").Append("ad_offer").ToString();
    }

    //Player open an ad.
    public static void AdStart()
    {

    }

    //Player started watching an ad.
    public static void AdOpen()
    {
        //TODO : StartTimer
    }

    //Player closed the watching ad.
    public static void AdClose()
    {
        //TODO : StopTimer
    }

    //Player finished watching an ad.
    public static void AdComplete()
    {

    }
    //Player skipped an ad before completion.
    public static void AdSkip()
    {
    }
    //Ad Failed to show before start.
    public static void AdFail()
    {

    }
    //Player completed an action prompted by an ad.
    public static void PostAdAction()
    {

    }
    #endregion


    #endregion


    /// <summary>
    /// Core Events are session-based and device-based Analytics events.
    /// When you enable Unity Analytics for your project, Unity dispatches Core Events automatically.
    /// Core Events provide the basis for several metrics computed by the Analytics system, including:
    ///     New installs
    ///     Daily active users (DAU)
    ///     Monthly active users (MAU)
    ///     Total sessions
    ///     Sessions per user
    ///     Time spent in app
    ///     User Segments for country and platform
    ///     Revenue (when using Unity Ads and/or Unity IAP)
    /// </summary>
    #region Core Events

    //Core Events include:
    //  AppStart: Dispatched at the start of a new session.A new session starts when the user first launches an app or brings an app to the foreground after more than 30 minutes of inactivity.
    //  AppRunning: Dispatched periodically while an app runs.
    //  DeviceInfo: Dispatched the first time a user launches an app and whenever device information changes.
    #endregion

}
