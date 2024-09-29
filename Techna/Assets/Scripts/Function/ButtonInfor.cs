using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfor : MonoBehaviour
{
    public ButtonsController controller;
    public bool trueButton; // ��¥ ��ư
    public bool currentStatus; // ���� ����

    public Material[] materials; // 0: False, 1: True

    public new Renderer renderer; // Material�� �����ϱ� ���� Renderer

    private void Awake()
    {
        controller = GetComponentInParent<ButtonsController>();
        renderer = GetComponent<Renderer>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Expansion")) // �Ѿ� �浹�� ���� ����
        {
            if(controller != null) 
            {
                controller.currentCheckCount--;
            }

            currentStatus = true;
            renderer.material = materials[1];
        }
    }
}
