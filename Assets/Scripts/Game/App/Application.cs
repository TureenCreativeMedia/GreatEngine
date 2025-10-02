using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using System.IO;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.InputSystem.Interactions;
using UnityEditor;
using System.Collections.Generic;
using Unity.VisualScripting;

public static class BuildConfig
{
    public enum AppVersionGETFROM
    {
        AppVersionString,
        Unity
    }

    public static string AppVersion = "1.3.0";
    public enum DEVELOPMENTTEXT 
    { 
        None,
        EditorOnly,
        DevelopmentBuildOnly,
        Both
    }
}

[System.Serializable]
public static class AppSettings
{
    // Build config enabler
        public static BuildConfig.DEVELOPMENTTEXT DevelopmentBuildText = BuildConfig.DEVELOPMENTTEXT.DevelopmentBuildOnly;
        public static BuildConfig.AppVersionGETFROM AppVersionGETFROM = BuildConfig.AppVersionGETFROM.Unity;

        public static string appVersion;


    // End

    public static PostProcessLayer PostProcessLayer;
    public static bool fullScreen;
    public static Resolution screenRes;

    public static float gameVolume = 0.7f;
    public static bool antiAlias = false;
    public static int width = 1920;
    public static int height = 1080;
    public static int hz = 60;
    public static bool showFPS = false;

    public static Vector2Int ScreenAspectRatio => AspectReturner.GetSimplifiedAspectRatio(width, height);
    public static void LoadSettings()
    {
        // Check if we're parsing the app version from Unity or from a custom
        if (AppVersionGETFROM == BuildConfig.AppVersionGETFROM.Unity)
        {
            appVersion = UnityEngine.Application.version;
        }
        else
        {
            appVersion = BuildConfig.AppVersion;
        }

            Resolution defaultRes = Screen.currentResolution;

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