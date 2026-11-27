using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoconutWin : MonoBehaviour
{
    [SerializeField] private int targetsToWin = 3;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private GameObject cellPrefab;

    private int targetsDown;
    private bool haveWon;

    public void TargetDown()
    {
        targetsDown++;
        if(targetsDown >= targetsToWin && !haveWon)
        {
            Win();
        }
    }

    public void TargetUp() => targetsDown--;

    private void Win()
    {
        haveWon = true;
        GetComponent<AudioSource>().PlayOneShot(winSound);

        Transform displayCell = transform.Find("powerCell");
        Vector3 position = displayCell.position + transform.right * -1;
        Instantiate(cellPrefab, position, displayCell.rotation);
        Destroy(displayCell.gameObject);
    }
}
