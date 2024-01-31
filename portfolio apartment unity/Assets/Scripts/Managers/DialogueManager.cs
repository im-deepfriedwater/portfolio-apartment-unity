using Ink.Runtime;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StartStoryEvent : UnityEvent<Story> { }

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField]
    private CanvasElementsController canvasController;

    public UnityEvent NextDialogue;
    public UnityEvent InputAttempt;
    public StartStoryEvent StartDialogue;
    public UnityEvent EndOfDialogueReached;

    private Story currentStory;

    // Start is called before the first frame update
    void Start()
    {   
        StartDialogue.AddListener(OnStartDialogue);
        NextDialogue.AddListener(OnNextDialogue);
    }



    void OnStartDialogue(StartStoryEvent story)
    {
        story = currentStory;
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

}

// Dialogue Manager business logic
// Canvas Controller controlling the frontend and input
// Canvas Controller will drive when we're ready to move through the Dialogue
// Canvas Controller should tell the Dialogue Manager when we're ready to try to show the next dialogue
