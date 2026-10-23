using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player"))
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
