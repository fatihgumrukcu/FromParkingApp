using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;
using System;

public class LevelBannerAdManager : MonoBehaviour
{
    private BannerView _bannerView;

    #if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3607553271294307/2895037907"; // GERÇEK ID
    #else
    private string _adUnitId = "unused"; // Android dışı platformlar için
    #endif

    void Start()
    {
        // AdMob başlat
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("AdMob başlatıldı! Level ekranına banner ekleniyor.");
            CreateBannerView();
        });
    }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view for Level Selection...");

        // Eğer zaten bir banner varsa, önce eskiyi yok et
        DestroyBannerAd();

        // Yeni banner oluştur
        _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Reklam olaylarını dinle (Google Mobile Ads SDK 2023+ için)
        _bannerView.OnBannerAdLoaded += () => Debug.Log("Level Selection banner yüklendi.");
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) => Debug.LogError($"Level Selection banner yüklenemedi: {error.GetMessage()}");
        _bannerView.OnAdFullScreenContentOpened += () => Debug.Log("Level Selection banner açıldı.");
        _bannerView.OnAdFullScreenContentClosed += () => Debug.Log("Level Selection banner kapatıldı.");

        // Reklam yükle
        // AdRequest oluşturma (GÜNCEL SÜRÜM)
            AdRequest request = new AdRequest();

        _bannerView.LoadAd(request);
    }

    public void DestroyBannerAd()
    {
        if (_bannerView != null)
        {
            _bannerView.Destroy(); // Banner'ı kapat
            _bannerView = null; // Belleği temizle
            Debug.Log("Level Selection banner reklamı yok edildi.");
        }
    }

    // Sahne değiştiğinde banner'ı kaldır
    void OnDestroy()
    {
        DestroyBannerAd();
    }
}