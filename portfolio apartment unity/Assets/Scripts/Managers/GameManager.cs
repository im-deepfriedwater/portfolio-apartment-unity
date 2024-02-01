public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        StoryManager.Instance.IntroStoryEvent.Invoke();
    }

}
