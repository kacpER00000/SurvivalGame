using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AudioClip collectedSound;
    [Header("Charge indicators")]
    [SerializeField] private Texture2D[] hudCharge;
    [SerializeField] private RawImage chargeHud;
    [Header("Generator indicators")]
    [SerializeField] private Texture2D[] meterCharge;
    [SerializeField] private Renderer meter;
    [SerializeField] private TextMeshProUGUI chargeText;

    [Header("Matches")]
    [SerializeField] private RawImage matchHud;
    [SerializeField] private TextHints textHints;
    [SerializeField, TextArea] private string needMatchesHint = "I need something to light the fire with...";

    private int charge;

    public int Charge => charge;

    private bool haveMatches;

    public void MatchPickup()
    {
        AudioSource.PlayClipAtPoint(collectedSound, transform.position);
        matchHud.enabled = true;
        haveMatches = true;
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!hit.collider.TryGetComponent(out Campfire campfire) || campfire.IsLit)
        {
            return;
        }
        if (haveMatches)
        {
            campfire.LightFire();
            haveMatches = false;
            matchHud.enabled = false;
        } else
        {
            textHints.ShowHint(needMatchesHint);
        }
    }

    public void CellPickup()
    {
        AudioSource.PlayClipAtPoint(collectedSound, transform.position);
        charge++;
        HudOn();
        int maxLevel = hudCharge.Length-1;
        int level = Mathf.Min(charge, maxLevel);
        chargeHud.texture = hudCharge[level];
        meter.material.mainTexture = meterCharge[level];
        chargeText.text = level + " / " + maxLevel;
    }

    public void HudOn()
    {
        chargeHud.enabled = true;
        chargeText.enabled = true;
    }

    public void HudOff() {
        chargeHud.enabled = false;
        chargeText.enabled = false;
    }
}
