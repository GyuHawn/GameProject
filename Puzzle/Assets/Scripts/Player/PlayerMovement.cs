using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // �̵��ӵ�
    public float mouseSensitivity; // ���콺 ����
    public float jumpPower; // ������

    // Ű�Է�
    private float hAxis;
    private float vAxis;
    private float rotationY;
    private bool isGrounded; // ���� ����
    
    // ����ġ
    public Transform gunPos; 
    public Vector3 gunOffset;

    public bool isCursorVisible; // ���콺 Ŀ�� Ȱ��ȭ ����

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // ȸ���� ���� ������ ó������ �ʵ��� ����

        moveSpeed = 15f; 
        mouseSensitivity = 300f;
        jumpPower = 7f;

        gunOffset = new Vector3(0, 1.2f, 0);

        isCursorVisible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        GetInput(); // Ű�Է�
        Move(); // �̵�
        Rotate(); // ȸ��

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump(); // ����
        }

        UpdateGunPosition(); // �� ��ġ ����
        OffCursorVisibility("`"); // ���콺 Ŀ�� ��/Ȱ��ȭ
    }

    void UpdateGunPosition() // �� ��ġ ����
    {
        gunPos.position = gameObject.transform.position + gunOffset; // �÷��̾� ��ġ�� ������ ����
    }

    void GetInput() // Ű�Է�
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    void Move() // �̵�
    {
        Vector3 moveDirection = transform.right * hAxis + transform.forward * vAxis;
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
    }

    void Rotate() // ȸ��
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);

        gunPos.localRotation = Camera.main.transform.localRotation;
    }

    void Jump() // ����
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
