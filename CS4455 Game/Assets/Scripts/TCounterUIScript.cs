using UnityEngine;
using TMPro;

public class TCounterUIScript : MonoBehaviour
{
    public TextMeshProUGUI tCounterText; // Reference to the Text component
    private int tCount = 0;

    void Start()
    {
        UpdateCounterText();
    }

    public void CollectT()
    {
        
        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        tCounterText.text = "T's Collected: " + tCount;
    }
}
