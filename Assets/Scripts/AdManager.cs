using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener
{
    public static AdManager Instance { get; private set; }

    [SerializeField] string gameID;
    [SerializeField] string rewardedVideoPlacementId;
    [SerializeField] bool testMode;

    private void Awake()
    {
        if (AdManager.Instance == null)
        {
            Instance = this;
            Advertisement.Initialize(gameID, testMode, this);
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void ShowRewardedAd(IUnityAdsShowListener argListener)
    {
        Advertisement.Show(rewardedVideoPlacementId, argListener);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads initialization Failed: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Unity Ads Loaded.");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Unity Ads Failed Loaded.");
    }
}
