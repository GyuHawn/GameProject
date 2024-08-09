using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGem : MonoBehaviour
{
    public Material[] gemMaterials; // ����
    public bool onControl; // �ڽ� ���� ����

    private Renderer render;

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void UpdateColor() // ���� ����
    {
        render.material = onControl ? gemMaterials[1] : gemMaterials[0];
    }

    IEnumerator ChangeColor(float changeTime) // ���� ������ �ٽ� ����
    {
        onControl = true;
        UpdateColor();

        yield return new WaitForSeconds(changeTime);

        onControl = false;
        UpdateColor();
    }

    private void OnCollisionEnter(Collision collision) 
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
