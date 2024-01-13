using UnityEngine;
using UnityEngine.AI;

public class PlayerController : Singleton<PlayerController>
{
    public Camera cam;

    public float walkSpeed = 8;
    public float walkAcceleration = 15;

    public float runSpeed = 6;
    public float runAcceleration = 10;

    private Animator animator;
    private NavMeshAgent agent;
    private bool canMove = true;

    private readonly float runDistanceThreshold = 3;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        agent.acceleration = walkAcceleration;

        animator = GetComponentsInChildren<Animator>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleAgentNavigation();
            }

            HandleCurrentMovement();
        }
    }

    void HandleAgentNavigation()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            agent.SetDestination(hit.point);
        }
    }

    void HandleCurrentMovement()
    {
        float distance = Vector3.Distance(agent.destination, transform.position);
        bool isMoving = distance > 0;

        if (distance > runDistanceThreshold)
        {
            agent.speed = runSpeed;
            agent.acceleration = runAcceleration;
        }
        else
        {
            agent.speed = walkSpeed;
            agent.acceleration = walkAcceleration;
        }
        animator.SetBool("IsRunningDistance", distance > runDistanceThreshold);
        animator.SetBool("IsMoving", isMoving);
    }
}
