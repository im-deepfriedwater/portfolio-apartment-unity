using UnityEngine;
using UnityEngine.AI;

public class FollowerController : Singleton<FollowerController>
{
    private Animator animator;
    private NavMeshAgent agent;
    private bool canMove = true;
    private DialogueManager dialogueManager;

    private PlayerController player;

    public float RunDistanceThreshold = 0.5f;
    public float WalkSpeed = 8;
    public float WalkAcceleration = 15;

    public float RunSpeed = 6;
    public float RunAcceleration = 10;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentsInChildren<Animator>()[0];
        player = PlayerController.Instance;
        dialogueManager = DialogueManager.Instance;

        dialogueManager.EndOfDialogueReached.AddListener(() => animator.Play("Death"));
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


        if (distance > RunDistanceThreshold)
        {
            agent.speed = RunSpeed;
            agent.acceleration = RunAcceleration;
        }
        else
        {
            agent.speed = WalkSpeed;
            agent.acceleration = WalkAcceleration;
        }

        animator.SetBool("IsRunningDistance", distance > RunDistanceThreshold);
        animator.SetBool("IsMoving", isMoving);
    }
}
