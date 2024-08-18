using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGem : MonoBehaviour
{
    public Material[] gemMaterials; // ����
    public bool activate; // �ڽ� ���� ����

    private Renderer render;

    public bool control;
    public bool electricity;
    public float electricityTime;

    public delegate void CheckActivationChange(bool activate);
    public event CheckActivationChange activationChanged; // �̺�Ʈ ȣ��

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void UpdateColor() // ���� ����
    {
        render.material = activate ? gemMaterials[1] : gemMaterials[0];
    }

    public void OnElectricityGem() // ���� �Ӽ� ���� ����
    {
        StartCoroutine(ElectricityUpdateColor(electricityTime));
    }

    IEnumerator ElectricityUpdateColor(float time)
    {
        yield return new WaitForSeconds(time);
        UpdateColor();
    }

    IEnumerator ChangeColor(float changeTime) // ���� ������ �ٽ� ����
    {
        activate = true;
        activationChanged?.Invoke(true);
        UpdateColor();

        yield return new WaitForSeconds(changeTime);

        activate = false;
        activationChanged?.Invoke(false);
        UpdateColor();
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (control) // ���� �Ӽ� ���� ����
        {
            // �浹�� ���� �ð� ����
            if (collision.gameObject.name == "BasicControl")
            {
                StartCoroutine(ChangeColor(3f));
            }
            else if (collision.gameObject.name == "LargeControl")
            {
                StartCoroutine(ChangeColor(5f));
            }
        }
    }

}