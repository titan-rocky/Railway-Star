using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitNav : MonoBehaviour
{
    public GameObject navigationObject;

    private string uiManagerScene = "UI Manager";

    /// Event Handler - Triggered when the Menu button is clicked (stop navigation and load UI Manager)
    public void MenuButtonClicked()
    {
        if (navigationObject != null)
        {
            Destroy(navigationObject);        }

        SceneManager.LoadScene(uiManagerScene);
    }

    /// Even Handler - Triggered when the Exit button is clicked (close the app)
    public void ExitButtonClicked()
    {
        // Close the application
        Application.Quit();

        // In the editor, this will stop play mode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
