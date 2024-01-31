using Ink.Runtime;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.Instance.StartDialogue.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
