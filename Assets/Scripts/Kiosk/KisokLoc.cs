using UnityEngine;
using UnityEngine.AI;

public class KioskLoc : MonoBehaviour
{
    public GameObject destinationMarker;  // The destination marker GameObject
    public Transform kioskLocation;       // The kiosk location (fixed point)
    public GameObject[] locations;        // Array of empty GameObjects representing locations

    private LineRenderer lineRenderer;    // LineRenderer for drawing the line
    private NavMeshPath navMeshPath;      // NavMeshPath for calculating the shortest path

    // Expose these fields to the Inspector for customization
    [Header("Line Renderer Settings")]
    public float lineStartWidth = 0.1f;
    public float lineEndWidth = 0.1f;
    public Color lineStartColor = Color.green;
    public Color lineEndColor = Color.green;

    // Optional height adjustment if needed
    public float lineHeightOffset = 0.05f;  // Small offset to avoid z-fighting issues

    // This determines how much to offset the line from the walls and corners
    public float pathCenteringOffset = 0.5f;  // Offset to move line to the middle of the path

    void Start()
    {
        // Create a LineRenderer at runtime
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Configure the LineRenderer properties using the Inspector values
        lineRenderer.startWidth = lineStartWidth;
        lineRenderer.endWidth = lineEndWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineStartColor;
        lineRenderer.endColor = lineEndColor;
        lineRenderer.positionCount = 0;  // Initially, no line is drawn

        // Create a new NavMeshPath object for path calculations
        navMeshPath = new NavMeshPath();
    }

    // This function is triggered when a button is clicked
    public void MoveDestinationToLocation(int locationIndex)
    {
        if (locationIndex >= 0 && locationIndex < locations.Length)
        {
            // Move the destination marker to the selected location
            destinationMarker.transform.position = locations[locationIndex].transform.position;

            // Calculate and draw the shortest path from the kiosk location to the destination marker
            DrawCenteredPath(destinationMarker.transform.position);
        }
    }

    void DrawCenteredPath(Vector3 destinationPosition)
    {
        // Calculate the shortest path using NavMesh
        if (NavMesh.CalculatePath(kioskLocation.position, destinationPosition, NavMesh.AllAreas, navMeshPath))
        {
            // Check if the path is valid and complete
            if (navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                // Set the number of positions for the LineRenderer based on the number of corners in the path
                lineRenderer.positionCount = navMeshPath.corners.Length;

                // Set the positions of the LineRenderer to follow the calculated path, but centered away from walls
                for (int i = 0; i < navMeshPath.corners.Length; i++)
                {
                    // Get each corner and adjust it to be centered in the walkable path
                    Vector3 corner = navMeshPath.corners[i];

                    // Sample the NavMesh around the corner to get a point that's centered in the path
                    Vector3 adjustedPosition = GetCenteredPosition(corner);

                    // Optionally, add a small height offset to avoid z-fighting issues with the ground
                    adjustedPosition.y += lineHeightOffset;

                    // Set the adjusted position to the LineRenderer
                    lineRenderer.SetPosition(i, adjustedPosition);
                }
            }
            else
            {
                Debug.LogWarning("Path is not complete.");
                lineRenderer.positionCount = 0;  // Clear the line if the path is incomplete
            }
        }
        else
        {
            Debug.LogWarning("Unable to calculate path.");
            lineRenderer.positionCount = 0;  // Clear the line if path calculation fails
        }
    }

    // Function to center the path based on the surrounding NavMesh
    Vector3 GetCenteredPosition(Vector3 position)
    {
        NavMeshHit hit;
        
        // Use NavMesh.SamplePosition to get the closest point on the NavMesh to ensure it's valid
        if (NavMesh.SamplePosition(position, out hit, pathCenteringOffset, NavMesh.AllAreas))
        {
            // Perform a raycast to detect walls or obstacles and adjust the position accordingly
            Vector3 directionToCenter = GetPathCenterDirection(position);
            Vector3 centeredPosition = position + directionToCenter.normalized * pathCenteringOffset;
            
            // Ensure the new position is valid and on the NavMesh
            if (NavMesh.SamplePosition(centeredPosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return position;  // Return the original position if we can't adjust it
    }

    // Function to determine the direction to the center of the path
    Vector3 GetPathCenterDirection(Vector3 position)
    {
        // This method can be customized to calculate the direction toward the center of the path
        // For now, we assume a basic direction adjustment, but you can add more sophisticated checks
        return Vector3.forward + Vector3.right;  // Adjust this to fit your specific NavMesh layout
    }
}
