using UnityEngine;

public class Campfire : MonoBehaviour
{

    [SerializeField] private Light light;

    void Update()
    {
        if (!light.enabled)
        {
            return;
        }
        light.intensity = Random.Range(10.0f, 100.0f);
    }

    private bool isLit;

    public bool IsLit => isLit;

    public void LightFire()
    {
        isLit = true;
        light.enabled = true;
        foreach (ParticleSystem parcitles in GetComponentsInChildren<ParticleSystem>())
            parcitles.Play();
        GetComponent<AudioSource>().Play();
    }
}
