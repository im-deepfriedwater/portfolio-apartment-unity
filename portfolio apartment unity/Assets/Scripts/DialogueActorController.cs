using UnityEngine;

public class DialogueActorController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void PlayAnimation(string animationTagName)
    {
      var animName = animationTagName[0].ToString().ToUpper() + animationTagName.Substring(1);
      animator.Play(animName);
    }
}
