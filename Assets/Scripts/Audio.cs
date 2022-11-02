using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio : GenericSingleton<Audio>
{
    public AudioSource bgMusicSource;
    public AudioSource sfxSource;

    public override void Awake()
    {
        base.Awake();

        bgMusicSource = gameObject.AddComponent<AudioSource>();
        bgMusicSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();

        Util.AudioSettings loaded = Util.LoadSoundSettings();

        bgMusicSource.volume = loaded.bgVolume;
        sfxSource.volume = loaded.sfxVolume;

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            bgMusicSource.clip = FindObjectOfType<AudioHolder>().bgMusic;
            bgMusicSource.Play();
        };
    }

    public void SetBGAudioVolume(float volume)
    {
        bgMusicSource.volume = volume;
    }
    public void SetSFXAudioVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void PlaySFX(AudioClip sfx)
    {
        sfxSource.PlayOneShot(sfx);
    }


    private void OnApplicationQuit()
    {
        Util.SaveSoundSettings(new Util.AudioSettings(bgMusicSource.volume, sfxSource.volume));
    }
}


