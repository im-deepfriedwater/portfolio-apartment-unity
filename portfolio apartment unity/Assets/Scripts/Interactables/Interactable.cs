using cakeslice;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{   
    [SerializeField]
    private Outline[] childrenOutlines;
    protected bool IsInputActive = false; 
    protected Outline Outline;

    // Start is called before the first frame update
    public virtual void Start()
    {
      Outline = GetComponent<Outline>();
      Outline.eraseRenderer = true;
      DialogueManager.Instance.StartDialogue.AddListener((TextAsset _) => IsInputActive = false);
      DialogueManager.Instance.EndOfDialogueReached.AddListener(() => IsInputActive = true);
    }

    // Update is called once per frame
    virtual public void Update()
    {}


    public virtual void OnMouseEnter()
    {
      if (!IsInputActive) return;
      Outline.eraseRenderer = false;

      foreach (var childOutline in childrenOutlines)
      {
        childOutline.eraseRenderer = false;
      }
    }

    public virtual void OnMouseExit()
    {
      Outline.eraseRenderer = true;

      
      foreach (var childOutline in childrenOutlines)
      {
        childOutline.eraseRenderer = true;
      }
    }
}
