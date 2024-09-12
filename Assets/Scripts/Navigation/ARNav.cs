using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Vuforia;

public class ARNav : MonoBehaviour
{
    public Camera arCamera;
    public GameObject kitchenDestination; // Assign this in the Inspector
    public GameObject bedroomDestination; // Assign this in the Inspector
    public GameObject destinationObject;
    public LineRenderer lineRenderer;
    public float updateInterval = 0.1f;
    public float lineHeight = 0.1f;
    public float lineWidth = 0.05f;
    public GameObject agentObject;
    public TMP_Text label;

    private NavMeshAgent agent;
    private NavMeshPath path;
    private float elapsedTime;
    private AreaTargetBehaviour areaTarget;
    private bool isLocalized = false;
    private bool lineDefined = true;
    private AreaTargetBehaviour nearestAreaTarget;

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
        if (LocationSelector.selectedLocation == "Cisco")
        {
            destinationObject.transform.position = kitchenDestination.transform.position;
        }
        else if (LocationSelector.selectedLocation == "RestRoom")
        {
            destinationObject.transform.position = bedroomDestination.transform.position;
        }
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        isLocalized = (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED);
        if (isLocalized)
        {
            Debug.Log("Localized");
        }
    }

    void Update()
    {
        //FindNearestAreaTarget();
        //areaTarget = nearestAreaTarget;
        if (!isLocalized)
        {
            lineRenderer.positionCount = 0; // Clear the line when not localized
            return;
        }

        agent.Warp(new Vector3(arCamera.transform.position.x, agent.gameObject.transform.position.y, arCamera.transform.position.z));

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= updateInterval)
        {
            elapsedTime = 0f;
            UpdatePath();
        }
        
        //if (label) label.text="Target: "+nearestAreaTarget.TargetName;
    }

    void UpdatePath()
    {
        if (arCamera == null || destinationObject == null || areaTarget == null)
            return;

        Vector3 sourcePosition = agentObject.transform.position;
        Vector3 destinationPosition = destinationObject.transform.position;

        // Project source and destination positions onto the NavMesh
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

    void FindNearestAreaTarget() {
        AreaTargetBehaviour[] areaTargets = FindObjectsOfType<AreaTargetBehaviour>();
        float closestDistance = Mathf.Infinity;
        foreach (AreaTargetBehaviour areaTarget in areaTargets)
        {
            float distance = Vector3.Distance(agent.transform.position, areaTarget.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestAreaTarget = areaTarget;
            }
        }
        if (nearestAreaTarget != null)
        {
            Debug.Log("Nearest Area Target: " + nearestAreaTarget.name);
            // Now you can localize to the nearest target
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
