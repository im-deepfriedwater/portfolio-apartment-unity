using System.Collections;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class CanvasElementsController : MonoBehaviour
{   
    private Animator animator;
    private DialogueManager dialogueManager;
    private bool isReadyForInput = false;
    private bool hasTriedToSkip = false;
    private bool isDialogueDisplayFinished = false;

    private Story currentStory;

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

    [SerializeField]
    private GameObject goNextIndicator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dialogueManager = DialogueManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReadyForInput) return;

        if (Input.GetMouseButtonDown(0))
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

    // Call this first when starting
    // dialogue. Then, call NextDialogue
    // when player prompts the dialogue 
    // forward.
    public void InitDialogue(Story story)
    {
        currentStory = story;
        Show();
    }

    public void NextDialogue()
    {   
        bool hasRantTag = false;
        isDialogueDisplayFinished = false;
        goNextIndicator.SetActive(false);

        for (int i = 0; i < currentStory.currentTags.Count; i++)
        {
            string tag = currentStory.currentTags[i].ToLower();
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
            } else if (tag.Contains("rant"))
            {
                hasRantTag = true;
            }
        }

        StartDisplayText(hasRantTag, currentStory.currentText);
    }

    public void EndDialogue()
    {
        Hide();
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

    private void Hide()
    {
        animator.Play("Hide");
    }

    private void Show()
    {
        animator.Play("Show");
    }

    // Called by the Show anim as an AnimationEvent when the anim finishes
    void OnShowAnimFinished()
    {
        NextDialogue();
    }

    void OnHideAnimFinished()
    {
        dialogueManager.EndOfDialogueReached.Invoke();
    }

    void StartDisplayText(bool hasRantTag, string msg)
    {   
        hasTriedToSkip = false;
        textDisplayCoroutine = DisplayText(hasRantTag, msg);
        StartCoroutine(textDisplayCoroutine);
    }

    IEnumerator DisplayText(bool isRant, string msg)
    {   
        foreach (char c in msg)
        {
            if (!isRant && hasTriedToSkip)
            {
                FinishText(msg);
                break;
            }
            body.text += c;
            yield return null;
        }

        HandleDialogueDisplayFinish();
    }

    void FinishText(string msg)
    {
        body.text = msg;
        HandleDialogueDisplayFinish();
    }

    void HandleDialogueDisplayFinish()
    {
        isDialogueDisplayFinished = true;
        goNextIndicator.SetActive(true);
    }

    void OnHandleInputInterrupt()
    {
        if (!isReadyForInput) return;
        
        if (!isDialogueDisplayFinished)
        {
            hasTriedToSkip = true;
        }

        if (isDialogueDisplayFinished)
        {
            dialogueManager.NextDialogue.Invoke();
        }
    }
}
