using UnityEngine;

public class DestinationSelector : MonoBehaviour
{
    public Transform kioskLocation;  // Reference to the kiosk location (fixed point)
    public LineRenderer lineRenderer;  // LineRenderer component to draw the path

    void OnMouseDown()
    {
        // When the destination is clicked, draw the line from the kiosk location to the destination
        DrawPathToDestination(transform.position);
    }

    void DrawPathToDestination(Vector3 destinationPosition)
    {
        // Set the LineRenderer points to draw the line
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, kioskLocation.position);  // Start at the kiosk
        lineRenderer.SetPosition(1, destinationPosition);     // End at the destination
    }
}
