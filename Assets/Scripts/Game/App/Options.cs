using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

struct MusicStruct
{
    public static string[] SongNames =
    {
        "Random",
        "Theme Song"
    };
};

public class Options : MonoBehaviour
{
    [SerializeField] private GameObject optionsObject;
    [SerializeField] private TMP_Dropdown resDropdown;
    [SerializeField] private TMP_Dropdown songDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle fpsToggle;
    [SerializeField] private Toggle antiAliasToggle;
    [SerializeField] private Slider audioVolume;

    private Resolution[] resolutions;
    private List<Vector2Int> uniqueResolutions = new List<Vector2Int>();


    void Update()
    {
        CONSTANTS.INOPTIONSMENU = optionsObject.activeInHierarchy;
    }

    void Awake()
    {
        AppSettings.LoadSettings();

        Screen.SetResolution(AppSettings.width, AppSettings.height, AppSettings.fullScreen);
        Screen.fullScreen = AppSettings.fullScreen;
    }

    void OnEnable()
    {
        AppSettings.LoadSettings();
    }

    void Start()
    {
        optionsObject.SetActive(false);

        PopulateResolutionDropdown();
        PopulateSongDropdown();

        int selectedIndex = FindResolutionIndex(AppSettings.width, AppSettings.height);
        if (selectedIndex >= 0)
        {
            resDropdown.value = selectedIndex;
            resDropdown.RefreshShownValue();
        }

        int songSelected = PlayerPrefs.GetInt("Music_SongIndex", 0);
        if (songSelected >= 0)
        {
            songDropdown.value = songSelected;
            songDropdown.RefreshShownValue();
        }

        antiAliasToggle.isOn = AppSettings.antiAlias;
        fpsToggle.isOn = AppSettings.showFPS;
        audioVolume.value = AppSettings.gameVolume;
        fullscreenToggle.isOn = AppSettings.fullScreen;
    }

    public void Aliased(bool antialiased)
    {
        PlayerPrefs.SetInt("AntiAlias", antialiased ? 1 : 0);
        PlayerPrefs.Save();

        AppSettings.antiAlias = antialiased;
        AppSettings.SaveSettings();

        //AppSettings.SaveSettings(); allows for a live update which loads the settings afterward, making this more like a circuit than a script
    }

    void PopulateSongDropdown()
    {
        songDropdown.ClearOptions();

        List<string> options = new List<string>();

        foreach (string song in MusicStruct.SongNames)
        {
            options.Add(song);
        }

        songDropdown.AddOptions(options);
    }

    public void NewVolume()
    {
        PlayerPrefs.SetFloat("Volume", audioVolume.value);
        PlayerPrefs.Save();

        AppSettings.SaveSettings();
    }

    void PopulateResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        uniqueResolutions.Clear();

        List<string> options = new List<string>();
        HashSet<string> added = new HashSet<string>();

        foreach (Resolution res in resolutions)
        {
            string label = $"{res.width} x {res.height}";
            Vector2Int resSize = new Vector2Int(res.width, res.height);

            if (!added.Contains(label))
            {
                options.Add(label);
                added.Add(label);
                uniqueResolutions.Add(resSize);
            }
        }

        resDropdown.AddOptions(options);
    }

    public void ApplySong(int songIndex)
    {
        PlayerPrefs.SetInt("Music_SongIndex", songIndex);
        PlayerPrefs.Save();
    }

    int FindResolutionIndex(int width, int height)
    {
        for (int i = 0; i < uniqueResolutions.Count; i++)
        {
            if (uniqueResolutions[i].x == width && uniqueResolutions[i].y == height)
                return i;
        }
        return -1;
    }

    public void ApplyResolution(int dropdownIndex)
    {
        if (dropdownIndex < 0 || dropdownIndex >= uniqueResolutions.Count)
            return;

        Vector2Int selectedSize = uniqueResolutions[dropdownIndex];
        int refreshRate = Screen.currentResolution.refreshRate;

        Screen.SetResolution(selectedSize.x, selectedSize.y, Screen.fullScreen, refreshRate);

        AppSettings.width = selectedSize.x;
        AppSettings.height = selectedSize.y;
        AppSettings.fullScreen = Screen.fullScreen;


        PlayerPrefs.SetInt("ScreenWidth", selectedSize.x);
        PlayerPrefs.SetInt("ScreenHeight", selectedSize.y);
        PlayerPrefs.SetInt("FullScreen", Screen.fullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }


    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        AppSettings.fullScreen = isFullscreen;

        PlayerPrefs.SetInt("FullScreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
        AppSettings.SaveSettings();
    }

    public void ToggleFPS(bool isFPSDisplay)
    {
        AppSettings.showFPS = isFPSDisplay;

        PlayerPrefs.SetInt("showFPS", isFPSDisplay ? 1 : 0);
        PlayerPrefs.Save();
        AppSettings.SaveSettings(); 
    }

}
