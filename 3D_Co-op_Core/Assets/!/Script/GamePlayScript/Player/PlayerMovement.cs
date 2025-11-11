using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField] private float moveSpeed; // 이동 속도
    public float walkSpeed = 3f;
    public float runSpeed = 4f;
    public float jumpForce = 1f; // 점프 힘
    public float gravity = -9.81f; // 중력 값
    public LayerMask groundLayer;

    private Vector3 velocity; // 현재 속도


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        moveSpeed = walkSpeed;
    }

    void Update()
    {
        GetMove();

        GetRun();

        // 바닥 체크
        if (IsGrounded() && velocity.y < 0)
        {
            animator.SetBool("isGround", true);

            if (velocity.y > -1)
            {
                velocity.y = -1; // 바닥에 있을 때 약간의 음수로 유지
            }
        }
        else animator.SetBool("isGround", false);


        // 중력 적용
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void GetMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        animator.SetBool("isMove", moveX!=0 || moveZ!=0);
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    public void GetRun()
    {
        bool isRun = Input.GetKey(KeyCode.LeftShift) ? true : false;
        animator.SetBool("isRun", isRun);
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
    }

    public void GetJump()
    {
        if (IsGrounded() && velocity.y < 0)
        {
            animator.SetTrigger("Jump");
            velocity.y = 0;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.4f, groundLayer);
    }
}