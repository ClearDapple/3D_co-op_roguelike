using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigid;
    public float moveSpeed = 5f;          //이동 속도
    public float jumpForce = 5f;          //점프 힘


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 이동
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 velocity = new Vector3(move.x * moveSpeed, rigid.linearVelocity.y, move.z * moveSpeed);
        rigid.linearVelocity = velocity;

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        float sphereRadius = 0.5f; // 플레이어 바닥 반지름
        float castDistance = 0.6f; // 바닥까지 거리 + 여유
        Vector3 origin = transform.position + Vector3.up * 0.1f; // 약간 위에서 시작
        return Physics.SphereCast(origin, sphereRadius, Vector3.down, out RaycastHit hit, castDistance); // 바닥이 있다면 True 반환
    }
}