using Ink.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField]
    private CanvasElementsController canvasController;

    public UnityEvent NextDialogue;
    public UnityEvent InputAttempt;
    public UnityEvent EndOfDialogueReached;

    public Story CurrentStory;

    // Start is called before the first frame update
    void Start()
    {
        NextDialogue.AddListener(OnNextDialogue);
    }

    void InitDialogue()
    {
        canvasController.InitDialogue(CurrentStory);
    }

    void OnNextDialogue()
    {   
        if (!CurrentStory.canContinue)
        {
            canvasController.EndDialogue();
            return;
        }

        CurrentStory.Continue();
        canvasController.NextDialogue();
    }
}

// Dialogue Manager business logic
// Canvas Controller controlling the frontend and input? 
// Canvas Controller will drive when we're ready to move through the Dialogue
// Canvas Controller should tell the Dialogue Manager when we're ready to try to show the next dialogue
