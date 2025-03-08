using UnityEngine;
using System.Collections;

public class CarCollisionSound : MonoBehaviour
{
    public AudioClip crashSound; // Tek bir çarpışma sesi
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = crashSound;
        audioSource.volume = 0.3f; // Daha düşük başlangıç sesi
        audioSource.playOnAwake = false; 
        audioSource.spatialBlend = 0f; // 2D ses
        audioSource.priority = 128;
        audioSource.pitch = 1.0f; // Sabit pitch
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float impactForce = collision.relativeVelocity.magnitude; // Çarpışma gücü

        if (impactForce > 0.5f) // Çok hafif temasları engelle
        {
            // Çarpışma şiddetine göre ses seviyesini çok daha düşük tutuyoruz
            float volume = Mathf.Clamp(impactForce / 25f, 0.1f, 0.5f); // Ses şiddetini daha da yumuşat
            audioSource.pitch = Mathf.Clamp(1.0f - impactForce / 30f, 0.8f, 1.2f); // Çarpışma şiddetine göre pitch
            audioSource.PlayOneShot(crashSound, volume);
            StartCoroutine(FadeOut(audioSource, 1.5f)); // 1.5 saniyede fade-out
        }
    }

    IEnumerator FadeOut(AudioSource source, float fadeTime)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        source.Stop();
        source.volume = startVolume; // Ses seviyesini sıfırlayıp tekrar kullanıma hazır hale getir
    }
}
