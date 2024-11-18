using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

    // ADVERTISEMENT GUWOP BABY
    private string playStoreID = "3560572";
    private string BannerAdvert = "banner";

    public bool isTargetPlayStore;
    public bool isTestAd;

    // Use this for initialization
    void Start () {
        Advertisement.Initialize(playStoreID, isTestAd);
        StartCoroutine(ShowBannerWhenReady());
    } // Start

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(BannerAdvert))
        {
            yield return new WaitForSeconds(0.5f);
        }
        //Advertisement.Banner.Show(BannerAdvert);
    }
}
