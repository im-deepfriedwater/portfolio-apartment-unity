using UnityEngine;

public class PlayerStateMachineController: MonoBehaviour
{
    private State currentState;

    public void Start()
    {
        currentState = new PlayerIdleState();
        currentState.Enter();
    }

    private void Update()
    {
        currentState.Update();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }
}
