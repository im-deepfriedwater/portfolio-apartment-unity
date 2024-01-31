using cakeslice;
using Ink.Runtime;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    private bool isInputActive = false; 
    protected Outline Outline;

    // Start is called before the first frame update
    public virtual void Start()
    {
      Outline = GetComponent<Outline>();
      Outline.eraseRenderer = true;
      DialogueManager.Instance.StartDialogue.AddListener((Story _) => isInputActive = false);
      DialogueManager.Instance.StartDialogue.AddListener((Story _) => isInputActive = true);

    }

    // Update is called once per frame
    virtual public void Update()
    {}


    public virtual void OnMouseEnter()
    {
      if (!isInputActive) return;
      Outline.eraseRenderer = false;
    }

    public virtual void OnMouseExit()
    {
      Outline.eraseRenderer = true;
    }
}
