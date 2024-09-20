using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public Transform grabbedObject; // ���� ��� �ִ� ��ü
    private Vector3 lastValidPosition; // ������ ��ȿ�� ��ġ ����
    public Transform holdPosition; // ��ü�� ���� ��ġ
    public bool grab; // ��Ҵ��� ����
    public float grabRange; // ���� �� �ִ� �ִ� �Ÿ�
    public float throwForce; // ���� �� ��

    void Start()
    {
        grab = false;
        grabRange = 8.0f;
        throwForce = 20f;
    }

    void Update()
    {
        if (Input.GetButtonDown("Grab")) // FŰ�� ���� ��� (�⺻ FŰ�� ����) 
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

        if (Input.GetButtonDown("Throw") && grab)  // ���� ���¿��� EŰ�� ���� ������ (�⺻ EŰ�� ����)
        {
            TryThrowObject(); // ������ �õ�
        }

        if (grab && grabbedObject != null) // ��ü�� ��� ���� ��
        {
            UpdateGrabObjectPosition(); // ���� ��ü ��ġ ������Ʈ
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
                lastValidPosition = grabbedObject.position; // �ʱ� ��ġ ����
                grabbedObject.position = holdPosition.position; // ��ü ��ġ ����

                // ȸ�� ����
                Rigidbody grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
                if (grabbedRigidbody != null)
                {
                    grabbedRigidbody.freezeRotation = true; // ȸ�� ����
                }

                grab = true; // ���� ���·� ����
            }
        }

        // ���� ������ ���԰� 2 �̻��� �� �ٷ� ����
        if (grabbedObject != null)
        {
            CheckObjectInfor cube = grabbedObject.GetComponent<CheckObjectInfor>();
            if (cube.weight > 2)
            {
                ReleaseObject();
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
            }

            grabbedObject = null; // ���� ��ü �ʱ�ȭ
            grab = false; // ���� ���� ���·� ����
        }
    }

    void TryThrowObject() // ������ �õ�
    {
        if (grabbedObject != null) // ���� ��ü�� ���� ��
        {
            CheckObjectInfor cube = grabbedObject.GetComponent<CheckObjectInfor>();

            // �浹 ���̸� ������ ����
            if (!cube.colliding)
            {
                ThrowObject(); // ������
            }
        }
    }

    void ThrowObject() // ���� ������ ����
    {
        Rigidbody grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
        if (grabbedRigidbody != null)
        {
            grabbedRigidbody.freezeRotation = false; // ������ ���� ȸ�� ���� ����
            grabbedRigidbody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse); // ������
        }

        grabbedObject = null; // ���� ��ü �ʱ�ȭ
        grab = false; // ���� ���� ���·� ����
    }

    void UpdateGrabObjectPosition() // ��ü ��ġ ������Ʈ
    {
        if (grabbedObject != null)
        {
            CheckObjectInfor cube = grabbedObject.GetComponent<CheckObjectInfor>();

            // �浹 ���ο� ���� ��ġ ������Ʈ
            if (!cube.colliding) // �浹���� �ʴ� ���
            {
                lastValidPosition = grabbedObject.position; // ������ ��ȿ�� ��ġ ����
                grabbedObject.position = holdPosition.position; // ��ü ��ġ ������Ʈ
            }
            else // �浹 ���� ���
            {
                grabbedObject.position = lastValidPosition; // ������ ��ȿ�� ��ġ�� �ǵ���
                cube.colliding = false; // colliding�� false�� ����
            }
        }
    }
}
