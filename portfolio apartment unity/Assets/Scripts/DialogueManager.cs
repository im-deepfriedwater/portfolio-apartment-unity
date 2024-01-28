using Ink.Runtime;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
  [SerializeField]
  private  CanvasElementsController CanvasController;
  [SerializeField]
	private TextAsset dialogueInk = null;

	public Story story;

  // Start is called before the first frame update
  void Start()
  {
      
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
