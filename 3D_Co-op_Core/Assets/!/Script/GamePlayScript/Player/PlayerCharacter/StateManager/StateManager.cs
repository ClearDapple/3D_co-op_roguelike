using UnityEngine;

public class StateManager : MonoBehaviour
{
    public Animator animator;
    public IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState == newState) return;

        currentState?.Exit(animator);
        currentState = newState;
        currentState.Enter(animator);
    }

    public void Update()
    {
        currentState?.Update(animator);
    }
}

public class IdleState : IState
{
    public void Enter(Animator animator)
    {
        animator.SetBool("isDead", false);
        animator.SetBool("isGround", true);
        animator.SetBool("isMove", false);
    }

    public void Update(Animator animator)
    {
        
    }

    public void Exit(Animator animator)
    {

    }
}

public class WalkState : IState
{
    public void Enter(Animator animator)
    {
        animator.SetBool("isDead", false);
        animator.SetBool("isGround", true);
        animator.SetBool("isMove", true);
        animator.SetBool("isRun", false);
    }

    public void Update(Animator animator)
    {

    }

    public void Exit(Animator animator)
    {

    }
}

public class RunState : IState
{
    public void Enter(Animator animator)
    {
        animator.SetBool("isDead", false);
        animator.SetBool("isGround", true);
        animator.SetBool("isMove", true);
        animator.SetBool("isRun", true);
    }

    public void Update(Animator animator)
    {

    }

    public void Exit(Animator animator)
    {
    }
}

public class AttackState : IState
{
    public void Enter(Animator animator)
    {
        animator.SetBool("isDead", false);
        animator.SetTrigger("Attack");
    }

    public void Update(Animator animator)
    {

    }

    public void Exit(Animator animator)
    {

    }
}

public class PickUpState : IState
{
    public void Enter(Animator animator)
    {
        animator.SetBool("isDead", false);
        animator.SetTrigger("PickUp");
    }

    public void Update(Animator animator)
    {

    }

    public void Exit(Animator animator)
    {

    }
}

public class DeadState : IState
{
    public void Enter(Animator animator)
    {
        animator.SetBool("isDead", true);
    }

    public void Update(Animator animator)
    {

    }

    public void Exit(Animator animator)
    {

    }
}
