using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonManager : MonoBehaviour
{
    public AudioSource clickSound;  // 🎵 Ses efekti için AudioSource

    public void GoToMainMenu()
    {
        Debug.Log("🔙 Main Menu'ye dönülüyor...");

        // 🎵 **Tıklama sesini çal**
        if (clickSound != null)
        {
            clickSound.Play();
        }
        Time.timeScale = 1f;
        // **Sahneyi yüklemeyi ses çaldıktan sonra yap**
        Invoke("LoadMainMenu", 0.1f); // 0.2 saniye gecikmeli yükleme
    }

   void LoadMainMenu()
{
    Debug.Log("🎮 MainMenu sahnesi yükleniyor...");

    if (Application.CanStreamedLevelBeLoaded("MainMenu"))
    {
        Debug.Log("✅ Sahne bulundu, yükleniyor...");
        SceneManager.LoadScene("MainMenu");
    }
    else
    {
        Debug.LogError("❌ Hata! MainMenu sahnesi Build Settings'e ekli olmayabilir.");
    }
}

}
