using UnityEngine;

public class ExclaimBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AudioClip exclaimClip;

    private SoundManager soundManager;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {   
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;
        soundManager = SoundManager.Instance;
        animator = GetComponent<Animator>();
    }

    public void ShowExclaim()
    {   
        spriteRenderer.enabled = true;
        animator.Play("Base Layer.BounceAndFadeOut");
        soundManager.PlayDialogueExclaimEvent.Invoke(exclaimClip);
    }

    public void HideExclaim()
    {   
        spriteRenderer.enabled = false;
        spriteRenderer.color = new Color(0, 0, 0, 0);
    }

    void OnAnimBounceAndFadeOutFinished()
    {
        spriteRenderer.enabled = false;
    }
}
