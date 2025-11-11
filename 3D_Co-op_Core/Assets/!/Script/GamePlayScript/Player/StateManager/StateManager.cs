using UnityEngine;

public class StateManager : MonoBehaviour
{
    public IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState == newState) return;

        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }
}

public class IdleState : IState
{
    public void Enter()
    {

    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}

public class SitState : IState
{
    public void Enter()
    {
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}

public class WalkState : IState
{
    public void Enter()
    {    
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}

public class RunState : IState
{
    public void Enter()
    {
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}

public class AttackState : IState
{
    public void Enter()
    {
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}

public class PickUpState : IState
{
    public void Enter()
    {
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}

public class DeadState : IState
{
    public void Enter()
    {
    }

    public void Update()
    {
    }

    public void Exit()
    {
    }
}
