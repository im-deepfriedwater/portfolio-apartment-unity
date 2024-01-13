using UnityEngine;
using UnityEngine.AI;

public class FollowerController : Singleton<FollowerController>
{
    private NavMeshAgent agent;

    private PlayerController player;

    public float distanceFromPlayerRunThreshold = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = PlayerController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);

        var distance = Vector3.Distance(player.transform.position.normalized, transform.position.normalized);
        Debug.Log($"Follower {distance}");

        if (distance > distanceFromPlayerRunThreshold)
        {
            // increase speed to run and update state in animation controller
        }
    }
}
