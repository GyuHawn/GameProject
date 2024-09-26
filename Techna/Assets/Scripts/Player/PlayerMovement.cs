using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
    private bool isGrounded; // ���� ����
    private Vector3 moveDirection; // �̵� ����
    private Vector3 velocity; // �߷� �� �̵��ӵ� ����
    private float rotationY;

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
    public MeshRenderer[] deactivateMesh; // Ÿ�Ӷ����� ��Ȱ��ȭ�� ������Ʈ

    private PlayerInputActions inputActions; // Input Actions ����

    private void Awake()
    {
        inputActions = new PlayerInputActions(); // Input Actions �ʱ�ȭ

        // ���� ���� Ÿ�Ӷ��� ����
        PlayableDirectorSetting();
    }

    private void OnEnable()
    {
        inputActions.Enable(); // Input Actions Ȱ��ȭ
    }

    private void OnDisable()
    {
        inputActions.Disable(); // Input Actions ��Ȱ��ȭ
    }

    void Start()
    {
        NullObjectFind(); // ������Ʈ ã��

        controller = GetComponent<CharacterController>();

        // Ÿ�Ӷ��� ���� �̺�Ʈ �߰�
        pd.stopped += OnTimelineStopped;
        StartCinemachine();

        currentStage = 1;

        moveSpeed = 12f;
        mouseSensitivity = 10f;
        jumpPower = 2f;

        maxHealth = 100;
        currentHealth = maxHealth;
        damage = 3;

        gunOffset = new Vector3(0, 1.2f, 0);

        // Input Actions�� �޼��� ����
        inputActions.PlayerActions.Move.performed += OnMove; // Move �Է� ����
        inputActions.PlayerActions.Look.performed += OnLook; // Look �Է� ����
        inputActions.PlayerActions.Jump.performed += OnJump; // Jump �Է� ����
    }

    void PlayableDirectorSetting()
    {
        // ���� �� �̸� Ȯ��
        string currentScene = SceneManager.GetActiveScene().name;

        // �� �̸��� ���� Ÿ�Ӷ��� �迭���� Ÿ�Ӷ��� ���� �� ���/����
        if (currentScene == "Stage1")
        {
            pd.Play();  // Stage1�� ��� Ÿ�Ӷ��� ����
        }
        else
        {
            pd.Stop();  // �ٸ� �������������� Ÿ�Ӷ��� ����
        }
    }

    void StartCinemachine()
    {
        moving = false;
        canvasCamera.SetActive(false);
    }

    // Ÿ�Ӷ��� ���� �� ȣ��Ǵ� �Լ�
    void OnTimelineStopped(PlayableDirector director)
    {
        if (director == pd)
        {
            moving = true; // �̵� ���� ���·� ����
            canvasCamera.SetActive(true); // ĵ���� ī�޶� Ȱ��ȭ

            // deactivateMesh�� �޽� ������ Ȱ��ȭ
            foreach (MeshRenderer meshRenderer in deactivateMesh)
            {
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = true; // �޽� ������ Ȱ��ȭ
                }
            }
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
            Move(); // �̵�
        }

        UpdateGunPosition(); // �� ��ġ ����
        FunctionLever(); // ���� �۵�
        UPdateInfor(); // �÷��̾� ���� ������Ʈ
        ApplyPlatformMovement(); // �̵� ������ �̵��� ����
    }

    // Input System�� ���� �̵�
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        // �̵� ���� ���
        moveDirection = transform.right * input.x + transform.forward * input.y;

        if (context.canceled) // �Է��� �����Ǹ�
        {
            moveDirection = Vector3.zero; // �̵� ������ 0���� ����
        }
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f); // ���� ȸ�� ����

        transform.Rotate(Vector3.up * mouseX); // �¿� ȸ��
        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f); // ī�޶� ���� ȸ��
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (controller.isGrounded && context.performed)
        {
            velocity.y = Mathf.Sqrt(jumpPower * -2f * gravity);
        }
    }

    public void ApplyJump(Vector3 jump)
    {
        velocity = jump;
    }

    void ApplyPlatformMovement()
    {
        // ���� �̵� ���� ���� �ִٸ�
        if (movingPlatform != null)
        {
            Vector3 platformMovement = movingPlatform.position - lastPlatformPosition;
            controller.Move(platformMovement);
            lastPlatformPosition = movingPlatform.position;
        }
    }

    void UpdateGunPosition() // �� ��ġ ����
    {
        gunPos.position = gameObject.transform.position + gunOffset; // �÷��̾� ��ġ�� ������ ����
    }

    void Move() // �̵�
    {
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
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
            movingPlatform = null;
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
