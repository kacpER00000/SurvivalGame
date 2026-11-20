using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class CoconutThrower : MonoBehaviour
{
    [SerializeField] private Rigidbody coconutPrefab;
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private float throwSpeed = 30f;

    private InputAction attackAction;
    private AudioSource audioSource;

    public bool CanThrow { get; set; }


    void Awake()
    {
        attackAction = InputSystem.actions.FindAction("Player/Attack");
        audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if(CanThrow && attackAction.WasPressedThisFrame())
        {
            Throw();
        }
    }

    private void Throw()
    {
        audioSource.PlayOneShot(throwSound);
        Rigidbody coconut = Instantiate(coconutPrefab, transform.position, transform.rotation);
        coconut.name = "coconut";
        coconut.linearVelocity = transform.forward * throwSpeed;
    }
}
