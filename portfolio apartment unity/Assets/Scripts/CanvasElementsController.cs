using System.Collections;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class CanvasElementsController : MonoBehaviour
{   
    private Animator animator;
    private DialogueManager dialogueManager;
    private bool isReadyForInput = false;

    private IEnumerator textDisplayCoroutine;

    [SerializeField]
    private TextMeshProUGUI body;

    [SerializeField]
    private TextMeshProUGUI overflowChecker;

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

    void ProcessNameTag(string newValue, string side)
    {
        namePlate.text = newValue;
        namePlate.alignment = side == "left"
            ? TextAlignmentOptions.Left
            : TextAlignmentOptions.Right;
    }

    public void NextDialogue(Story story)
    {   
        bool isRant = false;
        for (int i = 0; i < story.currentTags.Count; i++)
        {
            string tag = story.currentTags[i].ToLower();
            if (i == 0)
            {
                if (tag == "rcr")
                {
                    ProcessNameTag("Recruiter (she/her)", "right");
                }
                else if (tag == "jt")
                {
                    ProcessNameTag("Justin (he/him)", "left");
                }
                else if (tag == "narrator")
                {
                    ProcessNameTag("Narrator", "left");

                }
                continue;
            }

            if (tag.Contains("anim"))
            {
                ProcessAnimationTag(tag);
            }
        }

        StartDisplayText(isRant, story.currentText);
    }


    void ProcessAnimationTag(string tag)
    {
        string[] animationTags = tag.Split("_");
        string animationTarget = animationTags[1];
        string animationName = animationTags[2];

        switch (animationTarget)
        {
            case "left":
                left.PlayAnimation(animationName);
                break;

            case "right":
                right.PlayAnimation(animationName);
                break;

            case "both":
                left.PlayAnimation(animationName);
                right.PlayAnimation(animationName);
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

    void StartDisplayText(bool isRant, string msg)
    {
        textDisplayCoroutine = DisplayText(isRant, msg);
        StartCoroutine(textDisplayCoroutine);
    }

    IEnumerator DisplayText(bool isRant, string msg)
    {   
        foreach (char c in msg)
        {
            body.text += c;
            yield return null;
        }
    }


}
