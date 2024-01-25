using UnityEngine;

public class GroundNavMesh : MonoBehaviour
{
  public Camera Cam;

  private EventManager eventManager;

  void Start()
  {
    eventManager = EventManager.Instance;
  }

  void OnMouseDown()
  {
    eventManager.NavMeshClickEvent.Invoke(Cam.ScreenPointToRay(Input.mousePosition));
  }
}
