using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    //public AudioMixer musicMixer;
    //public AudioMixer SFXMixer;

    public Toggle muteToggle;
    public Toggle fpsToggle;
    public Toggle fullScreenToggle;

    public Slider musicSlider;
    public Slider SFXSlider;

    public Text muteText;
    public Text fpsYesNoText;
    public Text fullScreenYesNoText;

    public float musicSliderValue;
    public float SFXSliderValue;

    Resolution[] resolutions;

    public Dropdown resolutionDropdown;

    private void OnEnable()
    {
        muteToggle.isOn = GameManager.Instance.mutedToggleValue;

        fpsToggle.isOn = GameManager.Instance.FPSToggleValue;

        fullScreenToggle.isOn = GameManager.Instance.FullScreenToggleValue;

        if (GameManager.Instance.FPSText != null)
        {
            fpsYesNoText = GameManager.Instance.FPSText;
        }

        if (GameManager.Instance.mutedText != null)
        {
            muteText = GameManager.Instance.mutedText;
        }

        if (GameManager.Instance.FullScreenText != null)
        {
            fullScreenYesNoText = GameManager.Instance.FullScreenText;
        }

        if (GameManager.Instance.isThereSaveData)
        {
            musicSlider.value = GameManager.Instance.musicSliderValue;
            SFXSlider.value = GameManager.Instance.SFXSliderValue;
        }

        else
        {
            musicSlider.value = AudioManagerScript.instance.musicSource.volume;
            SFXSlider.value = AudioManagerScript.instance.sfxSource.volume;
        }

        musicSliderValue = musicSlider.value;
        SFXSliderValue = SFXSlider.value;
    }

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        System.Array.Reverse(resolutions);

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;

        resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {

    }

    public void SetMusic(float volume)
    {
        AudioManagerScript.instance.musicSource.volume = volume;

        GameManager.Instance.musicSliderValue = volume;

        if (!GameManager.Instance.isMutedBool)
        {
            musicSliderValue = volume;
        }

    }

    public void SetSFX(float volume)
    {
        AudioManagerScript.instance.sfxSource.volume = volume;

        GameManager.Instance.SFXSliderValue = volume;

        if (!GameManager.Instance.isMutedBool)
        {
            SFXSliderValue = volume;
        }
    }

    public void Mute()
    {
        GameManager.Instance.isMutedBool = true;

        musicSlider.value = 0f;
        SFXSlider.value = 0f;

        AudioManagerScript.instance.musicSource.volume = 0f;
        AudioManagerScript.instance.sfxSource.volume = 0f;

        muteText.text = "Yes";
    }

    public void UnMute()
    {
        GameManager.Instance.isMutedBool = false;

        Debug.Log("El GameManagerMusicSlider antes de mutear es: " + GameManager.Instance.musicSliderValueBeforeMuting);

        AudioManagerScript.instance.musicSource.volume = GameManager.Instance.musicSliderValueBeforeMuting;
        AudioManagerScript.instance.sfxSource.volume = GameManager.Instance.SFXSliderValueBeforeMuting;

        musicSlider.value = GameManager.Instance.musicSliderValueBeforeMuting;
        SFXSlider.value = GameManager.Instance.SFXSliderValueBeforeMuting;

        Debug.Log("El slider de la musica es: " + musicSlider.value);


        muteText.text = "No";
    }

    public void ToggleMute()
    {
        if (muteToggle.isOn)
        {
            musicSliderValue = musicSlider.value;
            SFXSliderValue = SFXSlider.value;

            GameManager.Instance.musicSliderValueBeforeMuting = musicSliderValue;
            GameManager.Instance.SFXSliderValueBeforeMuting = SFXSliderValue;

            Mute();
        }
        else
        {
            UnMute();
        }

        GameManager.Instance.mutedToggleValue = muteToggle.isOn;
    }

    public void ShowFPS()
    {
        if (fpsToggle.isOn)
        {
            GameManager.Instance.isFPSTextActive = true;
            fpsYesNoText.text = "Yes";
        }
        else
        {
            fpsYesNoText.text = "No";

            GameManager.Instance.isFPSTextActive = false;
        }

        GameManager.Instance.FPSToggleValue = fpsToggle.isOn;
    }

    public void SetFullScreen()
    {
        if (fullScreenToggle.isOn)
        {
            GameManager.Instance.isFullScreen = true;
            Screen.fullScreen = true;
            fullScreenYesNoText.text = "Yes";
        }
        else
        {
            GameManager.Instance.isFullScreen = false;
            Screen.fullScreen = false;
            fullScreenYesNoText.text = "No";
        }

        GameManager.Instance.FullScreenToggleValue = fullScreenToggle.isOn;

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}