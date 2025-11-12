using UnityEngine;

public interface IState
{
    void Enter(Animator animator);   // 상태 진입 시 실행
    void Update(Animator animator);  // 매 프레임 실행
    void Exit(Animator animator);    // 상태 종료 시 실행
}
