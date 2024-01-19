using UnityEngine;

public class GroundNavMesh : MonoBehaviour
{
  public Camera Cam;

  private PlayerController player;

  void Start()
  {
    player = PlayerController.Instance;
  }

  void OnMouseDown()
  {
    player.NavMeshClickEvent.Invoke(Cam.ScreenPointToRay(Input.mousePosition));
  }
}
