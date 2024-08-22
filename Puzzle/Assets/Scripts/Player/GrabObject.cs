using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private Transform grabbedObject; // ���� ��� �ִ� ��ü
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
            if (grab) // �������
            {
                ReleaseObject(); // ����
            }
            else 
            {
                TryGrabObject(); // ���
            }
        }

        if (Input.GetButtonDown("Throw") && grab)  // ���� ���¿��� EŰ�� ���� ������ (�⺻ EŰ�� ����) 
        {
            ThrowObject(); // ������
        }

        if (grab && grabbedObject != null) // ��ü�� ��� ���϶�
        {
            UpdateGrabObjectPosition(); // ���� ��ü ��ġ ������Ʈ
        }
    }

    void TryGrabObject() // ���
    {
        RaycastHit hit;
        // ī�޶� �������� ��ü ����
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, grabRange))
        {
            if (hit.collider.CompareTag("GrabObject")) // �±� Ȯ��
            {
                grabbedObject = hit.transform; // ���� ��ü ����
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true; // ������ ����
                grabbedObject.position = holdPosition.position; // ��ü ��ġ ����
                //grabbedObject.parent = holdPosition; // ��ü�� �θ� ����
                grab = true; // ���� ���·� ����
            }
        }
        
        // ���� ������ ���԰� 2�̻��϶� �ٷ� ����
        if (grabbedObject != null)
        {

            CheckCubeInfor cube = grabbedObject.GetComponent<CheckCubeInfor>();
            if (cube.weight > 2)
            {
                ReleaseObject();
            }
        }

    }

    void ReleaseObject() // ����
    {
        if (grabbedObject != null) // ������� ������
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false; // ������ �簳
            //grabbedObject.parent = null; // ��ü�� �θ� ���� ����
            grabbedObject = null; // ���� ��ü �ʱ�ȭ
            grab = false; // ���� ���� ���·� ����
        }
    }

    void ThrowObject() // ������
    {
        if (grabbedObject != null) // ���� ��ü�� ������
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false; // ������ ���
            //grabbedObject.parent = null; // �θ� ���� ����
            // �������� ������
            grabbedObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
            grabbedObject = null; // ���� ��ü �ʱ�ȭ
            grab = false; // �������� ���·� ����
        }
    }

    void UpdateGrabObjectPosition()
    {
        if (grabbedObject != null)
        {
            grabbedObject.position = holdPosition.position; // ��ü�� ��ġ ��� ������Ʈ
        }
    }  
}
