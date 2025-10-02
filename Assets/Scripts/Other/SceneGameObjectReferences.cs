using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGameObjectReferences : MonoBehaviour
{
    public static SceneGameObjectReferences Instance;
    [SerializeField] public GameObject loadingBar;
    [SerializeField] public GameObject loadingScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
