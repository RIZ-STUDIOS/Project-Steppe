using ProjectSteppe.Managers;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GeneralOptions : MonoBehaviour
{
    [Header("Resolution Settings")]
    private List<ResItem> resolutions = new List<ResItem>();
    [SerializeField]
    private TMP_Text resText;
    private int resolutionIndex;

    [SerializeField]
    private TextMeshProUGUI grassDensityText;
    private int grassDensityIndex;

    [Header("Audio Settings")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private TMP_Text masterVolText, musicVolText, sfxVolText;
    [SerializeField]
    private Slider masterVolSlider, musicVolSlider, sfxVolSlider;
    private void Start()
    {
        resolutions.Clear();
        foreach (var res in Screen.resolutions)
        {
            resolutions.Add(new ResItem()
            {
                horizontal = res.width,
                vertical = res.height,
                refreshRate = res.refreshRateRatio
            });
        }
        for (int i = 0; i < resolutions.Count; i++)
        {
            var res = resolutions[i];
            var currentRes = new UnityEngine.Resolution()
            {
                height = res.vertical,
                width = res.horizontal,
                refreshRateRatio = res.refreshRate
            };
            if (Screen.currentResolution.Equals(currentRes))
            {
                resolutionIndex = i;

                UpdateResolution();
            }
        }

        /*float vol = 0f;
        if (!audioMixer.GetFloat("MasterVol", out vol))
            vol = 0;
        masterVolSlider.value = Mathf.Pow(10, vol/20f);
        if (!audioMixer.GetFloat("MusicVol", out vol))
            vol = 0;
        musicVolSlider.value = Mathf.Pow(10, vol / 20f);
        if (!audioMixer.GetFloat("SFXVol", out vol))
            vol = 0;
        sfxVolSlider.value = Mathf.Pow(10, vol/20f);

        masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 100).ToString() + "%";
        musicVolText.text = Mathf.RoundToInt(musicVolSlider.value * 100).ToString() + "%";
        sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 100).ToString() + "%";*/

        masterVolSlider.value = PlayerPrefs.GetFloat("MasterVol", 1);
        musicVolSlider.value = PlayerPrefs.GetFloat("MusicVol", 1);
        sfxVolSlider.value = PlayerPrefs.GetFloat("SFXVol", 1);
        grassDensityIndex = PlayerPrefs.GetInt("grassDensity", (int)GrassDensity.High);

        SetMasterVolume();
        SetMusicVolume();
        SetSfxVolume();

        UpdateGrassDensity();
    }
    #region Resolution
    private void UpdateResolution()
    {
        var currentRes = resolutions[resolutionIndex];
        resText.text = currentRes.horizontal.ToString() + " x " + currentRes.vertical.ToString() + " " + (currentRes.refreshRate.numerator / (float)currentRes.refreshRate.denominator).ToString("F0") + "hz";
        Screen.SetResolution(currentRes.horizontal, currentRes.vertical, FullScreenMode.FullScreenWindow, currentRes.refreshRate);
    }
    public void ResLeft()
    {
        //Debug.Log("S");
        resolutionIndex--;
        if (resolutionIndex < 0)
        {
            resolutionIndex = 0;
        }
        UpdateResolution();
    }
    public void ResRight()
    {
        //Debug.Log("S");
        resolutionIndex++;
        if (resolutionIndex > resolutions.Count - 1)
        {
            resolutionIndex = resolutions.Count - 1;
        }
        UpdateResolution();
    }

    public void GrassDensityLeft()
    {
        grassDensityIndex--;
        if(grassDensityIndex < 0)
        {
            grassDensityIndex = 0;
        }
        UpdateGrassDensity();
    }

    public void GrassDensityRight()
    {
        grassDensityIndex++;
        if(grassDensityIndex >= (int)GrassDensity.High)
        {
            grassDensityIndex = (int)GrassDensity.High;
        }
        UpdateGrassDensity();
    }

    private void UpdateGrassDensity()
    {
        grassDensityText.text = ((GrassDensity)grassDensityIndex).ToString();
        PlayerPrefs.SetInt("grassDensity", grassDensityIndex);
        if (GrassDensityManager.Instance)
        {
            GrassDensityManager.Instance.UpdateGrass();
        }
    }

    [System.Serializable]
    public class ResItem
    {
        public int horizontal, vertical;
        public RefreshRate refreshRate;
    }
    #endregion
    #region Audio
    public void SetMasterVolume()
    {
        masterVolText.text = Mathf.RoundToInt(masterVolSlider.value * 100).ToString() + "%";
        if (masterVolSlider.value <= 0)
            audioMixer.SetFloat("MasterVol", -80);
        else
            audioMixer.SetFloat("MasterVol", Mathf.Log10(masterVolSlider.value) * 20);
        PlayerPrefs.SetFloat("MasterVol", masterVolSlider.value);
    }
    public void SetMusicVolume()
    {
        musicVolText.text = Mathf.RoundToInt(musicVolSlider.value * 100).ToString() + "%";
        if (musicVolSlider.value <= 0)
            audioMixer.SetFloat("MusicVol", -80);
        else
            audioMixer.SetFloat("MusicVol", Mathf.Log10(musicVolSlider.value) * 20);
        PlayerPrefs.SetFloat("MusicVol", musicVolSlider.value);
    }
    public void SetSfxVolume()
    {
        sfxVolText.text = Mathf.RoundToInt(sfxVolSlider.value * 100).ToString() + "%";
        if (sfxVolSlider.value <= 0)
            audioMixer.SetFloat("SFXVol", -80);
        else
            audioMixer.SetFloat("SFXVol", Mathf.Log10(sfxVolSlider.value) * 20);
        PlayerPrefs.SetFloat("SFXVol", sfxVolSlider.value);
    }
    #endregion
}