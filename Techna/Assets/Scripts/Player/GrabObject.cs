using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Input System ���ӽ����̽� �߰�

public class GrabObject : MonoBehaviour
{
    public Transform grabbedObject; // ���� ��� �ִ� ��ü
    public bool grab; // ��Ҵ��� ����
    public float grabRange; // ���� �� �ִ� �ִ� �Ÿ�
    public float throwForce; // ���� �� ��
    private Vector3 holdOffset = new Vector3(0, 4, 0); // ��ü�� ��� ���� ������

    private PlayerInputActions inputActions; // Input Actions ����

    void Awake()
    {
        inputActions = new PlayerInputActions(); // Input Actions �ʱ�ȭ
    }

    void OnEnable()
    {
        inputActions.Enable(); // Input Actions Ȱ��ȭ
        inputActions.PlayerActions.Grab.performed += ctx => ToggleGrab(); // Grab �׼ǰ� �޼��� ����
        inputActions.PlayerActions.Throw.performed += ctx => TryThrowObject(); // Throw �׼ǰ� �޼��� ����
    }

    void OnDisable()
    {
        inputActions.Disable(); // Input Actions ��Ȱ��ȭ
    }

    void Start()
    {
        grab = false;
        grabRange = 8.0f;
        throwForce = 20f;
    }

    void Update()
    {
        if (grab && grabbedObject != null) // ��ü�� ��� ���� ��
        {
            UpdateGrabObjectPosition(); // ���� ��ü ��ġ ������Ʈ
        }
    }

    private void ToggleGrab() // ���/���� ���
    {
        if (grab) // �̹� ��ü�� ����� ��
        {
            ReleaseObject(); // ��ü�� ����
        }
        else
        {
            TryGrabObject(); // ��ü�� ��� �õ�
        }
    }

    void TryGrabObject() // ��� �õ�
    {
        RaycastHit hit;
        // ī�޶� �������� ��ü ����
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, grabRange))
        {
            if (hit.collider.CompareTag("GrabObject")) // �±� Ȯ��
            {
                grabbedObject = hit.transform; // ���� ��ü ����

                CheckObjectInfor obj = grabbedObject.GetComponent<CheckObjectInfor>();

                if (obj.weight >= 2) // ���� ���� �̻� ��� ������
                {
                    grabbedObject = null;
                    return;
                }

                SetObjectPosition(grabbedObject, holdOffset); // ��ü ��ġ ����

                // ȸ�� ����
                Rigidbody grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
                if (grabbedRigidbody != null)
                {
                    grabbedRigidbody.freezeRotation = true; // ȸ�� ����
                    grabbedRigidbody.isKinematic = true; // ���� ȿ�� ��Ȱ��ȭ (���� ����)
                }

                grab = true; // ���� ���·� ����
            }
        }
    }

    void ReleaseObject() // ����
    {
        if (grabbedObject != null) // ��ü�� ��� ���� ��
        {
            Rigidbody grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
            if (grabbedRigidbody != null)
            {
                grabbedRigidbody.freezeRotation = false; // ȸ�� ���� ����
                grabbedRigidbody.isKinematic = false; // ���� ȿ�� �ٽ� Ȱ��ȭ
            }

            grabbedObject = null; // ���� ��ü �ʱ�ȭ
            grab = false; // ���� ���� ���·� ����
        }
    }

    void TryThrowObject() // ������ �õ�
    {
        if (grabbedObject != null) // ���� ��ü�� ���� ��
        {
            ThrowObject(); // ������
        }
    }

    void ThrowObject() // ���� ������ ����
    {
        Rigidbody grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.freezeRotation = false; // ������ ���� ȸ�� ���� ����
            grabbedRigidbody.isKinematic = false; // ���� ȿ�� �ٽ� Ȱ��ȭ
            grabbedRigidbody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse); // ī�޶� �ٶ󺸴� �������� ������
        }

        grabbedObject = null; // ���� ��ü �ʱ�ȭ
        grab = false; // ���� ���� ���·� ����
    }

    void UpdateGrabObjectPosition() // ��ü ��ġ ������Ʈ
    {
        if (grabbedObject != null)
        {
            SetObjectPosition(grabbedObject, holdOffset); // ���� ��ü�� ��ġ�� �׻� ������Ʈ
        }
    }

    void SetObjectPosition(Transform obj, Vector3 offset) // ��ü ��ġ ���� �޼���
    {
        obj.position = gameObject.transform.position + offset;
    }
}
