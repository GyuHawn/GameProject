using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateFunction : MonoBehaviour
{
    public GameObject[] movingObj; // �̵��ϴ� ������Ʈ
    public GameObject[] rotateObj; // ȸ���ϴ� ������Ʈ

   // public GameObject functionObj; // ������ ������Ʈ
    public GameObject checkObj; // Ȯ���� ������Ʈ

    public bool activate; // Ȱ��ȭ
    public bool checkPlate; // Ư�� ������Ʈ���� Ȱ��ȭ �ǵ��� 
    
    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged; // �̺�Ʈ ȣ��

    private void OnCollisionStay(Collision collision)
    {
        // checkPlate �϶��� �˸��� ������Ʈ���� Ȯ��
        if (checkPlate)
        {
            if (collision.gameObject == checkObj)
            {
                activate = true;
                activationChanged?.Invoke(true);
            }
        }
        else // �ƴҽ� �±� Ȯ��
        {
            if (collision.gameObject.CompareTag("GrabObject"))
            {
                activate = true;
                activationChanged?.Invoke(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (checkPlate)
        {
            if (collision.gameObject == checkObj)
            {
                activate = false;
                activationChanged?.Invoke(false);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("GrabObject"))
            {
                activate = false;
                activationChanged?.Invoke(false);
            }
        }
    }
}
