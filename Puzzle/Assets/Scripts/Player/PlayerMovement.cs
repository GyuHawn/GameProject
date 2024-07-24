using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float mouseSensitivity;
    public float jumpPower;

    private float hAxis;
    private float vAxis;
    private float rotationY;
    private bool isGrounded;

    private Rigidbody rb;

    public Transform gunPos;
    public Vector3 gunOffset;

    public bool isCursorVisible = true; // ���콺 Ŀ�� Ȱ��ȭ ����
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // ȸ���� ���� ������ ó������ �ʵ��� ����

        moveSpeed = 15f;
        mouseSensitivity = 300f;
        jumpPower = 7f;

        gunOffset = new Vector3(0, 1.2f, 0);
    }

    void Update()
    {
        GetInput();
        Rotate();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        UpdateGunPosition();

        OffCursorVisibility("`"); // ���콺 Ŀ�� ��/Ȱ��ȭ
    }

    void FixedUpdate()
    {
        Move();
    }

    void UpdateGunPosition()
    {
        gunPos.position = gameObject.transform.position + gunOffset; // �÷��̾� ��ġ�� ������ ����
    }

    void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    void Move()
    {
        Vector3 moveDirection = transform.right * hAxis + transform.forward * vAxis;
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);

        gunPos.localRotation = Camera.main.transform.localRotation;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OffCursorVisibility(string KeyNum) // Ŀ�� ��Ȱ��ȭ
    {
        if (Input.GetButtonDown(KeyNum)) // Ű �Է� ����
        {
            isCursorVisible = !isCursorVisible; // ���콺 ������ Ȱ��ȭ ����
            Cursor.visible = isCursorVisible; // ���콺 ������ Ȱ��ȭ ���� ����
            Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked; // ���콺 ������ ��� ���� ����
        }
    }
}
