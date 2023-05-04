using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class Ads : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private Coroutine showAd;
    private string gameId = "5268204", type = "Rewarded_Android";
    private bool testMode = true, needToStop;

    private static int countLoses;


    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, testMode, this);
        }
    }

    private void Start()
    {
        countLoses++;
        if (countLoses % 3 == 0)
            showAd = StartCoroutine(ShowAd());
    }

    private void Update()
    {
        if(needToStop)
        {
            needToStop = false;
            StopCoroutine(showAd);
        }
    }

    IEnumerator ShowAd()
    {
        while (true)
        {
            Debug.Log("Showing Ad: " + gameId);
            Advertisement.Show(type, this);
            needToStop = true;
            yield return new WaitForSeconds(1f);
        }
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + gameId);
        Advertisement.Load(gameId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializationComplete()
    {
        // throw new System.NotImplementedException();
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        // throw new System.NotImplementedException();
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
