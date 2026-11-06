using Unity.VisualScripting;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private int cellsRequired = 4;
    [SerializeField] private AudioClip lockedSound;
    [SerializeField] private Light doorLight;
    [SerializeField] private TextHints textHints;
    [SerializeField, TextArea] private string noCellsHint = "The door is locked and the generator is dead. Maybe there are power cells somewhere on the island?";
    [SerializeField, TextArea] private string moreCellsHint = "The generator needs more power cells to open the door.";
    [SerializeField] private float blinkingSpeed = 0.5f;
    private float blinkTimer = 0f;
    private bool lightOn = true;
    private bool isBlinking = true;

    void Start()
    {
        blinkTimer = blinkingSpeed;
    }

    void Update()
    {
        if(!isBlinking){return;}
        blinkTimer -= Time.deltaTime;

        if (blinkTimer <= 0f)
        {
            lightOn = !lightOn;
            doorLight.enabled = lightOn;
            
            blinkTimer = blinkingSpeed;
        }
    }


    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player") || !other.TryGetComponent(out Inventory inventory))
        {
            return;
        }

        Transform door = transform.Find("door");
        DoorManager doorManager = door.GetComponent<DoorManager>();

        if(inventory.Charge >= cellsRequired)
        {
            doorManager.DoorCheck();
            doorManager.TriggerEnter();
            inventory.HudOff();
            doorLight.color = Color.green;
            isBlinking = false;
        } else
        {
            door.GetComponent<AudioSource>().PlayOneShot(lockedSound);
            inventory.HudOn();
            textHints.ShowHint(inventory.Charge == 0 ? noCellsHint : moreCellsHint);
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
