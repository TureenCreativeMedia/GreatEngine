using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [Header("Gamemode Only")]
    public string Gamemode;

    [Header("Button Altering")]
    public string sceneToGo = "Game";
    [SerializeField] private UnityEvent ue_Click;
    [SerializeField] private UnityEvent ue_Hover;
    [SerializeField] private UnityEvent ue_Unhover;

    [SerializeField] public Slider loadingBar;
    private AsyncOperation operation;
    private float timer;
    private float pre_loadingTime;
    private bool canLoad;

    [SerializeField] private bool thisDoesLoading;

    void Awake()
    {
        if (!thisDoesLoading) return;

        canLoad = false;

        if (loadingBar == null)
        {
            GameObject sliderObj = SceneGameObjectReferences.Instance.loadingBar;
            if (sliderObj != null)
            {
                loadingBar = sliderObj.GetComponent<Slider>();
            }
            else
            {
                Debug.Log("Cannot find sliderObj (Slider) on loadingBar, SceneGameObjectReferences.loadingBar may not be set correctly");
            }
        }
    }


    void Update()
    {
        if (operation != null) timer += Time.deltaTime;

        if (operation != null)
        {
            Debug.Log($"{operation.progress * 100}%");
            if (loadingBar != null)
            {
                SceneGameObjectReferences.Instance.loadingScreen?.SetActive(true);
                loadingBar.transform.gameObject.SetActive(true);
                loadingBar.value = operation.progress * 100; // 0 to 1
            }
        }
    }

    public void TravelScene()
    {
        timer = 0;
        pre_loadingTime = 0.5f;
        canLoad = true;
        operation = SceneManager.LoadSceneAsync(sceneToGo);
    }

    public void SetCONSTANTSGamemode()
    {
        CONSTANTS.GAME_TYPE = Gamemode;
    }

    public void Hover() => ue_Hover.Invoke();
    public void Unhover() => ue_Unhover.Invoke();
    public void Click() => ue_Click.Invoke();
    public void Quit() => UnityEngine.Application.Quit();
}
