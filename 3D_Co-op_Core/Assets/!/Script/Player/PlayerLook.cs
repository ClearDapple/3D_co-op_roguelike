using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform playerTransform;   //플레이어 트랜스폼
    [SerializeField] Transform cameraTransform;   //카메라 트랜스폼
    public float mouseSensitivity = 300f; // 마우스 감도

    float xRotation = 0f;


    void Start()
    {
        // 마우스 커서 고정
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 입력
        float mouseX = Input.GetAxis("Mouse X") * 300f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 300f * Time.deltaTime;
        // 카메라 회전
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 상하 회전 제한
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // 카메라 상하 회전
        transform.parent.Rotate(Vector3.up * mouseX); // 플레이어 좌우 회전
    }
}