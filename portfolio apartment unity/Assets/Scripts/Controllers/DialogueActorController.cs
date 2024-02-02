using UnityEngine;

public class DialogueActorController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public AudioClip DialogueBlip;

    // e.g. animationTagName = "shocked"
    public void PlayAnimation(string animationTagName)
    {
      var animName = animationTagName[0].ToString().ToUpper() + animationTagName.Substring(1);
      animator.Play($"Base Layer.{animName}");
    }
}
