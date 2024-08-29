using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // �̵��ӵ�
    public float mouseSensitivity; // ���콺 ����
    public float jumpPower; // ������

    // ����
    public int maxHealth;
    public int currentHealth;
    public Image healthBar;
    public TMP_Text healthText;

    public int damage; // ������

    public bool hit; // �ǰ� ���� ����

    // Ű�Է�
    private float hAxis;
    private float vAxis;
    private float rotationY;
    private bool isGrounded; // ���� ����

    // ����ġ
    public Transform gunPos; 
    public Vector3 gunOffset;

    public bool isCursorVisible; // ���콺 Ŀ�� Ȱ��ȭ ����

    public Transform movingPlatform; // �̵� ����
    private Vector3 lastPlatformPosition; // ���������� ��ϵ� ������ ��ġ

    // ���� �۵�
    private bool checkLever = false;
    private GameObject currentLever;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // ȸ���� ���� ������ ó������ �ʵ��� ����

        //moveSpeed = 10f; 
        moveSpeed = 20f; 
        mouseSensitivity = 100f;
        jumpPower = 6f;

        maxHealth = 100;
        currentHealth = maxHealth;

        damage = 3;

        gunOffset = new Vector3(0, 1.2f, 0);

        isCursorVisible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        GetInput(); // Ű�Է�
        Move(); // �̵�
        Rotate(); // ȸ��
        Jump(); // ����

        UpdateGunPosition(); // �� ��ġ ����
        OffCursorVisibility(); // ���콺 Ŀ�� ��/Ȱ��ȭ

        FunctionLever(); // ���� �۵�

        UPdateInfor(); // �÷��̾� ���� ������Ʈ
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
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FunctionLever()
    {
        if (checkLever && Input.GetButtonDown("lever")) // ���� �۵�
        {
            if (currentLever != null)
            {
                LeverFunction lever = currentLever.GetComponent<LeverFunction>();
                if (lever != null)
                {
                    lever.activate = true;
                }
            }
        }
    }

    void UPdateInfor()
    {
        healthText.text = "HP " + currentHealth.ToString() + " / " + maxHealth.ToString();

        // ���� ü���� �ִ� ü������ ���� ������ ü�� ���� Fill Amount�� ����
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �ٴڿ� ������ ���� ����
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("MovingObject"))
        {
            isGrounded = true;
        }

        // �̵� ���� ���� ������ �̵��� ��ȭ
        if (collision.gameObject.CompareTag("MovingObject"))
        {
            movingPlatform = collision.transform; // �̵� ����
            lastPlatformPosition = movingPlatform.position; // ������ ��ġ ����
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �̵� ���� �浹 ����
        if (collision.gameObject.CompareTag("MovingObject"))
        {
            movingPlatform = null; // ���� ����
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� �浹, ���� �߰�
        if (other.gameObject.CompareTag("Lever"))
        {
            checkLever = true;
            currentLever = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // �ǰ�
        if (other.gameObject.CompareTag("Monster"))
        {
            MonsterController monster = other.gameObject.GetComponent<MonsterController>();
            HitDamage(other.gameObject, monster.damage);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        // ���� �浹 ����, ���� ����
        if (other.gameObject.CompareTag("Lever"))
        {
            checkLever = false;
            currentLever = null;
        }
    }

    private void OffCursorVisibility() // Ŀ�� ��Ȱ��ȭ
    {
        if (Input.GetButtonDown("CursorHide")) // Ű �Է� ����
        {
            isCursorVisible = !isCursorVisible; // ���콺 ������ Ȱ��ȭ ����
            UnityEngine.Cursor.visible = isCursorVisible; // ���콺 ������ Ȱ��ȭ ���� ����
            UnityEngine.Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked; // ���콺 ������ ��� ���� ����
        }
    }

    private void HitDamage(GameObject obj, int damage) // �ǰ�
    {
        if (!hit)
        {
            hit = true;
            currentHealth -= damage;

            StartCoroutine(HitInterval());
        }
    }

    IEnumerator HitInterval() // �ǰ� ����
    {
        yield return new WaitForSeconds(1f);
        hit = false;
    }
}
