using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateFunction : MonoBehaviour
{
    public GameObject[] movingObj; // �̵��ϴ� ������Ʈ
    public GameObject[] rotateObj; // ȸ���ϴ� ������Ʈ

    public GameObject functionObj; // ������ ������Ʈ

    public bool activate; // Ȱ��ȭ

    public delegate void ActivationChangedHandler(bool activate);
    public event ActivationChangedHandler OnActivationChanged;

    private void Update()
    {
        if ((movingObj != null || rotateObj != null) && activate)
        {
            OnActivate();
        }
    }

    void OnActivate() // �̵� or ȸ��
    {
        if (movingObj != null)
        {
            for (int i = 0; i < movingObj.Length; i++)
            {
                MovingObject obj = movingObj[i].GetComponent<MovingObject>();
                obj.MoveObject();
            }
        }

        if (rotateObj != null)
        {
            foreach (var r_Obj in rotateObj)
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
            OnActivationChanged?.Invoke(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("GrabObject"))
        {
            activate = false;
            OnActivationChanged?.Invoke(false);
        }
    }
}
