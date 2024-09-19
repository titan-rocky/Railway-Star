using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class DestinationTrigger : MonoBehaviour
{
    public TMP_Text label; 
 
    public GameObject destinationObject;
    public LineRenderer lineRenderer;       
    private bool isReached =false;
    private NavMeshAgent agent;

    void Update()
    {
       
        float distance = Vector3.Distance(agent.transform.position, destinationObject.transform.position); 
        DisplayDistanceToUser(distance);
    }
    void OnTriggerEnter(Collider other)
    {
        isReached = true;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Destination Reached!");
            DisplayDestinationReachedMessage();

            StopLineRenderer();
        }
    }

    void DisplayDestinationReachedMessage()
    {
        if (label == null) return;
        
        if(isReached)
        {
         label.text = "REACHED";
         label.color = Color.green;
        }
        else
        {

        }
        
    }
     void DisplayDistanceToUser(float distance)
{
    label.text = "Distance to destination: " + distance.ToString("F2") + " meters";
}

    void StopLineRenderer()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }
}
