using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorShutSound;
    [SerializeField] private float doorOpenTime = 3f;
    

    private AudioSource audioSource;
    private Animator animator;
    private bool doorIsOpen;
    private bool playerLeft = false;
    private float doorTimer;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (!doorIsOpen || !playerLeft)
        {
            return;
        }
        doorTimer += Time.deltaTime;
        if(doorTimer > doorOpenTime)
        {
            Door(false, doorShutSound);
            playerLeft = false;
        }

    }

    public void DoorCheck()
    {
        if (!doorIsOpen)
        {
            Door(true, doorOpenSound);
        }
    }

    public void TriggerLeave()
    {
        playerLeft = true;
    }

    private void Door(bool open, AudioClip audio)
    {
        doorIsOpen = open;
        doorTimer = 0f;
        audioSource.PlayOneShot(audio);
        animator.SetBool("DoorOpen",doorIsOpen);
    }
}
