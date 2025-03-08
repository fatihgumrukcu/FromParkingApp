using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionManager : MonoBehaviour
{
    public Button[] levelButtons;
    public Sprite unlockedSprite;
    public Sprite lockedSprite;

    void Start()
    {
        Debug.Log("🚀 LevelSelectManager başlatıldı!");

        if (levelButtons == null || levelButtons.Length == 0)
        {
            Debug.LogError("❌ levelButtons dizisi boş! Inspector'da butonları eklediğinden emin ol.");
            return;
        }

        UpdateLevelButtons();
    }

    private void UpdateLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("HighestUnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;

            if (levelIndex <= unlockedLevel)
            {
                levelButtons[i].interactable = true;
                levelButtons[i].GetComponent<Image>().sprite = unlockedSprite;
                levelButtons[i].transform.GetChild(0).gameObject.SetActive(true);

                int capturedIndex = levelIndex;
                levelButtons[i].onClick.RemoveAllListeners();
                levelButtons[i].onClick.AddListener(() => LoadLevel(capturedIndex));
            }
            else
            {
                levelButtons[i].interactable = false;
                levelButtons[i].GetComponent<Image>().sprite = lockedSprite;
                levelButtons[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        string sceneName = "Level " + levelIndex;

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"❌ Hata! {sceneName} sahnesi bulunamadı!");
        }
    }

    private void OnEnable()
    {
        UpdateLevelButtons();
    }
}
