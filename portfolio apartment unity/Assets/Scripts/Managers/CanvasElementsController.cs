using System.Collections;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class CanvasElementsController : MonoBehaviour
{
    private Animator animator;
    private DialogueManager dialogueManager;
    private SoundManager soundManager;
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
        soundManager = SoundManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReadyForInput) return;

        if (Input.GetMouseButtonDown(0))
        {
            OnHandleInputInterrupt();
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
        body.text = "";
        namePlate.text = "";
        currentStory = story;
        Show();
    }

    public void NextDialogue()
    {
        bool hasRantTag = false;
        bool hasLeftAnim = false;
        bool hasRightAnim = false;

        bool isLeftSpeaker = false;
        bool isRightSpeaker = false;

        isDialogueDisplayFinished = false;
        hasTriedToSkip = false;
        
        goNextIndicator.SetActive(false);

        body.text = "";
        namePlate.text = "";

        for (int i = 0; i < currentStory.currentTags.Count; i++)
        {
            string tag = currentStory.currentTags[i].ToLower();
            if (i == 0)
            {
                if (tag == "rcr")
                {
                    isRightSpeaker = true;
                    ProcessNameTag("Recruiter (she/her)", "right");
                }
                else if (tag == "jt")
                {   
                    isLeftSpeaker = true;
                    ProcessNameTag("Justin (he/him)", "left");
                }
                else if (tag == "narrator")
                {
                    isLeftSpeaker = true;
                    ProcessNameTag("Narrator", "left");
                }
                continue;
            }

            if (tag.Contains("anim"))
            {
                if (tag.Contains("left")) hasLeftAnim = true;
                if (tag.Contains("right")) hasRightAnim = true;
                if (tag.Contains("both"))
                {
                    hasLeftAnim = true;
                    hasRightAnim = true;
                }

                ProcessAnimationTag(tag);
            }
            else if (tag.Contains("rant"))
            {
                hasRantTag = true;
            }
        }

        if (isLeftSpeaker)
        {
            if (!hasLeftAnim) left.PlayAnimation("speaking");
            if (!hasRightAnim) right.PlayAnimation("idle");
        } 
        else if (isRightSpeaker)
        {
            if (!hasLeftAnim) left.PlayAnimation("idle");
            if (!hasRightAnim) right.PlayAnimation("speaking");
        } 
        else
        {
            left.PlayAnimation("idle");
            right.PlayAnimation("idle");
        }

        StartDisplayText(hasRantTag, currentStory.currentText);
    }

    public void EndDialogue()
    {
        Hide();
    }

    void ProcessAnimationTag(string tag)
    {
        Debug.Log(tag);
        string[] animationTags = tag.Split("_");
        string animationTarget = animationTags[1].ToLower();
        string animationName = animationTags[2].ToLower();

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
        DialogueManager.Instance.NextDialogue.Invoke();
    }

    void OnHideAnimFinished()
    {
        dialogueManager.EndOfDialogueReached.Invoke();
    }

    void StartDisplayText(bool hasRantTag, string msg, bool isLeftSpeaker, bool isRightSpeaker)
    {
        goNextIndicator.SetActive(false);
        hasTriedToSkip = false;
        textDisplayCoroutine = DisplayText(hasRantTag, msg);
        StartCoroutine(textDisplayCoroutine);
    }

    IEnumerator DisplayText(bool isRant, string msg, bool isLeftSpeaker, bool isRightSpeaker)
    {
        foreach (char c in msg)
        {
            if (!isRant && hasTriedToSkip)
            {
                FinishText(msg);
                break;
            }
            body.text += c;

            soundManager.PlayDialogueBlipEvent();
            yield return new WaitForSeconds(0.1f);
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
        isReadyForInput = true;
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
