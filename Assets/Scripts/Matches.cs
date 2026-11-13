using UnityEngine;

public class Matches : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Inventory inventory))
        {
            inventory.MatchPickup();
            Destroy(gameObject);
        }
    }
}
