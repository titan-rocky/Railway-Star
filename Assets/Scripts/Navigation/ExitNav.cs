using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitNav : MonoBehaviour
{
    // Reference to the navigation object (if needed)
    public GameObject navigationObject;

    // UIManager Scene name
    private string uiManagerScene = "UI Manager";

    // Function to call when the Menu button is clicked (stop navigation and load UI Manager)
    public void MenuButtonClicked()
    {
        // Stop the navigation (destroy or stop the navigation object if applicable)
        if (navigationObject != null)
        {
            Destroy(navigationObject);  // Optionally destroy the navigation object or stop the logic
        }

        // Load the UI Manager scene
        SceneManager.LoadScene(uiManagerScene);
    }

    // Function to call when the Exit button is clicked (close the app)
    public void ExitButtonClicked()
    {
        // Close the application
        Application.Quit();

        // If you're testing in the editor, this will stop play mode
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
