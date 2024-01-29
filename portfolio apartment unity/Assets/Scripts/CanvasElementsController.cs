using System.Diagnostics;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class CanvasElementsController : MonoBehaviour
{
    private Animator animator;
    private DialogueManager dialogueManager;
    private bool isReadyForInput = false;

    [SerializeField]
    private TextMeshProUGUI body;

    [SerializeField]
    private TextMeshProUGUI namePlate;

    [SerializeField]
    private DialogueActorController left;

    [SerializeField]
    private DialogueActorController right;

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

    public void NextDialogue(Story story)
    {
        for (int i = 0; i < story.currentTags.Count; i++)
        {
            string tag = story.currentTags[i].ToLower();
            if (i == 0)
            {
                if (tag == "rcr")
                {

                } else if (tag == "jt")
                {

                }
                continue;
            }

            if (tag.Contains("anim"))
            {
                ProcessAnimationTag(tag);
            }
        }
    }

    void ProcessAnimationTag(string tag)
    {
        string[] animationTags = tag.Split("_");
        string animationTarget = animationTags[1];
        string animationName = animationTags[2];

        switch (animationTarget)
        {
            case "left":
                
            break;

            case "right":
            break;

            case "both":
            break;

            default:
                throw new System.Exception($"CanvasElementController: Bad param for animation target passed {animationTarget}");
        }
    }

    public void Hide()
    {
        animator.Play("Hide");
    }

    public void Show()
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
