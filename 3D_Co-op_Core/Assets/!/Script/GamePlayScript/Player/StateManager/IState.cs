public interface IState
{
    void Enter();   // 상태 진입 시 실행
    void Update();  // 매 프레임 실행
    void Exit();    // 상태 종료 시 실행
}
