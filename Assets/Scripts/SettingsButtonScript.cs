using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButtonScript : MonoBehaviour
{
    public void OpenSettingsScene()
    {
        Debug.Log("⚙️ Ayarlar sahnesine geçiliyor...");
        SceneManager.LoadScene("SettingSceneManager");
    }
}
