using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    public GameObject playingUI;

    public VisualElement ui;

    public Button playButton;
    public Button optionsButton;
    public Button quitButton;


    private void OnEnable()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        playButton = ui.Q<Button>("PlayButton");
        playButton.clicked += OnPlayButtonClicked;

        optionsButton = ui.Q<Button>("OptionsButton");
        optionsButton.clicked += OnOptionsButtonClicked;

        quitButton = ui.Q<Button>("QuitButton");
        quitButton.clicked += OnQuitButtonClicked;
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options!");
    }

    private void OnPlayButtonClicked()
    {
        playingUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
