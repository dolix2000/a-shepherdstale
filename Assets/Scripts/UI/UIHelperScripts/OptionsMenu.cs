using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Options helper to set game resolution, toggle fullscreen mode and change volume.
/// Issue: Menu elements get messed up on resolution change, as their original transform point travels/gets translated to new coordinates.
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    void Start()
    {
        FetchScreenResolutions();
    }

    /// <summary>
    /// Resolution Dropdown values get pulled from Unity's Engine.
    /// Original Code by Brackeys.
    /// </summary>
    public void FetchScreenResolutions()
    {
        resolutions = Screen.resolutions; // Fetches supported resolutions

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Setting Volume through Audiomanager, uses dynamic float via slider in game.
    /// </summary>
    public void SetVolume (float volume) 
    {
        AudioManager.Instance.SetVolume(volume);
    }

    /// <summary>
    /// Soundcheck: As game music pauses as well, this will continue the music during slider manipulation to check volume in pause mode.
    /// </summary>
    public void EnableSoundCheck()
    {
        AudioManager.Instance.SoundCheck = true;
    }

    /// <summary>
    /// Soundcheck: Disables SoundCheck after releasing the slider.
    /// </summary>
    public void DisableSoundCheck()
    {
        AudioManager.Instance.SoundCheck = false;
    }

    /// <summary>
    /// Set resolution chosen in dropdown menu.
    /// </summary>
    public void SetResolution (int resolutionIndex)
    {
       Resolution resolution = resolutions[resolutionIndex];
       Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Fullscreen Toggle - Dynamic bool to switch mode.
    /// </summary>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
