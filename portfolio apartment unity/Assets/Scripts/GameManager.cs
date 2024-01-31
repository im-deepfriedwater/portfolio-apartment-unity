using Ink.Runtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    [SerializeField]
    private Story introStory;
    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.Instance.StartDialogue.Invoke(introStory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
