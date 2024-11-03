using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using UnityEngine.UIElements; // For UI Toolkit

public class MenuBehaviour : MonoBehaviour
{
    [SerializeField] private UIDocument uiDoc;

    private VisualElement rootEl;

    private void OnEnable()
    {
        rootEl = uiDoc.rootVisualElement;

        // Find buttons by name
        Button singleplayerButton = rootEl.Q<Button>("Singleplayer");
        Button exitButton = rootEl.Q<Button>("Exit");

        // Assign click event handlers
        if (singleplayerButton != null)
        {
            singleplayerButton.clicked += () => LoadSingleplayerScene();
        }

        if (exitButton != null)
        {
            exitButton.clicked += () => ExitApplication();
        }
    }

    // Method to load the singleplayer scene
    private void LoadSingleplayerScene()
    {
        SceneManager.LoadScene("ProjectCurrentDemo"); // Replace with the actual scene name
    }

    // Method to exit the application
    private void ExitApplication()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#endif
    }
}
