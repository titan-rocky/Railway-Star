using UnityEngine;
using UnityEngine.AI;
using Vuforia;

public class ARNav : MonoBehaviour
{
    public Camera arCamera;
    public GameObject destinationObject;
    public LineRenderer lineRenderer;
    public float updateInterval = 0.1f;
    public float lineHeight = 0.1f; // Height of the line above the ground
    public float lineWidth = 0.05f; // Width of the line

    private NavMeshPath path;
    private float elapsedTime;
    private AreaTargetBehaviour areaTarget;
    private bool isLocalized = false;
    private bool lineDefined = true;

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineDefined = false;
        }

        SetupLineRenderer();
        path = new NavMeshPath();

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
    }

    void SetupLineRenderer()
    {
        if (!lineDefined){
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.positionCount = 0;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.red;
            //lineRenderer.alignment = LineAlignment.TransformZ; // Center the line
        }
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        isLocalized = (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED);
        if (isLocalized) {
            Debug.Log("Localized");
        }
    }

    void Update()
    {
        if (!isLocalized)
        {
            lineRenderer.positionCount = 0; // Clear the line when not localized
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= updateInterval)
        {
            elapsedTime = 0f;
            UpdatePath();
        }
    }

    void UpdatePath()
    {
        if (arCamera == null || destinationObject == null || areaTarget == null)
            return;

        Vector3 sourcePosition = arCamera.transform.position;
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
                // Elevate the path points
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

    void OnDisable()
    {
        if (areaTarget != null)
        {
            areaTarget.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}
