using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueController : MonoBehaviour
{
    [SerializeField] float typingSpeed = 0.05f;
    [SerializeField] public Dialogue currentDialogue; //todo set private
    [SerializeField] public GameObject playerUI; //todo set other method
    DialogueUI dialogueUI;
    DialogueNode currentNode;

    public Action onFinishDialogue;

    void Start() 
    {
        dialogueUI = FindObjectOfType<DialogueUI>();
        dialogueUI.gameObject.SetActive(false);
    }

    public void StartConversation()
    {
        dialogueUI.gameObject.SetActive(true);
        playerUI.SetActive(false);
        dialogueUI.StartConversation();

        currentNode = currentDialogue.GetRootNode();

        StartCoroutine(TextTypingCoroutine(currentNode.text));
    }

    IEnumerator TextTypingCoroutine(string text)
    {
        string textType = "";
        foreach(char character in text)
        {
            textType += character;
            dialogueUI.DisplayText(textType);
            yield return new WaitForSeconds(typingSpeed);
        }

        dialogueUI.DisplayText(text);
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));

        if (HaveNextNode())
        {
            StartCoroutine(TextTypingCoroutine(currentNode.text));
        }
        else
        {
            dialogueUI.gameObject.SetActive(false);
            playerUI.SetActive(true);
            dialogueUI.EndConversation();
            onFinishDialogue?.Invoke();
        }
    }

    bool HaveNextNode()
    {
        currentNode = currentDialogue.GetNodeFromString(currentNode.GetNextNode());

        if (currentNode == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
