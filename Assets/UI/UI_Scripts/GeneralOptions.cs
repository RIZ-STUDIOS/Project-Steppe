using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class GeneralOptions : MonoBehaviour
{
    [Header("Resolution Settings")]
    [SerializeField]
    private List<ResItem> resolutions = new List<ResItem>();
    [SerializeField]
    private TMP_Text resText;
    private int resolutionIndex;

    [Header("Audio Settings")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private TMP_Text masterVolText, musicVolText, sfxVolText;
    [SerializeField]
    private Slider masterVolSlider, musicVolSlider, sfxVolSlider;
    private void Start()
    {
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                resolutionIndex = i;

                UpdateResolution();
            }
        }

        float vol = 0f;
        audioMixer.GetFloat("MasterVol", out vol);
        masterVolSlider.value = vol;
        audioMixer.GetFloat("MusicVol", out vol);
        musicVolSlider.value = vol;
        audioMixer.GetFloat("SFXVol", out vol);
        sfxVolSlider.value = vol;

        masterVolText.text = Mathf.RoundToInt(masterVolSlider.value + 80).ToString() + "%";
        musicVolText.text = Mathf.RoundToInt(musicVolSlider.value + 80).ToString() + "%";
        sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value + 80).ToString() + "%";
    }
    #region Resolution
    private void UpdateResolution()
    {
        resText.text = resolutions[resolutionIndex].horizontal.ToString() + " x " + resolutions[resolutionIndex].vertical.ToString();
        Screen.SetResolution(resolutions[resolutionIndex].horizontal, resolutions[resolutionIndex].vertical, true);
    }
    public void ResLeft()
    {
        resolutionIndex--;
        if (resolutionIndex < 0)
        {
            resolutionIndex = 0;
        }
        UpdateResolution();
    }
    public void ResRight()
    {
        resolutionIndex++;
        if (resolutionIndex > resolutions.Count - 1)
        {
            resolutionIndex = resolutions.Count - 1;
        }
        UpdateResolution();
    }
    [System.Serializable]
    public class ResItem
    {
        public int horizontal, vertical;
    }
    #endregion
    #region Audio
    public void SetMasterVolume()
    {
        masterVolText.text = Mathf.RoundToInt(masterVolSlider.value + 80).ToString() + "%";
        audioMixer.SetFloat("MasterVol", masterVolSlider.value);
        PlayerPrefs.SetFloat("MasterVol", masterVolSlider.value);
    }
    public void SetMusicVolume()
    {
        musicVolText.text = Mathf.RoundToInt(musicVolSlider.value + 80).ToString() + "%";
        audioMixer.SetFloat("MusicVol", musicVolSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicVolSlider.value);
    }
    public void SetSfxVolume()
    {
        sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value + 80).ToString() + "%";
        audioMixer.SetFloat("SFXVol", sfxVolSlider.value);
        PlayerPrefs.SetFloat("SFXVol", sfxVolSlider.value);
    }
    #endregion
}