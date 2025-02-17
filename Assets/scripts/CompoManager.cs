using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ComboManager : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    private BoxCollider boxCollider; // Thêm BoxCollider

    private float comboTimer = 0.5f; // Thời gian tối đa để thực hiện combo
    private float timer;


    PlayerMovement playerMovement;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Giảm timer nếu đang trong trạng thái combo
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ResetCombo();
            }
        }

        // Kiểm tra nếu nút North được nhấn
        if (Gamepad.current != null && Gamepad.current.buttonNorth.wasPressedThisFrame && Gamepad.current.leftStick.y.ReadValue() > 0.5f)
        {

            Tackle();
            ResetCombo();

        }
    }

    public void OnStickMoved(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 stickValue = context.ReadValue<Vector2>();
            timer = comboTimer; // Reset timer
        }
    }

    private IEnumerator TackleCoroutine()
    {
        animator.SetTrigger("IsTackle");
        playerMovement.moveSpeed -= 5f;
        float originalY = transform.position.y;
        float targetY = 0.1f;

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Vector3 newPosition = new Vector3(transform.position.x, Mathf.Lerp(originalY, targetY, elapsedTime), transform.position.z);
            transform.position = newPosition;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        playerMovement.moveSpeed += 5f;
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            Vector3 newPosition = new Vector3(transform.position.x, Mathf.Lerp(targetY, originalY, elapsedTime), transform.position.z);
            transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void Tackle()
    {


        StartCoroutine(TackleCoroutine());
    }



    private void ResetCombo()
    {
        timer = 0;
    }
}