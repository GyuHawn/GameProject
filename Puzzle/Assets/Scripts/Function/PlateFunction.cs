using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateFunction : MonoBehaviour
{
    public GameObject[] movingObj; // �̵��ϴ� ������Ʈ
    public GameObject[] rotateObj; // ȸ���ϴ� ������Ʈ

    public bool activate; // Ȱ��ȭ

    private void Update()
    {
        if (activate) // Ȱ��ȭ �� �۵�
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

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� 3�ʰ� �۵�
        if (collision.gameObject.CompareTag("GrabObject"))
        {
            StartCoroutine(OnButton(3f));
        }
    }
    IEnumerator OnButton(float time)
    {
        activate = !activate;
        yield return new WaitForSeconds(time);
        activate = !activate;
    }
}
