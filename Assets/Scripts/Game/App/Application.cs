using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public static class AppSettings
{
    public static PostProcessLayer PostProcessLayer;
    public static bool fullScreen;
    public static Resolution screenRes;

    public static string appVersion;
    public static float gameVolume;

    public static bool antiAlias;
    public static int width;
    public static int height;
    public static int hz;
    public static bool showFPS;

    public static void LoadSettings()
    {
        Resolution defaultRes = Screen.currentResolution;

        appVersion = UnityEngine.Application.version;
        gameVolume = PlayerPrefs.GetFloat("Volume", 0.7f);
        width = PlayerPrefs.GetInt("ScreenWidth", defaultRes.width);
        height = PlayerPrefs.GetInt("ScreenHeight", defaultRes.height);
        showFPS = PlayerPrefs.GetInt("showFPS", 0) == 1;
        antiAlias = PlayerPrefs.GetInt("AntiAlias", 0) == 1;
        fullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;

        screenRes = new Resolution
        {
            width = width,
            height = height,
            refreshRate = defaultRes.refreshRate
        };

        PostProcessLayer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessLayer>();
        if (PostProcessLayer != null)
        {
            PostProcessLayer.antialiasingMode = PlayerPrefs.GetInt("AntiAlias") == 1 ? PostProcessLayer.Antialiasing.FastApproximateAntialiasing : PostProcessLayer.Antialiasing.None;
        }
    }


    public static void SaveSettings()
    {
        PlayerPrefs.SetInt("ScreenWidth", screenRes.width);
        PlayerPrefs.SetInt("ScreenHeight", screenRes.height);
        PlayerPrefs.SetInt("AntiAlias", antiAlias ? 1 : 0);
        PlayerPrefs.SetInt("FullScreen", fullScreen ? 1 : 0);
        PlayerPrefs.SetInt("showFPS", showFPS ? 1 : 0);
        PlayerPrefs.Save();
        LoadSettings();
    }

}

public class Application : MonoBehaviour
{
    [SerializeField] AudioMixer MasterAudio;
    KeyCode fullscreenBind = KeyCode.F11;

    public static Application instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        AppSettings.LoadSettings();
    }

    void Start()
    {

        if (!ResolutionEquals(Screen.currentResolution, AppSettings.screenRes))
        {
            Screen.SetResolution(
                AppSettings.screenRes.width,
                AppSettings.screenRes.height,
                AppSettings.fullScreen,
                AppSettings.screenRes.refreshRate
            );
        }

        Screen.fullScreen = AppSettings.fullScreen;
        
    }

    void Update()
    {
        
        MasterAudio.SetFloat("MasterVolume", AppSettings.gameVolume);
        AppSettings.fullScreen = Screen.fullScreen;
        AppSettings.screenRes = Screen.currentResolution;

        if (Input.GetKeyDown(fullscreenBind))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

    }

    bool ResolutionEquals(Resolution a, Resolution b)
    {
        return a.width == b.width && a.height == b.height && a.refreshRate == b.refreshRate;
    }
}
