using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float moveSpeed = 3f;          // 이동 속도
    public float jumpForce = 1f;          // 점프 힘
    public float gravity = -9.81f;        // 중력 값

    private Vector3 velocity;             // 현재 속도
    private bool isGrounded;              // 바닥에 닿았는지 여부

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 바닥 체크
        isGrounded = controller.isGrounded;
        if (isGrounded && IsGrounded() && velocity.y < 0)
        {
            velocity.y = -1f; // 바닥에 있을 때 약간의 음수로 유지
        }

        // 이동
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && IsGrounded())
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // 중력 적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.4f, LayerMask.GetMask("Ground"));
    }
}