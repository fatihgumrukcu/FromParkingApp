using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMainMenu : MonoBehaviour
{
    // **Ana menüye dönüş fonksiyonu**
    public void GoToMainMenu()
    {
        Debug.Log("🔙 Ana menüye dönülüyor...");

        if (Application.CanStreamedLevelBeLoaded("MainMenu"))
        {
            Debug.Log("✅ MainMenu sahnesi yükleniyor...");
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.LogError("❌ Hata! MainMenu sahnesi Build Settings'te ekli olmayabilir.");
        }
    }
}
