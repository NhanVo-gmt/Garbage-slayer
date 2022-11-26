using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : CoreComponent
{
    InputManager inputManager;

    DialogueController dialogueController;

    [SerializeField] bool isInteracting = false;

    protected override void Awake()
    {
        base.Awake();

        inputManager = GetComponentInParent<InputManager>();
        dialogueController = GetComponentInChildren<DialogueController>();
    }

    void OnEnable() 
    {
        dialogueController.onFinishDialogue += FinishInteract;
    }

    void OnDisable() 
    {
        dialogueController.onFinishDialogue -= FinishInteract;
    }

    void Update() 
    {
        if (inputManager.interactionInput)
        {
            inputManager.UseInteractionInput();

            Interact();
        }
    }

    void Interact() 
    {
        if (isInteracting) return;

        if (dialogueController.currentDialogue != null)
        {
            dialogueController.StartConversation();
            isInteracting = true;
        }
    }

    public void SetDialogue(Dialogue dialogue)
    {
        dialogueController.currentDialogue = dialogue;
    }

    public void UnsetDialogue(Dialogue dialogue)
    {
        if (dialogueController.currentDialogue == dialogue)
        {
            dialogueController.currentDialogue = null;
        }
    }


    void FinishInteract()
    {
        isInteracting = false;
    }
}
