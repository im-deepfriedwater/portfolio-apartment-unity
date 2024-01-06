
using UnityEngine.AI;

public class FollowerController : Singleton<FollowerController>
{
    private NavMeshAgent agent;
    private PlayerController player;


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
    }
}
