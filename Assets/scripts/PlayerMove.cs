using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // Tốc độ di chuyển
    public float jumpForce = 5f; // Lực nhảy
    public float rotationSpeed = 100f; // Tốc độ xoay
    private CharacterController characterController;
    
    private Vector3 velocity;
    private Vector3 initialDirection; // Hướng di chuyển ban đầu
    private Animator animator; // Thêm biến Animator

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        initialDirection = transform.forward; // Lưu hướng ban đầu
        animator = GetComponent<Animator>(); // Khởi tạo Animator
    }

    void Update()
    {
        MovePlayer();
        Jump();
        LogRightJoystick(); // Ghi lại giá trị joystick bên phải

    }

    void MovePlayer()
    {
        float horizontal = Gamepad.current.leftStick.x.ReadValue();
        float vertical = Gamepad.current.leftStick.y.ReadValue();

        Vector3 move = initialDirection * vertical + transform.right * horizontal;
        characterController.Move(move.normalized * moveSpeed * Time.deltaTime);

        // Tính toán giá trị Move cho Animator
        bool IsMove = horizontal != 0 || vertical != 0;
        float moveValue = horizontal; // Giá trị di chuyển ngang
        float moveValueVer = vertical; // Giá trị di chuyển dọc
        animator.SetFloat("Move", moveValue); // Cập nhật giá trị Move trong Animator
        animator.SetFloat("MoveVer", moveValueVer); // Cập nhật giá trị Move trong Animator
        if(moveValueVer <0.5f && moveValueVer > -0.5f)
        {
            animator.SetFloat("MoveVer", 0f);
            }
        animator.SetBool("IsMove", IsMove); // Cập nhật giá trị Move trong Animator
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            // Kiểm tra nút nhảy B0 (nút X)
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                velocity.y = jumpForce;
                Debug.Log("Jump!");
            }
        }

        // Thêm trọng lực
        velocity.y -= 9.81f * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    public void Tackle()
    {


        // animator.SetTrigger("IsTackle");


        characterController.Move(velocity * Time.deltaTime);
    }

    void LogRightJoystick()
    {
        // Lấy giá trị từ joystick bên phải
        float rightStickX = Gamepad.current.rightStick.x.ReadValue(); // Giá trị từ joystick bên phải
        RotatePlayer(rightStickX); // Xoay nhân vật

    }

    void RotatePlayer(float rightStickX)
    {
        if (Mathf.Abs(rightStickX) > 0.1f)
        {// Ngưỡng xác định
            float angle = rightStickX * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, angle, 0);//Xoay nhan vat
            initialDirection = transform.forward; // Lưu hướng ban đầu để tiếp tục di thẳng
        }

    }
    
}