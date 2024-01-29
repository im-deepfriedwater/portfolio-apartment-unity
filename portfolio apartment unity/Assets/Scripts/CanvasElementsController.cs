using UnityEngine;

public class CanvasElementsController : MonoBehaviour
{
    private Animator animator;
    private DialogueManager dialogueManager;
    private bool isReadyForInput = false;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dialogueManager = DialogueManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        bool inputEvent = false;

        if (!isReadyForInput) return;
    
        if (inputEvent)
        {
            dialogueManager.NextDialogue.Invoke();
        }
    }

    void NextDialogue()
    {

    }

    void Hide()
    {
        animator.Play("Hide");
    }

    void Show()
    {
        animator.Play("Show");
    }

    void OnShowAnimFinished()
    {
        
    }

    void DisplayText()
    {

    }
}
