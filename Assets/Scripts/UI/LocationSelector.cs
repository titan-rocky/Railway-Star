using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelector : MonoBehaviour
{
    string Scenename = "ClgScene";
    public static string selectedLocation; // Static variable to store the selected location

    // This function will be called when the "Kitchen" button is pressed
    public void SelectCisco()
    {
        selectedLocation = "Cisco";
        SceneManager.LoadScene(Scenename);
    }

    // This function will be called when the "Bedroom" button is pressed
    public void SelectRestroom()
    {
        selectedLocation = "Lab";
        SceneManager.LoadScene(Scenename);
    }
}
