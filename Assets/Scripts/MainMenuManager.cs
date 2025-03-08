using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OpenLevelSelection()
    {
        Debug.Log("📌 Level Selection sahnesine gidiliyor...");
        SceneManager.LoadScene("LevelSelection"); // Sahne ismi Unity Build Settings ile aynı olmalı
    }
}
