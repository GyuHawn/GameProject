using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunction : MonoBehaviour
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
            foreach (var m_Obj in movingObj)
            {
                MovingObject obj = m_Obj.GetComponent<MovingObject>();
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
        // �Ѿ˿� �浹�� 3�ʰ� �۵�
        if (collision.gameObject.CompareTag("Bullet"))
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
