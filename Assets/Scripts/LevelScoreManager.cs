using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelScoreManager : MonoBehaviour
{
    public GameObject levelScorePanel;
    private CanvasGroup canvasGroup;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;

    private int totalLevels = 20; // 📌 **Toplam level sayısı**

    void Start()
    {
        Debug.Log("🟢 LevelScoreManager başlatıldı.");
        
        Time.timeScale = 1f; // **Oyun tekrar oynanabilir hale getirildi.**

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int currentLevel = sceneIndex - 1; 

        Debug.Log($"📌 Mevcut Level: {currentLevel}");

        int score = PlayerPrefs.GetInt("PlayerScore", 0);
        int unlockedLevel = PlayerPrefs.GetInt("HighestUnlockedLevel", 1);

        Debug.Log($"📌 Yüklenen Veriler: Level: {currentLevel}, Skor: {score}, Açık Level: {unlockedLevel}");

        if (levelScorePanel == null)
        {
            Debug.LogError("🚨 levelScorePanel atanmadı!");
            return;
        }

        canvasGroup = levelScorePanel.GetComponent<CanvasGroup>() ?? levelScorePanel.AddComponent<CanvasGroup>();
        SetPanelVisibility(false);

        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.Save();

        UpdateUI();
    }

    public void ShowLevelCompletePanel()
    {
        Debug.Log("🏆 LEVEL COMPLETE PANEL AÇILDI!");

        if (levelScorePanel == null)
        {
            Debug.LogError("🚨 levelScorePanel bulunamadı!");
            return;
        }

        SetPanelVisibility(true);

        int score = PlayerPrefs.GetInt("PlayerScore", 0);
        score += 100;
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.Save();

        UnlockNextLevel();  
        UpdateUI(); 
        
        Debug.Log($"✅ Bölüm tamamlandı! Yeni Skor: {score}");

        Time.timeScale = 0f;
    }

    private void SetPanelVisibility(bool isVisible)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = isVisible ? 1 : 0;
            canvasGroup.interactable = isVisible;
            canvasGroup.blocksRaycasts = isVisible;
            Debug.Log($"🔄 Panel {(isVisible ? "Açıldı" : "Kapatıldı")}");
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        int nextLevel = currentLevel + 1; 

        if (nextLevel <= totalLevels) 
        {
            PlayerPrefs.SetInt("CurrentLevel", nextLevel);
            PlayerPrefs.Save();
            SceneManager.LoadScene(nextLevel + 1); 
        }
        else
        {
            ResetGameData();
            SceneManager.LoadScene(2); 
        }
    }

    private void UnlockNextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        int unlockedLevel = PlayerPrefs.GetInt("HighestUnlockedLevel", 1);

        int newUnlockedLevel = Mathf.Max(currentLevel + 1, unlockedLevel);

        PlayerPrefs.SetInt("HighestUnlockedLevel", newUnlockedLevel);
        PlayerPrefs.Save();

        Debug.Log($"✅ Yeni level açıldı: {newUnlockedLevel}");
    }

    private void UpdateUI()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        int score = PlayerPrefs.GetInt("PlayerScore", 0);

        if (levelText != null) levelText.text = "Level " + currentLevel;
        if (scoreText != null) scoreText.text = "Skor: " + score;
    }

    private void ResetGameData()
    {
        PlayerPrefs.SetInt("CurrentLevel", 1);
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.SetInt("HighestUnlockedLevel", 1);
        PlayerPrefs.Save();
    }
}
