using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    private CanvasGroup canvasGroup;

    public GameObject restartButton; // 🔄 Restart Butonu
    public GameObject nextButton; // ⏩ Next Butonu (Gözükmeli ama tıklanamaz olacak)

    public AudioSource gameOverSound; // 🎵 **Game Over Müzik** (Inspector'dan atayacağız)

    private int collisionCount = 0; // 💥 Çarpışma sayacı
    private int maxCollisions = 3; // 🚨 Maksimum çarpışma

    void Start()
    {
        canvasGroup = gameOverPanel.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogWarning("⚠️ CanvasGroup bulunamadı! Yeni ekleniyor...");
            canvasGroup = gameOverPanel.AddComponent<CanvasGroup>();
        }

        SetPanelVisibility(false); // **Başlangıçta kapalı tut**

        // **Next Button pasif ama görünür olacak**
        if (nextButton != null)
        {
            nextButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }

    // 💥 **Sadece ShortCar'ın LongCar'a Çarpmasını Say!**
    public void ShortCarCollisionDetected(GameObject shortCar, GameObject collidedObject)
    {
        Debug.Log($"🚗 Çarpışma Algılandı! Çarpan: {shortCar.name}, Çarpışılan: {collidedObject.name}");

        // **Eğer çarpışan iki obje de LongCar ise sayma**
        if (shortCar.CompareTag("LongCar") && collidedObject.CompareTag("LongCar"))
        {
            Debug.Log("⚠️ İki LongCar çarpıştı, sayılmadı!");
            return;
        }

        // **Eğer çarpışan obje ShortCar değilse sayma**
        if (!shortCar.CompareTag("ShortCar"))
        {
            Debug.Log("⚠️ Çarpışan obje ShortCar değil, sayılmadı!");
            return;
        }

        // **Eğer çarpılan obje LongCar değilse sayma**
        if (!collidedObject.CompareTag("LongCar"))
        {
            Debug.Log("⚠️ Çarpılan obje LongCar değil, sayılmadı!");
            return;
        }

        collisionCount++; // 🔄 Çarpışma sayısını artır
        Debug.Log($"💥 ShortCar, LongCar'a Çarptı! Çarpışma Sayısı: {collisionCount}/{maxCollisions}");

        // 🚨 **3 çarpışmadan sonra Game Over Panelini aç**
        if (collisionCount >= maxCollisions)
        {
            Debug.Log("🔥 3. Çarpışma! Game Over Panel Açılıyor...");
            ShowGameOverPanel();
        }
    }

    // 🎉 **OYUN BİTTİ (GameOver Panel Aç)**
    public void ShowGameOverPanel()
    {
        Debug.Log("🎉 GAME OVER PANEL AÇILDI!");
        SetPanelVisibility(true);

        // 🔊 **Game Over müziğini çal (Eğer atandıysa)**
        if (gameOverSound != null && !gameOverSound.isPlaying)
        {
            gameOverSound.Play();
            Debug.Log("🎵 Game Over müziği çalıyor...");
        }
        else
        {
            Debug.LogWarning("⚠️ Game Over müziği atanmamış veya zaten çalıyor!");
        }

        // 🔄 **Restart Butonu Açık Kalacak**
        if (restartButton != null)
        {
            restartButton.SetActive(true);
        }

        // ❌ **Next Butonu Pasif (Tıklanamaz) Ama Görünür**
        if (nextButton != null)
        {
            nextButton.GetComponent<UnityEngine.UI.Button>().interactable = false; // Tıklanamaz
            nextButton.SetActive(true); // Ama gözükmeye devam etsin
        }

        Time.timeScale = 0f;  // **Oyunu durdur**
    }

    // 🔄 **Oyunu Tekrar Başlat**
    public void RestartGame()
    {
        Debug.Log("🔄 Oyun yeniden başlatılıyor...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 📌 **GameOver Paneli Alpha Değeriyle Aç/Kapat**
    private void SetPanelVisibility(bool isVisible)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = isVisible ? 1 : 0;
            canvasGroup.interactable = isVisible;
            canvasGroup.blocksRaycasts = isVisible;
            Debug.Log($"🔄 GameOver Panel {(isVisible ? "Açıldı" : "Kapandı")}");
        }
        else
        {
            Debug.LogError("🚨 CanvasGroup atanmadı! Alpha değiştirilemedi.");
        }
    }
}
