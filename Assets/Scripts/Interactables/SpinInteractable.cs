using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class SpinInteractable : Interactable
{
  private Rigidbody body;
  public float spinSpeed = 100;

  public override void Start()
  {
    base.Start();
    body = GetComponent<Rigidbody>();
  }

  void OnMouseDown()
  {
    body.AddTorque(new Vector3(0, spinSpeed, 0));
  }
}
