using cakeslice;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    protected Outline Outline;

    // Start is called before the first frame update
    public virtual void Start()
    {
      Outline = GetComponent<Outline>();
    }

    // Update is called once per frame
    virtual public void Update()
    {}


    public virtual void OnMouseEnter()
    {
      Outline.eraseRenderer = false;
    }

    public virtual void OnMouseExit()
    {
      Outline.eraseRenderer = true;
    }
}
