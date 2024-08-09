using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateFunction : MonoBehaviour
{
    public GameObject[] movingObj; // �̵��ϴ� ������Ʈ
    public GameObject[] rotateObj; // ȸ���ϴ� ������Ʈ

    public GameObject functionObj; // ������ ������Ʈ

    public bool activate; // Ȱ��ȭ

    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged; // �̺�Ʈ ȣ��

    private void Update()
    {
        // �̵�, ȸ���� ������Ʈ�� �ְ� Ȱ��ȭ ��
        if ((movingObj != null || rotateObj != null) && activate)
        {
            OnActivate(); // �̵�, ȸ��
        }
    }

    void OnActivate() // �̵�, ȸ��
    {
        if (movingObj != null)
        {
            for (int i = 0; i < movingObj.Length; i++) // ��� ������Ʈ �̵�
            {
                MovingObject obj = movingObj[i].GetComponent<MovingObject>();
                obj.MoveObject();
            }
        }

        if (rotateObj != null)
        {
            foreach (var r_Obj in rotateObj) // ��� ������Ʈ ȸ��
            {
                // RotateObject obj = r_Obj.GetComponent<RotateObject>();
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("GrabObject"))
        {
            activate = true;
            activationChanged?.Invoke(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("GrabObject"))
        {
            activate = false;
            activationChanged?.Invoke(false);
        }
    }
}
