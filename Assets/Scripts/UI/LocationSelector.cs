using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelector : MonoBehaviour
{
    string Scenename = "ClgScene";
    public static string selectedLocation; // Static variable to store the selected location

    // This function will be called when the "Kitchen" button is pressed
    public void SelectECE()
    {
        selectedLocation = "ECE";
        SceneManager.LoadScene(Scenename);
    }

    // This function will be called when the "Bedroom" button is pressed
    public void SelectMech()
    {
        selectedLocation = "Mech";
        SceneManager.LoadScene(Scenename);
    }
    public void SelectEEE()
    {
        selectedLocation = "EEE";
        SceneManager.LoadScene(Scenename);
    }
    public void SelectMCT()
    {
        selectedLocation = "MCT";
        SceneManager.LoadScene(Scenename);
    }
}
