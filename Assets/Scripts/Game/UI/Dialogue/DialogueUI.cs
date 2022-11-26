using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    public void StartConversation()
    {
        text.enabled = true;
    }

    public void EndConversation()
    {
        text.enabled = false;
    }

    public void DisplayText(string newText)
    {
        text.SetText(newText);
    }
}
