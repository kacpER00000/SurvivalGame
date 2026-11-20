using Unity.VisualScripting;
using UnityEngine;

public class TidyObj : MonoBehaviour
{
    [SerializeField] private float removeTime = 3.0f;
    void Start()
    {
        Destroy(gameObject, removeTime);
    }
}
