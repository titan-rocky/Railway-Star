using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelector : MonoBehaviour
{
    public static string selectedLocation = "Mech";
    
    string Scenename = "ClgScene";

    public void SelectECE()
    {
        selectedLocation = "ECE";
        
    }
    public void SelectMech()
    {
        selectedLocation = "Mech";
        
    }
    public void SelectEEE()
    {
        selectedLocation = "EEE";
        
    }
    public void SelectMCT()
    {
        selectedLocation = "MCT";
        
    }
    public void OpenNavScene()
    {
        SceneManager.LoadScene(Scenename);
    }
    public void OnDropdownValueChanged(Int32 value)
    {
        if(value == 0) selectedLocation = "Mech";
        else if (value == 1) selectedLocation = "MCT";
        else if(value == 2) selectedLocation = "EEE";
        else if(value == 3) selectedLocation = "ECE";
        Debug.Log(value);
    }
}
