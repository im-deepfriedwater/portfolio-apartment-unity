using Ink.Runtime;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StartDialogueEvent : UnityEvent<TextAsset> { }

public class DialogueManager : Singleton<DialogueManager>
{
    [HideInInspector]
    public UnityEvent NextDialogue;

    [HideInInspector]
    public UnityEvent InputAttempt;

    [HideInInspector]
    public UnityEvent EndOfDialogueReached;

    [HideInInspector]
    public StartDialogueEvent StartDialogue;


    [SerializeField]
    private CanvasElementsController canvasController;

    private Story currentStory;

    public override void AwakeInit()
    {
        NextDialogue.AddListener(OnNextDialogue);
        StartDialogue.AddListener(OnStartDialogue);
        EndOfDialogueReached.AddListener(OnEndOfDialogueReached);
    }

    void OnStartDialogue(TextAsset inkStoryJSON)
    {
        currentStory = new Story(inkStoryJSON.text);
        canvasController.InitDialogue(currentStory);
    }

    void OnNextDialogue()
    {
        if (!currentStory.canContinue)
        {
            canvasController.EndDialogue();
            return;
        }

        currentStory.Continue();
        canvasController.NextDialogue();
    }

    // empty for now
    void OnEndOfDialogueReached()
    {}
}

// Dialogue Manager business logic
// Canvas Controller controlling the frontend and input
// Canvas Controller will drive when we're ready to move through the Dialogue
// Canvas Controller should tell the Dialogue Manager when we're ready to try to show the next dialogue
