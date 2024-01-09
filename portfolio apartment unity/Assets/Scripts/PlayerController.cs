using UnityEngine;
using UnityEngine.AI;

public class PlayerController : Singleton<PlayerController>
{
    public Camera cam;
    private NavMeshAgent agent;
    private PlayerStateMachineController stateMachine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // has start & update monobehaviour logic
        stateMachine = GetComponent<PlayerStateMachineController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
