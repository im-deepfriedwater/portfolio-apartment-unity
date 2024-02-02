using UnityEngine;

public class ExclaimBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AudioClip exclaimClip;

    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        soundManager = SoundManager.Instance;
    }

    void OnEnable()
    {
        animator.Play("Base Layer.BounceAndFadeOut");
        soundManager.PlayDialogueExclaimEvent.Invoke(exclaimClip);
    }

    void OnAnimBounceAndFadeOutFinished()
    {
        gameObject.SetActive(false);
    }
}
