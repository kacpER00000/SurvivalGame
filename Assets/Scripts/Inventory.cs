using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AudioClip collectedSound;

    private int charge;

    public int Charge => charge;

    public void CellPickup()
    {
        AudioSource.PlayClipAtPoint(collectedSound, transform.position);
        charge++;
    }
}
