using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelector : MonoBehaviour
{
    public static string selectedLocation;
    
    string Scenename = "ClgScene";

    public void SelectECE()
    {
        selectedLocation = "ECE";
        SceneManager.LoadScene(Scenename);
    }
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
