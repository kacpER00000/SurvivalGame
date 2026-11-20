using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class CoconutThrower : MonoBehaviour
{
    [SerializeField] private Rigidbody coconutPrefab;
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private float minThrowSpeed = 30f;
    [SerializeField] private float maxThrowSpeed = 60f;
    [SerializeField] private float chargeRate = 20f;
    [SerializeField] private float cooldown = 0.5f;
    

    private bool isCharging = false;
    private float timer;
    private bool threw = false;
    private float currentThrowSpeed;



    private InputAction attackAction;
    private AudioSource audioSource;

    private void OnEnable()
    {
        attackAction.started += StartCharging;
        attackAction.canceled += ReleaseThrow;
    }

    private void OnDisable()
    {
        attackAction.started -= StartCharging;
        attackAction.canceled -= ReleaseThrow;
    }

    public bool CanThrow { get; set; }


    void Awake()
    {
        attackAction = InputSystem.actions.FindAction("Player/Attack");
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        timer = cooldown;
    }


    // Update is called once per frame
    void Update()
    {
        if (threw)
        {
            timer -= Time.deltaTime;
            if(timer < 0f)
            {
                threw = false;
                timer = cooldown;
            }
            return;
        }
        if (isCharging)
        {
            if (!CanThrow)
            {
                isCharging = false;
                currentThrowSpeed = minThrowSpeed;
                return;
            }
            currentThrowSpeed = Math.Min(currentThrowSpeed + chargeRate * Time.deltaTime, maxThrowSpeed);
        }
        
    }

    private void StartCharging(InputAction.CallbackContext callback)
    {
        if(!CanThrow || timer < 0f)
        {
            return;
        }
        isCharging = true;
        currentThrowSpeed = minThrowSpeed;
    }

    private void ReleaseThrow(InputAction.CallbackContext callback)
    {
        if (!isCharging)
        {
            return;
        }
        isCharging = false;
        Throw();
    }

    private void Throw()
    {
        audioSource.PlayOneShot(throwSound);
        Rigidbody coconut = Instantiate(coconutPrefab, transform.position, transform.rotation);
        coconut.name = "coconut";
        coconut.linearVelocity = transform.forward * currentThrowSpeed;
        threw = true;
    }
}
