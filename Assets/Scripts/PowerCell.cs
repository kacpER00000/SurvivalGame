using UnityEngine;

public class PowerCell : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float amplitude = 0.5f;
    private float angleInDegrees = 0f;
    private bool decrementing = false;
    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(angleInDegrees <= 0)
        {
            decrementing = false;
        }
        if(angleInDegrees >= 90)
        {
            decrementing = true;
        }
        angleInDegrees = decrementing ? --angleInDegrees : ++angleInDegrees;
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        float sineValue = Mathf.Sin(angleInRadians); 

        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.World);
        transform.position = new Vector3(transform.position.x, startY + sineValue * amplitude, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Inventory inventory))
        {
            inventory.CellPickup();
            Destroy(gameObject);
        }
    }
}
