using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public UnityEvent StartGame;

    // Start is called before the first frame update
    void Start()
    {   
        StartGame.AddListener(() => StoryManager.Instance.IntroStoryEvent.Invoke());
    }
}
