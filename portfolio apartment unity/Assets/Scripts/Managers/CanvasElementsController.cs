using System;
using System.Collections;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasElementsController : MonoBehaviour
{
    [SerializeField]
    private Button choicePrefab;
    private Animator animator;

    private DialogueManager dialogueManager;
    private SoundManager soundManager;
    private bool isReadyForInput = false;
    private bool hasTriedToSkip = false;
    private bool isDialogueDisplayFinished = false;

    private Story currentStory;

    private IEnumerator textDisplayCoroutine;

    [SerializeField]
    private bool isFirstChoice = false;

    [SerializeField]
    private GameObject choiceContainer;

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

    [SerializeField]
    private AudioClip glassBreakingClip;

    [SerializeField]
    private AudioClip exclaimClip;

    [SerializeField]
    private AudioClip narratorClip;

    [SerializeField]
    private Animator uiActorAnimator;



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
            Debug.Log("I lied i was ready for input :ok:");
            OnHandleInputInterrupt();
        }
    }

    void HideName()
    {
        namePlate.text = "???";
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

        if (currentStory.canContinue) currentStory.Continue();

        if (story.currentChoices.Count > 0)
        {
            Debug.Log("Init dialogue?????????/");
            CreateChoiceView();
            animator.Play("ShowChoices");
        }
        else
        {
            animator.Play("Show");
        }
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        currentStory.ChooseChoiceIndex(choice.index);
        DialogueManager.Instance.NextDialogue.Invoke();
    }

    private void CreateChoiceView()
    {
        // Based off of the example from Ink
        foreach (Choice choice in currentStory.currentChoices)
        {
            Button choiceButton = Instantiate(choicePrefab, choiceContainer.transform);
            TextMeshProUGUI buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = choice.text;
            choiceButton.onClick.AddListener(delegate
            {
                Debug.Log("get chosen");
                OnClickChoiceButton(choice);
            });
        }
    }

    private void CreateDialogueView()
    {
        bool hasRantTag = false;
        bool hasLeftAnim = false;
        bool hasRightAnim = false;

        bool isLeftSpeaker = false;
        bool isRightSpeaker = false;
        bool isNarrator = false;

        bool isSilent = false;

        // narrator by default but can get
        // overwritten
        AudioClip speakerClip = narratorClip;

        isDialogueDisplayFinished = false;
        hasTriedToSkip = false;

        goNextIndicator.SetActive(false);
        left.HideExclaimEvent.Invoke();
        right.HideExclaimEvent.Invoke();

        body.text = "";
        overflowChecker.text = "";
        namePlate.text = "";

        foreach (string currentTag in currentStory.currentTags)
        {
            string tag = currentTag.ToLower();

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
                isNarrator = true;
                ProcessNameTag("Narrator", "left");
            }
            else if (tag == "silent")
            {
                isSilent = true;
            }

            if (tag.Contains("hide_speaker"))
            {
                HideName();
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
            else if (tag.Contains("exclaim"))
            {
                if (tag.Contains("left")) left.ShowExclaimEvent.Invoke();
                if (tag.Contains("right")) right.ShowExclaimEvent.Invoke();
            }
        }

        if (isNarrator)
        {
            left.PlayAnimation("idle");
            right.PlayAnimation("idle");
        }
        else if (isLeftSpeaker)
        {
            if (!isNarrator && !isSilent) uiActorAnimator.Play("ShowLeft");
            if (!hasLeftAnim && !isNarrator) left.PlayAnimation("speaking");
            if (!hasLeftAnim && isNarrator) left.PlayAnimation("idle");
            if (!hasRightAnim) right.PlayAnimation("idle");
            speakerClip = left.DialogueBlip;
        }
        else if (isRightSpeaker)
        {
            if (!isSilent) uiActorAnimator.Play("ShowRight");
            if (!hasLeftAnim) left.PlayAnimation("idle");
            if (!hasRightAnim) right.PlayAnimation("speaking");
            speakerClip = right.DialogueBlip;
        }

        StartDisplayText(hasRantTag, currentStory.currentText, speakerClip);
    }

    public void EndDialogue()
    {
        isReadyForInput = false;
        Hide();
    }

    void ProcessAnimationTag(string tag)
    {
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
                throw new Exception($"CanvasElementController: Bad param for animation target passed {animationTarget}");
        }
    }

    public void NextDialogue()
    {
        isReadyForInput = false;

        if (currentStory.currentChoices.Count > 0)
        {
            Debug.Log("Next dialouge??????/");
            CreateChoiceView();
        }
        else
        {
            CreateDialogueView();
        }
    }

    private void Hide()
    {
        animator.Play("Hide");
    }

    // Called by the Show anim as an AnimationEvent when the anim finishes
    void OnShowAnimFinished()
    {
        NextDialogue();
    }

    // Called by the Hide anim as an AnimationEvent when the anim finishes
    void OnHideAnimFinished()
    {
        dialogueManager.EndOfDialogueReached.Invoke();
    }

    void StartDisplayText(bool hasRantTag, string msg, AudioClip speakerClip)
    {
        goNextIndicator.SetActive(false);
        hasTriedToSkip = false;
        textDisplayCoroutine = DisplayText(hasRantTag, msg, speakerClip);
        StartCoroutine(textDisplayCoroutine);
    }

    IEnumerator DisplayText(bool isRant, string msg, AudioClip speakerClip)
    {
        int i = 0;

        foreach (char c in msg)
        {
            if (!isRant && hasTriedToSkip)
            {
                FinishText(msg);
                break;
            }

            body.text += c;
            soundManager.PlayDialogueBlipEvent.Invoke(speakerClip);

            if (i + 1 < msg.Length)
            {
                overflowChecker.text = string.Copy(body.text) + msg[i + 1];
                if (isRant && overflowChecker.isTextOverflowing) body.text = "";
            }

            if (!isRant)
            {
                yield return new WaitForSeconds(0.08f);
            }
            else
            {
                yield return new WaitForSeconds(0.07f);
            }
        }

        HandleDialogueDisplayFinish();
    }

    void FinishText(string msg)
    {
        body.text = msg;

        // not necessary but
        // possibly nice for consistency
        overflowChecker.text = msg;

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
