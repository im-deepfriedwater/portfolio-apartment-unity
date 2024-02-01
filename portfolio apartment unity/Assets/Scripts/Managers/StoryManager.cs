using UnityEngine;
using UnityEngine.Events;

public class StoryManager : Singleton<StoryManager>
{
    [SerializeField]
    private TextAsset introStoryJSON;
    public UnityEvent IntroStoryEvent;

    [SerializeField]
    private TextAsset generalDialogueJSON;
    public UnityEvent GeneralDialogue;
    

    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = DialogueManager.Instance;
        IntroStoryEvent.AddListener(() => dialogueManager.StartDialogue.Invoke(introStoryJSON));
    }

}
