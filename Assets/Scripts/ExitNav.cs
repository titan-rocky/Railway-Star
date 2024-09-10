using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitNav : MonoBehaviour
{
    void Start() {

    }

    void Update() {

    }

    public void Click() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
