    using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Vuforia;
using System.Collections.Generic;

public class ARNav : MonoBehaviour
{
    public Camera arCamera;
    public List<GameObject> destinations;
    public GameObject destinationObject;
    public LineRenderer lineRenderer;
    public TMP_Text distanceText;  // TMP_Text for displaying distance
    public TMP_Text destinationReachedText;  // TMP_Text for "Destination Reached" message
    public TMP_Text ETAText;
    public float updateInterval = 0.1f;
    public float lineHeight = 0.1f;
    public float lineWidth = 0.05f;
    public GameObject agentObject;
    private NavMeshAgent agent;
    private NavMeshPath path;
    private float elapsedTime;
    private AreaTargetBehaviour areaTarget;
    private bool firstTime = true;
    private bool isLocalized = false;
    private bool lineDefined = true;
    private bool destinationReached = false;
    private float distanceThreshold = 2.0f;  // Set distance threshold for destination reach

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineDefined = false;
        }

        SetupLineRenderer();
        path = new NavMeshPath();
        agent = agentObject.GetComponent<NavMeshAgent>();

        // Initialize UI Texts
        destinationReachedText.text = "";  // Clear destination reached message at start
        distanceText.text = "";  // Clear distance text at start

        // Find the AreaTargetBehaviour in the scene
        areaTarget = FindObjectOfType<AreaTargetBehaviour>();
        if (areaTarget == null)
        {
            Debug.LogError("No AreaTargetBehaviour found in the scene!");
        }
        else
        {
            // Subscribe to the OnTargetStatusChanged event
            areaTarget.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        if (destinations.Count == 0)
        {
            Debug.LogError("No Destinations Available. Set at least one");
        }

        // Set the destination based on the selected location
        SetDestinationBasedOnLocation();
    }

    void SetupLineRenderer()
    {
        if (!lineDefined)
        {
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.positionCount = 0;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.red;
        }
    }

    void SetDestinationBasedOnLocation()
    {
        if (LocationSelector.selectedLocation == "ECE")
        {
            destinationObject.transform.position = destinations[0].transform.position;
        }
        else if (LocationSelector.selectedLocation == "Mech")
        {
            destinationObject.transform.position = destinations[1].transform.position;
        }
        else if (LocationSelector.selectedLocation == "EEE")
        {
            destinationObject.transform.position = destinations[2].transform.position;
        }
        else if (LocationSelector.selectedLocation == "MCT")
        {
            destinationObject.transform.position = destinations[3].transform.position;
        }
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        isLocalized = (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED || status.Status == Status.LIMITED);
        
        if (isLocalized)
        {
            firstTime = firstTime && false;
            Debug.Log("Localized");
        }
    }

    void Update()
    {
        if (firstTime || destinationReached)
        {
            lineRenderer.positionCount = 0;  // Clear the line when not localized or destination reached
            return;
        }
         Vector3 cameraPositionXZ = new Vector3(arCamera.transform.position.x, agent.transform.position.y, arCamera.transform.position.z);
         NavMeshHit hit;
        if (NavMesh.SamplePosition(cameraPositionXZ, out hit, 1.0f, NavMesh.AllAreas))
        {
            // Warp the agent to match the ARCamera's x and z while ensuring it's on the NavMesh
            agent.Warp(new Vector3(hit.position.x, hit.position.y, hit.position.z));
        }
        // Update distance and direction
        UpdateDistanceAndDirection();

         Vector3 directionToCamera = arCamera.transform.position - destinationObject.transform.position;

        directionToCamera.y = 0;

        if (directionToCamera != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
            destinationObject.transform.rotation = targetRotation;
        }
        
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= updateInterval)
        {
            elapsedTime = 0f;
            UpdatePath();
        }

        // Check if the distance to the destination is less than or equal to the threshold
        float distanceToDestination = Vector3.Distance(agent.transform.position, destinationObject.transform.position);
        if (distanceToDestination <= distanceThreshold)
        {
            OnDestinationReached();  // Trigger destination reached logic
        }
    }

    void UpdateDistanceAndDirection()
    {
        // Get the direction from the marker to the camera and make it face the camera
        Vector3 directionToCamera = arCamera.transform.position - destinationObject.transform.position;
        directionToCamera.y = 0;  // Prevent rotation on X and Z axes

        if (directionToCamera != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
            destinationObject.transform.rotation = targetRotation;
        }

        // Calculate and display distance
        float distanceToDestination = Vector3.Distance(agent.transform.position, destinationObject.transform.position);
        distanceText.text = "Distance: " + Mathf.RoundToInt(distanceToDestination) + " meters";

        DisplayETA(distanceToDestination, agent.speed);
    }

    void UpdatePath()
    {
        if (arCamera == null || destinationObject == null || areaTarget == null)
            return;

        Vector3 sourcePosition = agent.transform.position;
        Vector3 destinationPosition = destinationObject.transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(sourcePosition, out hit, 1.0f, NavMesh.AllAreas))
            sourcePosition = hit.position;
        if (NavMesh.SamplePosition(destinationPosition, out hit, 1.0f, NavMesh.AllAreas))
            destinationPosition = hit.position;

        if (NavMesh.CalculatePath(sourcePosition, destinationPosition, NavMesh.AllAreas, path))
        {
            Vector3[] elevatedCorners = new Vector3[path.corners.Length];
            for (int i = 0; i < path.corners.Length; i++)
            {
                elevatedCorners[i] = path.corners[i] + Vector3.up * lineHeight;
            }

            lineRenderer.positionCount = elevatedCorners.Length;
            lineRenderer.SetPositions(elevatedCorners);
        }
        else
        {
            Debug.LogWarning("Unable to calculate path!");
            lineRenderer.positionCount = 0;
        }
    }

    // Called when the agent reaches the destination
    public void OnDestinationReached()
    {
        destinationReached = true;  // Stop further updates

        // Display "Destination Reached" message
        distanceText.text = "Destination Reached!";
        distanceText.color = Color.green;
       

        // Stop the LineRenderer
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
        }

        Debug.Log("Destination Reached!");
    }

    void DisplayETA(float distance, float speed)
{
    if (speed > 0)
    {
        // Calculate ETA in seconds
        float etaInSeconds = distance / speed;

        // Convert ETA to minutes and seconds
        int minutes = Mathf.FloorToInt(etaInSeconds / 60);
        int seconds = Mathf.FloorToInt(etaInSeconds % 60);

        // Display the ETA
        ETAText.text += "\nETA: " + minutes + " min " + seconds + " sec";
    }
    else
    {
        // If speed is 0, show no ETA
        ETAText.text += "\nETA: N/A";
    }
}
    void OnDisable()
    {
        if (areaTarget != null)
        {
            areaTarget.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}
