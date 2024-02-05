using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    private GameManager gameManager;
    private SoundManager soundManager;

    [SerializeField]
    AudioClip glassBreaking;

    private Image image;

    [SerializeField]
    private bool isDebug = false;

    void Start()
    {
        gameManager = GameManager.Instance;
        soundManager = SoundManager.Instance;
        image = GetComponent<Image>();

        if (isDebug)
        {
            image.enabled = true;
        }
    }

    public void OnAnimTransitionSlideOutStart()
    {
        soundManager.PlayGlassBreakingEvent.Invoke(glassBreaking);
    }

    // called from the animation
    public void OnAnimTransitionSlideOutFinished()
    {
        gameManager.StartGame.Invoke();
    }
}
