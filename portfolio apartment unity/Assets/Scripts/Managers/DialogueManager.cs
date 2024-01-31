using Ink.Runtime;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StartDialogueEvent : UnityEvent<Story> { }

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField]
    private TextAsset introStory = null;
    [SerializeField]
    private TextAsset generalDialogueStory = null;
    [SerializeField]
    private CanvasElementsController canvasController;

    public UnityEvent NextDialogue;
    public UnityEvent InputAttempt;
    
    public UnityEvent StartDialogue;
    public UnityEvent EndOfDialogueReached;

    public UnityEvent InitGeneralDialogue;

    private Story currentStory;

    public override void AwakeInit()
    {
        NextDialogue.AddListener(OnNextDialogue);
        InitGeneralDialogue.AddListener(() => InitDialogue(new Story(generalDialogueStory.text)));
    }

    void InitDialogue(Story story)
    {
        currentStory = story;
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
