using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class IntroDialogueManager: Singleton<IntroDialogueManager>
{
    private ScreenManager screenManager;
    
    private int dialogueIndex;


    [SerializeField]
    private string[] dialogues;

    [SerializeField]
    private IntroDialogueController introDialogueController;

    public UnityEvent IntroDialogueControllerReady;

    public UnityEvent NextDialogue;

    void Start()
    {
        screenManager = ScreenManager.Instance;
        NextDialogue.AddListener(OnNextDialogue);
        IntroDialogueControllerReady.AddListener(OnIntroDialogueControllerReady);
    }

    // Starting point
    void OnIntroDialogueControllerReady()
    {
        introDialogueController.ShowNextDialogue(dialogues[dialogueIndex]);
    }

    void OnNextDialogue()
    {
        dialogueIndex += 1;

        if (dialogueIndex >= dialogues.Count())
        {
            introDialogueController.PrepareForTransition();
            screenManager.TransitionToMainGame();
            return;
        }

        introDialogueController.ShowNextDialogue(dialogues[dialogueIndex]);
    }




}