using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance { get; private set; }

    // Audio sources
    private AudioSource musicSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        // Asegurar que solo haya una instancia del AudioManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Configurar los AudioSources
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true; // Para música de fondo
    }

    // Método para reproducir música
    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource.isPlaying && musicSource.clip == musicClip)
        {
            musicSource.Stop(); // Detener si está sonando para reiniciar desde el principio
        }

        musicSource.clip = musicClip;
        musicSource.Play();
    }

    // Método para detener la música
    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    // Método para ajustar el volumen de la música
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume); // Asegura que el volumen esté entre 0 y 1
    }

    // Método para reproducir efectos de sonido (SFX)
    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }

    // Método para ajustar el volumen de los efectos de sonido
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume); // Asegura que el volumen esté entre 0 y 1
    }
}
