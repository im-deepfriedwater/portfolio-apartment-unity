using Ink.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField]
    private CanvasElementsController canvasController;

    [SerializeField]
    private TextAsset dialogueInk = null;

    public UnityEvent NextDialogue;

    public Story CurrentStory;

    // Start is called before the first frame update
    void Start()
    {
        NextDialogue.AddListener(OnNextDialogue);
    }

    void InitDialogue()
    {
        canvasController.Show();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnNextDialogue()
    {
        CurrentStory.Continue();
        canvasController.NextDialogue(CurrentStory);
    }
}
