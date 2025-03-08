using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;
using System;

public class BannerAdManager : MonoBehaviour
{
    private BannerView _bannerView;

    #if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3607553271294307/2895037907"; // Android için gerçek reklam ID
    #else
    private string _adUnitId = "unused"; // Android dışı platformlar için (iOS vb.)
    #endif

    void Start()
    {
        #if UNITY_ANDROID
        // AdMob başlat
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob başlatıldı!");
            CreateBannerView();
        });
        #endif
    }

    public void CreateBannerView()
    {
        #if UNITY_ANDROID
        Debug.Log("Creating banner view...");

        // Eğer zaten bir banner varsa, önce eskiyi yok et
        DestroyBannerAd();

        // Yeni banner oluştur
        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Reklam isteği oluştur ve yükle
        AdRequest request = new AdRequest();
        _bannerView.LoadAd(request);

        Debug.Log("Banner reklam yüklendi.");
        #endif
    }

    // Banner reklamını kaldırmak için
    public void DestroyBannerAd()
    {
        #if UNITY_ANDROID
        if (_bannerView != null)
        {
            _bannerView.Destroy(); // Banner'ı kaldır
            _bannerView = null; // Belleği temizle
            Debug.Log("Banner reklamı yok edildi.");
        }
        #endif
    }
}
