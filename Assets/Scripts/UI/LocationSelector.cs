using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelector : MonoBehaviour
{
    public static string selectedLocation; // Static variable to store the selected location

    // This function will be called when the "Kitchen" button is pressed
    public void SelectKitchen()
    {
        selectedLocation = "Kitchen";
        SceneManager.LoadScene("HomeScene");
    }

    // This function will be called when the "Bedroom" button is pressed
    public void SelectBedroom()
    {
        selectedLocation = "Bedroom";
        SceneManager.LoadScene("HomeScene");
    }
}
