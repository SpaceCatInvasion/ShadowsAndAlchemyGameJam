using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayingUIController : MonoBehaviour
{
    public GameObject menuUI;

    public VisualElement ui;

    public Button pauseButton;

    private void OnEnable()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        pauseButton = ui.Q<Button>("PauseButton");
        pauseButton.clicked += OnPauseButtonClicked;
    }
    private void OnPauseButtonClicked()
    {
        menuUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
