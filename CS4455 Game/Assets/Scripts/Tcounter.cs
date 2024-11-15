using UnityEngine;
using TMPro;

public class TCounterUI : MonoBehaviour
{
    public TextMeshProUGUI tCounterText; // Reference to the Text component
    private int tCount = 0;

  
    public void CollectT()
    {
       
        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        tCounterText.text = "T's Collected: " + tCount;
    }
}
