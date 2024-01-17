using UnityEngine;
using UnityEngine.AI;

public class FollowerController : Singleton<FollowerController>
{
    private Animator animator;
    private NavMeshAgent agent;
    private bool canMove = true;

    private PlayerController player;

    public float runDistanceThreshold = 0.5f;
    public float walkSpeed = 8;
    public float walkAcceleration = 15;

    public float runSpeed = 6;
    public float runAcceleration = 10;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentsInChildren<Animator>()[0];
        player = PlayerController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            HandleAgentNavigation();
            HandleCurrentMovement();
        }
    }

    void HandleAgentNavigation()
    {
        agent.SetDestination(player.transform.position);
    }

    void HandleCurrentMovement()
    {
        var distance = Vector3.Distance(player.transform.position.normalized, transform.position.normalized);
        bool isMoving = agent.velocity != Vector3.zero;


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
