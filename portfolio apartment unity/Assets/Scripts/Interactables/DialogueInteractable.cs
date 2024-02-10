using UnityEngine;

public class DialogueInteractable : Interactable
{
    [SerializeField]
    private TextAsset story;
    private DialogueManager dialogueManager;

    public override void Start()
    {
        base.Start();
        dialogueManager = DialogueManager.Instance;
    }

    void OnMouseDown()
    {
        dialogueManager.StartDialogue.Invoke(story);
    }
}
