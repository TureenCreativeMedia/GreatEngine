using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] Slider bar;
    [SerializeField] TMP_Text percentage;

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        percentage.text = $"{Mathf.Round(bar.value)}%";
    }
}
