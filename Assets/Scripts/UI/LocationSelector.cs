using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelector : MonoBehaviour
{
    public static string selectedLocation; // Static variable to store the selected location

    // This function will be called when the "Kitchen" button is pressed
    public void SelectKitchen()
    {
        selectedLocation = "Cisco";
        SceneManager.LoadScene("ClgScene");
    }

    // This function will be called when the "Bedroom" button is pressed
    public void SelectBedroom()
    {
        selectedLocation = "Lab";
        SceneManager.LoadScene("ClgScene");
    }
}
