using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // �̵��ӵ�
    public float mouseSensitivity; // ���콺 ����
    public float jumpPower; // ������

    // ����
    public bool moving; // �̵� ���� ����
    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��
    public Image healthBar; // ü�¹�
    public TMP_Text healthText; // ü�� �ؽ�Ʈ
    public int currentStage; // ���� ��������

    public int damage; // ������

    public bool hit; // �ǰ� ���� ����

    // Ű�Է�
    private float hAxis;
    private float vAxis;
    private float rotationY;
    private bool isGrounded; // ���� ����
    private Vector3 moveDirection; // �̵� ����
    private Vector3 velocity; // �߷� �� �̵��ӵ� ����

    // ����ġ
    public Transform gunPos;
    public Vector3 gunOffset;

    public Transform movingPlatform; // �̵� ����
    private Vector3 lastPlatformPosition; // ���������� ��ϵ� ������ ��ġ

    // ���� �۵�
    private bool checkLever = false;
    private GameObject currentLever;

    private CharacterController controller;
    private float gravity = -9.81f; // �߷�

    public PlayableDirector pd;
    public GameObject canvasCamera;

    private void Awake()
    {
        // ���� ���� Ÿ�Ӷ��� ����
        PlayableDirectorSetting();
    }

    void Start()
    {
        NullObjectFind(); // ������Ʈ ã��

        controller = GetComponent<CharacterController>();

        // Ÿ�Ӷ��� ���� �̺�Ʈ �߰�
        pd.stopped += OnTimelineStopped;
        StartCinemachine();

        currentStage = 1;

        moveSpeed = 10f;
        mouseSensitivity = 100f;
        jumpPower = 1.5f;

        maxHealth = 100;
        currentHealth = maxHealth;
        damage = 3;

        gunOffset = new Vector3(0, 1.2f, 0);
    }

    void PlayableDirectorSetting()
    {
        // ���� �� �̸� Ȯ��
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Stage1")
        {
            pd.Play();  // Ÿ�Ӷ��� ����
        }
        else
        {
            pd.Stop();  // Ÿ�Ӷ��� ����
        }
    }

    void StartCinemachine()
    {
        moving = false;
        canvasCamera.SetActive(false);
    }

    // Ÿ�Ӷ��� ���� ��
    void OnTimelineStopped(PlayableDirector director)
    {
        if (director == pd)
        {
            moving = true; // �̵� ���� ���·� ����
            canvasCamera.SetActive(true); // ĵ���� ī�޶� Ȱ��ȭ
        }
    }

    void NullObjectFind() // ������Ʈ ã��
    {
        if (canvasCamera == null)
        {
            canvasCamera = GameObject.Find("CanvasCamera");
        }
    }

    void Update()
    {
        NullObjectFind(); // ������Ʈ ã��

        if (moving)
        {
            GetInput(); // Ű�Է�
            Move(); // �̵�
            Rotate(); // ȸ��
            Jump(); // ���� �� �߷� ó��
        }

        UpdateGunPosition(); // �� ��ġ ����

        FunctionLever(); // ���� �۵�

        UPdateInfor(); // �÷��̾� ���� ������Ʈ

        ApplyPlatformMovement(); // �̵� ������ �̵��� ����
    }

    public void ApplyJump(Vector3 jump)
    {
        velocity = jump;
    }

    void ApplyPlatformMovement() // �̵� �������� ���� �� �Բ� �̵�
    {
        if (movingPlatform != null)
        {

            Vector3 platformMovement = movingPlatform.position - lastPlatformPosition;

            controller.Move(platformMovement);

            if (movingPlatform != null)
            {
                lastPlatformPosition = movingPlatform.position;
            }
        }
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
        moveDirection = (transform.right * hAxis + transform.forward * vAxis).normalized;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
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

    void Jump() // ���� �� �߷� ó��
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // �ٴڿ� ������ �ӵ� �ʱ�ȭ
            isGrounded = true;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
            isGrounded = false;

            if (movingPlatform != null)
            {
                movingPlatform = null;
            }
        }

        velocity.y += gravity * Time.deltaTime; // �߷� ����
        controller.Move(velocity * Time.deltaTime); // �߷¿� ���� �̵�

    }

    void FunctionLever()
    {
        if (checkLever && Input.GetButtonDown("Function")) // ���� �۵�
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
        healthText.text = "HP " + currentHealth.ToString() + " / " + maxHealth.ToString(); //

        // ���� ü���� �ִ� ü������ ���� ������ ü�� ���� Fill Amount�� ����
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // �ٴڿ� ������ ���� ����
        if (hit.gameObject.CompareTag("Floor") || hit.gameObject.CompareTag("MovingObject"))
        {
            isGrounded = true;
        }

        // �̵� ���� ���� ������ �̵��� ��ȭ
        if (hit.gameObject.CompareTag("MovingObject"))
        {
            if (movingPlatform == null)
            {
                movingPlatform = hit.transform;
                lastPlatformPosition = movingPlatform.position;
            }
        }
        else
        {
            if (movingPlatform != null)
            {
                movingPlatform = null;
            }
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

        // ����
        if (other.gameObject.CompareTag("Thorn"))
        {
            TrapScript thorn = other.gameObject.GetComponent<TrapScript>();
            currentHealth -= thorn.damage;
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