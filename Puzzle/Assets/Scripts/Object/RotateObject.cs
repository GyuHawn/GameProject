using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public ActivateGem gem;

    public float rotateDuration; // ȸ�� �ð�
    public float rotateNum; // ȸ�� ����
    public bool x; // x�� ȸ�� ����
    public bool y; // y�� ȸ�� ����
    public bool z; // z�� ȸ�� ����

    public float waitTime; // ȸ�� �� ��� �ð�

    private bool rotating = false;

    void Update()
    {
        if (gem != null && gem.activate && !rotating)
        {
            rotating = true;
            StartCoroutine(RotateAndWait());
        }
    }

    IEnumerator RotateAndWait()
    {
        float elapsedTime = 0f;

        while (elapsedTime < rotateDuration)
        {
            if (x) // x�������� ȸ��
            {
                transform.Rotate(Vector3.right * rotateNum * Time.deltaTime / rotateDuration);
            }
            if (y) // y�������� ȸ��
            {
                transform.Rotate(Vector3.up * rotateNum * Time.deltaTime / rotateDuration);
            }
            if (z) // z�������� ȸ��
            {
                transform.Rotate(Vector3.forward * rotateNum * Time.deltaTime / rotateDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ȸ�� �� ��� �ð�
        yield return new WaitForSeconds(waitTime);

        rotating = false;
    }
}
