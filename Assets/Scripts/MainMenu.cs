using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
{
    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);  // 🔄 İmleci varsayılan hale getir
    Cursor.visible = true;  // 🔄 Eğer gizlenmişse, tekrar görünür yap
}

    public void StartGame()
    {
        // Oyun başlatıldığında Level 1'i yüklerken PlayerPrefs verisi doğru şekilde yüklenecek
        PlayerPrefs.SetInt("CurrentLevel", 1);  // Level 1'e başlatılacak
        PlayerPrefs.SetInt("Score", 0);          // Skoru sıfırla
        PlayerPrefs.Save();                      // PlayerPrefs'i kaydet
        SceneManager.LoadScene(1); // 1. index -> Level 1
    }

    public void QuitGame()
    {
        Debug.Log("Oyun Kapatıldı!");
        Application.Quit();
    }
}
