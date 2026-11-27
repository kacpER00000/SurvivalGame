using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TargetCollision : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip resetSound;
    [SerializeField] private float resetTime = 3f;


    private bool beenHit;
    private Animator animator;
    private AudioSource audioSource;
    private CoconutWin game;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
        game = GetComponentInParent<CoconutWin>();
    
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!beenHit && collision.gameObject.name == "coconut")
        {
            StartCoroutine(TargetHit());
        }
    }

    private IEnumerator TargetHit()
    {
        beenHit = true;
        audioSource.PlayOneShot(hitSound);
        animator.SetBool("Down", true);
        game.TargetDown();

        yield return new WaitForSeconds(resetTime);

        audioSource.PlayOneShot(resetSound);
        animator.SetBool("Down", false);
        game.TargetUp();
        beenHit = false;
    }
}
