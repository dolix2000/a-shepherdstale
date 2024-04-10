using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.Events;

/// <summary>
/// Sound class to set different variables to use in the AudioManager.
/// </summary>
[System.Serializable]
public class Sound // Sound that can be played in the game with different properties.
{
    // Properties of the clip with default values.
    [Range(0f, 1f)] // Volume will range between 0 and 1 (with slider).
    [SerializeField]
    private float volume = 1f;

    [SerializeField]
    private AudioClip clip; // Audiofile that will be used.

    [SerializeField]
    private string name; // Stores the name of the sound.

    public string Name // Setter and Getter for the name of the sound.
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public bool loop = false; // Looping the sound if needed.

    private AudioSource source; // Reference for different GameObjects that play the sounds.

    /// <summary>
    /// Setting the sources.
    /// </summary>
    /// <param name="audioSource"></param>
    public void SetSource(AudioSource audioSource)
    {
        source = audioSource;
        source.volume = volume;
        source.clip = clip;
        source.loop = loop;
    }

    public void PlaySound()
    {
        source.Play();
    }

    public void StopSound()
    {
        source.Stop();
    }
}

/// <summary>
/// AudioManager to implement ingame sounds and music.
/// </summary>
public class AudioManager : MonoBehaviour
{

    [SerializeField] // Can be managed in Inspector but not accessible in scripts -> nice management
    private Sound[] sounds; // List of Sounds, stores sounds

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioMixerGroup masterVol;

    private AudioSource musicSource; // AudioSource for backgroundmusic
    private AudioSource sfxSource; // AudioSource for soundeffects

    private bool soundCheck = false;

    public bool SoundCheck
    {
        get { return this.soundCheck; }
        set { soundCheck = value; }
    }

    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (AudioManager.instance == null)
            {
                AudioManager.instance = GameManager.Instance.GetComponent<AudioManager>();
            }
            return AudioManager.instance;
        }
    } // Getter for the AudioManager. Returns instance.

    // Start is called before the first frame update. References CreateAudioSource().
    void Awake()
    {
        CreateAudioSource();
    }

    private void Update()
    {
        if(musicSource != null)
        {
            if (!GameManager.Instance.GameState.Equals(GameState.PAUSE_SCREEN) || soundCheck)
            {
                musicSource.UnPause();
            } else
            {
                musicSource.Pause();
            }
        }
    }

    /// <summary>
    /// Creating GameObjects for our AudioSource in the Hierarchy.
    /// </summary>
    private void CreateAudioSource()
    {
        GameObject newAudio = new GameObject("MusicSource");
        GameObject newSFX = new GameObject("SFXSource");

        musicSource = newAudio.AddComponent<AudioSource>();
        sfxSource = newSFX.AddComponent<AudioSource>();

        musicSource.outputAudioMixerGroup = masterVol;
        sfxSource.outputAudioMixerGroup = masterVol;

        newAudio.transform.SetParent(GameManager.Instance.transform);
        newSFX.transform.SetParent(GameManager.Instance.transform);
    }

    /// <summary>
    /// Setting the volume of sounds or music.
    /// </summary>
    /// <param name="volume">Volume of sound or music.</param>
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("SoundsVol", Mathf.Log(volume) * 20);
    }

    public void PlaySound(string name) // Method which will allow us to play the audio we desire
    {
        for (int i = 0; i < sounds.Length; i++) // Iterate through given sounds list.
        {
            if (sounds[i].Name == name) // If sound with the correct name is found ..
            {
                if (name.StartsWith("SFX")) // And if sound is marked with the SFX keyword..
                {
                    sounds[i].SetSource(sfxSource);
                } else
                {
                    sounds[i].SetSource(musicSource); // or not..
                }
                
                sounds[i].PlaySound(); // .. start playing sound.
                return;
            }
        }
        Debug.LogWarning("AudioManager says: No sound with that name has been found. " + name); // If no sound has been found, print a warning
    }

    public void StopSound(string name) // Method which will allow us to stop the audio
    {
        for (int i = 0; i<sounds.Length; i++) // Iterate through given sounds list.
        {
            if (sounds[i].Name == name) // If sound with the correct name is found ..
            {
                sounds[i].StopSound(); // .. stop playing sound.
                return;
            }
        }
        Debug.LogWarning("AudioManager says: No sound with that name has been found. " + name); // If no sound has been found, print a warning
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }
}

public enum SoundFiles
{
    TitleMusic,
    GameMusic,
    SFX_Shepherd,
    SFX_DogBark
}
