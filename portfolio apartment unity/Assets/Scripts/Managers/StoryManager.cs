using UnityEngine;
using UnityEngine.Events;

// This class feels unneeded. Leaving for now but
// could be refactored or repurposed. 
public class StoryManager : Singleton<StoryManager>
{
    [SerializeField]
    private TextAsset introStoryJSON;
    public UnityEvent IntroStoryEvent;    

    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = DialogueManager.Instance;
        IntroStoryEvent.AddListener(() => dialogueManager.StartDialogue.Invoke(introStoryJSON));
    }

}
