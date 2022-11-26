using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    
    public Dialogue GetDialogue()
    {
        return dialogue;
    }
}
