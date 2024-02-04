using UnityEngine;

public class TransitionController : MonoBehaviour
{
    private GameManager gameManager;
    private SoundManager soundManager;

    [SerializeField]
    AudioClip glassBreaking;

    

    void Start()
    {
        gameManager = GameManager.Instance;
        soundManager = SoundManager.Instance;
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
