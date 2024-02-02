using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerController : Singleton<PlayerController>
{
    public float WalkSpeed = 8;
    public float WalkAcceleration = 15;

    public float RunSpeed = 6;
    public float RunAcceleration = 10;

    private Animator animator;
    private NavMeshAgent agent;
    private bool canMove = false;
    private DialogueManager dialogueManager;
    private EventManager eventManager;
    private StoryManager storyManager;

    private readonly float runDistanceThreshold = 3;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = WalkSpeed;
        agent.acceleration = WalkAcceleration;

        animator = GetComponentsInChildren<Animator>()[0];

        eventManager = EventManager.Instance;
        eventManager.NavMeshClickEvent.AddListener(OnNavMeshClick);

        dialogueManager = DialogueManager.Instance;
        dialogueManager.EndOfDialogueReached.AddListener(OnEndOfDialogueReached);
        dialogueManager.StartDialogue.AddListener(OnStartDialogue);

        storyManager = StoryManager.Instance;
        storyManager.IntroStoryEvent.AddListener(() => animator.Play("Death"));
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            HandleCurrentMovement();
        }
    }

    void OnStartDialogue(TextAsset _)
    {
        canMove = false;
    }

    void OnEndOfDialogueReached()
    {
        canMove = true;
        animator.Play("Idle");
    }

    void OnNavMeshClick(Ray ray)
    {
        if (!canMove) return;
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
