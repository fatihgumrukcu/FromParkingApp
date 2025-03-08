using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsSceneManager : MonoBehaviour
{
    public Button soundToggleButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public AudioSource clickSound;  // 🎵 Ses efekti için AudioSource

    private bool isSoundOn;

    void Start()
    {
        isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        UpdateSoundButton();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateSoundButton();

        // 🎵 **Tıklama sesini çal**
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }

    void UpdateSoundButton()
    {
        soundToggleButton.image.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
        AudioListener.volume = isSoundOn ? 1f : 0f;
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
