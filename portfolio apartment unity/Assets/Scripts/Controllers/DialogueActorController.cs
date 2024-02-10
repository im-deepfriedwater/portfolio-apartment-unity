using UnityEngine;
using UnityEngine.Events;

public class DialogueActorController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private ExclaimBehaviour exclaimIndicator;

    public AudioClip DialogueBlip;

    [HideInInspector]
    public UnityEvent ShowExclaimEvent;

    [HideInInspector]
    public UnityEvent HideExclaimEvent;

    void Start()
    {
        ShowExclaimEvent.AddListener(() => exclaimIndicator.ShowExclaim());
        HideExclaimEvent.AddListener(() => exclaimIndicator.HideExclaim());
    }

    // e.g. animationTagName = "shocked"
    public void PlayAnimation(string animationTagName)
    {
        var animName = animationTagName[0].ToString().ToUpper() + animationTagName.Substring(1);
        animator.Play($"Base Layer.{animName}");
    }
}
