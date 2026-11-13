using UnityEngine;

public class Campfire : MonoBehaviour
{
    private bool isLit;

    public bool IsLit => isLit;

    public void LightFire()
    {
        isLit = true;
        foreach (ParticleSystem parcitles in GetComponentsInChildren<ParticleSystem>())
            parcitles.Play();
        GetComponent<AudioSource>().Play();
    }
}
