using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource backgroundMusic;

    private float defaultVolume = 0.3f; // 🎵 **Başlangıç ses seviyesi (0.0 - 1.0 arasında)**
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 🎵 **Eğer PlayerPrefs'te kayıtlı ses seviyesi varsa onu yükle, yoksa varsayılanı ata**
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", defaultVolume);
        backgroundMusic.volume = savedVolume;

        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Level")) // **Eğer bir oyun seviyesi açıldıysa müziği durdur**
        {
            if (backgroundMusic.isPlaying)
            {
                backgroundMusic.Pause();
            }
        }
        else if (scene.name == "MainMenu") // **Eğer ana menüye dönüldüyse müziği tekrar başlat**
        {
            if (!backgroundMusic.isPlaying)
            {
                backgroundMusic.Play();
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        backgroundMusic.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
