using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;

public class pathFinder : MonoBehaviour
{
    public Camera ARCam;
    public LineRenderer lineRenderer;
    public GameObject destination;
    public GameObject agentObject;
    public GameObject areaTargetParent;
    public float updateInterval = 0.1F;
    public float lineHeight = 0.1F;
    public float lineWidth = 0.05f;

    private NavMeshPath path;
    private NavMeshAgent agent;
    private float elapsedTime;
    private AreaTargetBehaviour nearestAreaTarget;


    void Start()
    {
        if (lineRenderer == null){
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        agent = agentObject.GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        
        SetProperties();
    }

    void Update()
    {
        // Update the line every updateInterval seconds
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= updateInterval)
        {
            elapsedTime = 0f;
            UpdatePath();
        }
        // Wraps the x and z of navMeshAgent to the camera
        agent.Warp(new Vector3(ARCam.transform.position.x, agent.gameObject.transform.position.y, ARCam.transform.position.z));
        //FindNearestAreaTarget();
    }

    private void UpdatePath() {
        Vector3 sourcePosition = agentObject.transform.position;
        Vector3 destinationPosition = destination.transform.position;

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

    private void SetProperties() {
        lineRenderer.positionCount = 10;
        lineRenderer.numCapVertices = 10;
        lineRenderer.numCornerVertices = 10;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color(0.63F, 0.28F, 0.28F, 1F);
        lineRenderer.endColor = new Color(0.63F, 0.28F, 0.28F, 1F);
    }

    private void FindNearestAreaTarget() {
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

    private void FindEuclideanDistance() {
    }

}
