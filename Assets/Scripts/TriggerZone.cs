using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private int cellsRequired = 4;
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && other.TryGetComponent(out Inventory inventory) && inventory.Charge >= cellsRequired)
        {
            transform.Find("door").GetComponent<DoorManager>().DoorCheck();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.Find("door").GetComponent<DoorManager>().TriggerLeave();
        }
    }
}
