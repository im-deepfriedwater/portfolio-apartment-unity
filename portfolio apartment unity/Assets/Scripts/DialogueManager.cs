using Ink.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField]
    private  CanvasElementsController CanvasController;

    [SerializeField]
	private TextAsset dialogueInk = null;

    public UnityEvent NextDialogue;

	public Story story;

  // Start is called before the first frame update
  void Start()
  {
      NextDialogue.AddListener(OnNextDialogue);
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  void OnNextDialogue()
  {

  }
}
