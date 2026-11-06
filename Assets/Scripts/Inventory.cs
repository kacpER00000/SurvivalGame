using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AudioClip collectedSound;
    [Header("Charge indicators")]
    [SerializeField] private Texture2D[] hudCharge;
    [SerializeField] private RawImage chargeHud;

    private int charge;

    public int Charge => charge;

    public void CellPickup()
    {
        AudioSource.PlayClipAtPoint(collectedSound, transform.position);
        charge++;
        HudOn();
        int level = Mathf.Min(charge, hudCharge.Length-1);
        chargeHud.texture = hudCharge[level];
    }

    public void HudOn() => chargeHud.enabled = true;

    public void HudOff() => chargeHud.enabled = false;
}
