using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;

    private float nextPlayTime = 0f;
    private const float soundCooldown = 1f;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject door = hit.gameObject;
        Animator outpostAnimator = door.GetComponentInParent<Animator>(); 
        AudioSource audioSource = door.GetComponent<AudioSource>();
        if(door.CompareTag("playerDoor"))
        {
            if(!outpostAnimator.GetBool("DoorOpen")){
                OpenDoor(door, audioSource);
            }
            else
            {
                CloseDoor(door, audioSource);
            }
        }
    }

    private void OpenDoor(GameObject door, AudioSource audioSource)
    {
        audioSource.PlayOneShot(doorOpenSound);
        door.GetComponentInParent<Animator>().SetBool("DoorOpen",true);
    }

    private void CloseDoor(GameObject door, AudioSource audioSource)
    {
        if(Time.time >= nextPlayTime)
        {
            audioSource.PlayOneShot(doorCloseSound);
            nextPlayTime = Time.time + soundCooldown;
        }
    }
}
