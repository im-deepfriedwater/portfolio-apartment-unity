using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[System.Serializable]
public class NavMeshClickEvent: UnityEvent<Ray> {}

public class PlayerController : Singleton<PlayerController>
{
    public float WalkSpeed = 8;
    public float WalkAcceleration = 15;

    public float RunSpeed = 6;
    public float RunAcceleration = 10;

    public NavMeshClickEvent NavMeshClickEvent;

    private Animator animator;
    private NavMeshAgent agent;
    private bool canMove = true;

    private readonly float runDistanceThreshold = 3;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = WalkSpeed;
        agent.acceleration = WalkAcceleration;

        animator = GetComponentsInChildren<Animator>()[0];

        NavMeshClickEvent.AddListener(OnNavMeshClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
          HandleCurrentMovement();
        }
    }

    void OnNavMeshClick(Ray ray)
    {
      if (Physics.Raycast(ray, out RaycastHit hit))
      {
        agent.SetDestination(hit.point);
      }
    }

    void HandleCurrentMovement()
    {
        float distance = Vector3.Distance(agent.destination, transform.position);
        bool isMoving = agent.velocity != Vector3.zero;

        if (distance > runDistanceThreshold)
        {
            agent.speed = RunSpeed;
            agent.acceleration = RunAcceleration;
        }
        else
        {
            agent.speed = WalkSpeed;
            agent.acceleration = WalkAcceleration;
        }

        animator.SetBool("IsRunningDistance", distance > runDistanceThreshold);
        animator.SetBool("IsMoving", isMoving);
    }
}
