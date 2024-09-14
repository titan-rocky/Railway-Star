using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestGen : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> destinations;
    public Canvas markerCanvas;
    [SerializeField]
    public Vector3 offset = new Vector3(0,1,0);
    public bool TourMode = false;

    private GameObject selDestination;

    void Start()
    {
        if (markerCanvas == null)
        {
            markerCanvas = GetComponent<Canvas>(); // Automatically get the canvas component if not assigned
        }
        if (markerCanvas.renderMode != RenderMode.WorldSpace)
        {
            Debug.LogWarning("Canvas render mode should be set to WorldSpace for this script to work.");
        }
        if (destinations.Count == 0)
        {
            Debug.LogError("No Destinations Available. Set atleast one");
        }

        selDestination = destinations[0];

        if (!TourMode) DisableOthers(selDestination);

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void DisableOthers(GameObject dest) {
        foreach(GameObject otherdest in destinations){
            if (dest != otherdest){
                otherdest.SetActive(false);
            }
        }
    }

    public GameObject GetDestination() {
        return selDestination;
    }
}
