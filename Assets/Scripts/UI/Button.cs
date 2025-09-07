using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public string sceneToGo = "Game";
    [SerializeField] private UnityEvent ue_Click;

    [SerializeField] private UnityEvent ue_Hover;
    [SerializeField] private UnityEvent ue_Unhover;

    [SerializeField] Slider loadingBar;
    AsyncOperation operation;
    float timer;
    float pre_loadingTime;
    bool canLoad;

    [SerializeField] private bool thisDoesLoading;

    void Start()
    {
        if (!thisDoesLoading) return;

        canLoad = false;
        if (loadingBar == null)
            try { loadingBar = GameObject.Find("Loading Slider").GetComponent<Slider>(); }
            catch { Debug.LogError($"Could not find {loadingBar} in hierarchy"); }
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (operation != null)
        {
            Debug.Log($"{operation.progress * 100}%");
            if (loadingBar != null) loadingBar.value = operation.progress * 100;
        }

        if (timer > pre_loadingTime && canLoad)
        {
            operation = SceneManager.LoadSceneAsync(sceneToGo);
            canLoad = false;
        }
    }

    public void TravelScene()
    {
        timer = 0;
        pre_loadingTime = .5f;
        canLoad = true;
    }

    public void Hover()
    {
        ue_Hover.Invoke();
    }

    public void Unhover()
    {
        ue_Unhover.Invoke();
    }

    public void Click()
    {
        ue_Click.Invoke();
    }

    public void Quit()
    {
        UnityEngine.Application.Quit();
    }
}
