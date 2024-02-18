using System.Collections;
using TMPro;
using UnityEngine;

public class IntroDialogueController: MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI screenTextUI;

    [SerializeField]
    private TextMeshProUGUI inputPromptUI;

    private IntroDialogueManager introDialogueManager;

    private bool isInputReady = false;

    void Start()
    {
        introDialogueManager = IntroDialogueManager.Instance;
        introDialogueManager.IntroDialogueControllerReady.Invoke();
    }

    void Update()
    {
        if (!isInputReady) return;

        if (Input.GetMouseButtonDown(0))
        {
            isInputReady = false;
            HandleNextInput();
        }
    }

    public void ShowNextDialogue(string msg)
    {
        screenTextUI.text = msg;
        isInputReady = true;
    }

    public void PrepareForTransition()
    {
        isInputReady = false;
        inputPromptUI.enabled = false;
        StartCoroutine(DisplayLoadingTextAnimated());
    }

    private void HandleNextInput()
    {
        introDialogueManager.NextDialogue.Invoke();
    }

    IEnumerator DisplayLoadingTextAnimated()
    {
        int ellipsisCount = 0;

        while (true)
        {
            ellipsisCount += 1;

            if (ellipsisCount > 3) ellipsisCount = 0;
        
            screenTextUI.text = "Loading ";

            for (int i = 0; i < ellipsisCount; i++)
            {
                screenTextUI.text += ".";
            }

            yield return new WaitForSeconds(1);
        }
    }

}