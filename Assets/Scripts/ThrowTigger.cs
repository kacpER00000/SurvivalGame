using System;
using UnityEngine;

public class ThrowTigger : MonoBehaviour
{
    [SerializeField] private TextHints textHints;
    [SerializeField, TextArea] private string shootHint = "Click LMB to throw a coconut.";
    void OnTriggerEnter(Collider other)
    {
        textHints.ShowHint(shootHint);
        SetCanThrow(other, true);
    }

    void OnTriggerExit(Collider other) => SetCanThrow(other, false);

    private void SetCanThrow(Collider other, bool canThrow)
    {
        var thrower = other.GetComponentInChildren<CoconutThrower>();
        if(thrower != null)
        {
            thrower.CanThrow = canThrow;
        }
    }
}
