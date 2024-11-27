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

        musicSource.loop = true; // Para m�sica de fondo
    }

    // M�todo para reproducir m�sica
    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource.isPlaying && musicSource.clip == musicClip)
        {
            musicSource.Stop(); // Detener si est� sonando para reiniciar desde el principio
        }

        musicSource.clip = musicClip;
        musicSource.Play();
    }

    // M�todo para detener la m�sica
    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    // M�todo para ajustar el volumen de la m�sica
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume); // Asegura que el volumen est� entre 0 y 1
    }

    // M�todo para reproducir efectos de sonido (SFX)
    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip);
    }

    // M�todo para ajustar el volumen de los efectos de sonido
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume); // Asegura que el volumen est� entre 0 y 1
    }
}
