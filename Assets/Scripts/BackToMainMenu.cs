using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{
    private Button backButton;

    void Start()
    {
        backButton = GetComponent<Button>();

        if (backButton != null)
        {
            backButton.onClick.AddListener(GoToMainMenu);
        }
        else
        {
            Debug.LogError("🚨 Hata: Butona BackToMainMenu scripti atanmış ancak Button bileşeni yok!");
        }
    }

    public void GoToMainMenu()
    {
        Debug.Log("🔙 Ana menüye dönülüyor...");
    
       
        SceneManager.LoadScene("MainMenu"); // 📌 Ana menü sahnesini yükle
    }
}
