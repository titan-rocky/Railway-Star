using UnityEngine;

public class DestinationTrigger : MonoBehaviour
{
    public ARNav arNav;  // Reference to the ARNav script

    // This method will detect when the NavMeshAgent (Player) reaches the destination
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the NavMeshAgent (tagged as "Player")
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided");
            // Call the method in ARNav to handle reaching the destination
            arNav.OnDestinationReached();
        }
    }
}
