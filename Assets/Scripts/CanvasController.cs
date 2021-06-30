using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text pauseText;
    private void OnEnable()
    {
        UIEventHandler.PauseTextClicked.AddListener(PauseTextClicked);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        UIEventHandler.PauseTextClicked.RemoveListener(PauseTextClicked);
    }
    private void PauseTextClicked(float time)
    {
        if (time == 0)
            pauseText.enabled = true;
        else
            pauseText.enabled = false;
    }
}
