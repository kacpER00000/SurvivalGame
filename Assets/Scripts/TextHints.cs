using TMPro;
using UnityEngine;

[RequireComponent(typeof (TextMeshProUGUI))]
public class TextHints : MonoBehaviour
{
    [SerializeField] private float displayTime = 4f;

    private TextMeshProUGUI hintText;
    private float timer;

    void Awake()
    {
        hintText = GetComponent<TextMeshProUGUI>();
        hintText.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!hintText.enabled)
        {
            return;
        }
        timer += Time.deltaTime;
        if(displayTime - timer <= 1f)
        {
            hintText.alpha *= 0.9f;
        }
        if(timer >= displayTime)
        {
            hintText.enabled = false;
            hintText.alpha = 1f;
        }
    }

    public void ShowHint(string message)
    {
        hintText.text = message;
        hintText.enabled = true;
        timer = 0f;

    }
}
