using UnityEngine;
using System.Collections;

public class ShortCarExit : MonoBehaviour
{
    public AudioSource exitSound; // 🎵 Çıkış sesi
    public LevelScoreManager levelScoreManager; // **Level Score Panel açmak için referans**

    private bool exiting = false;
    public float exitSpeed = 2f; // 🚀 **Çıkış hızı**
    public float exitDistance = 1.5f; // 📏 **Çıkış mesafesi**
    public float exitDuration = 1f; // ⏳ **Aracın çıkış süresi**

    void Start()
    {
        // **LevelScoreManager'ı otomatik bul**
        levelScoreManager = FindFirstObjectByType<LevelScoreManager>();

        if (levelScoreManager == null)
        {
            Debug.LogError("🚨 LevelScoreManager bulunamadı!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ShortCar") && !exiting)
        {
            Debug.Log("🚗💨 ShortCar çıkış noktasına ulaştı!");
            StartCoroutine(ExitSequence(other.gameObject));
        }
    }

    IEnumerator ExitSequence(GameObject shortCar)
    {
        exiting = true;
        Rigidbody2D rb = shortCar.GetComponent<Rigidbody2D>();

        Debug.Log("🚀 ShortCar çıkıyor...");

        // **Rigidbody'yi Kinematic yap**
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        // **Hedef Pozisyon**
        Vector3 startPos = shortCar.transform.position;
        Vector3 targetPos = startPos + new Vector3(exitDistance, 0, 0);

        float elapsedTime = 0f;

        // **Düzgün ve sabit hızda çıkış yap**
        while (elapsedTime < exitDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / exitDuration;
            shortCar.transform.position = Vector3.Lerp(startPos, targetPos, progress);
            yield return null;
        }

        shortCar.transform.position = targetPos; // **Son konumu sabitle**
        Debug.Log("🚗✅ ShortCar tamamen çıktı!");

        // 🔊 **Çıkış sesi çal (varsa)**
        if (exitSound != null)
        {
            Debug.Log("🎵 Çıkış sesi çalınıyor...");
            exitSound.Play();
        }

        // 🎉 **Panel Daha Hızlı Açılacak**
        yield return new WaitForSeconds(0.3f); // **1 saniye yerine 0.3 saniye bekle**
        levelScoreManager?.ShowLevelCompletePanel();
    }
}
