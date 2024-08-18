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

    private bool rotating; // ȸ�� �� ����
    public bool activate; // �Ϲ� ȸ���� ȸ�� ���� Ȯ��

    public bool gamRotate; // ���� ����
    public bool objRotate; // ���� ȸ��
    public bool objRotateSetting; // ������ ��ġ�� ȸ��

    void Update()
    {
        // ���� ���� ȸ��
        if (gamRotate && gem != null && gem.activate && !rotating)
        {
            rotating = true;
            StartCoroutine(RotateAndWait());
        }
        // ���� ȸ��
        if (objRotate && !rotating && activate)
        {
            rotating = true;
            StartCoroutine(Rotate());
        }
        // ������ ��ġ�� ȸ��
        if (objRotateSetting && !rotating && activate)
        {
            rotating = true;
            StartCoroutine(RotateValueSetting());
        }
    }

    IEnumerator Rotate() // ���� ȸ��
    {
        while (activate)
        {
            if (x) // x�������� ȸ��
            {
                transform.Rotate(Vector3.right * rotateNum * Time.deltaTime);
            }
            if (y) // y�������� ȸ��
            {
                transform.Rotate(Vector3.up * rotateNum * Time.deltaTime);
            }
            if (z) // z�������� ȸ��
            {
                transform.Rotate(Vector3.forward * rotateNum * Time.deltaTime);
            }

            yield return null; // ���� �����ӱ��� ���
        }

        rotating = false;
    }

    IEnumerator RotateAndWait() // ���� ����ŭ ȸ���� ����� �ٽ� ȸ��
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

    IEnumerator RotateValueSetting()
    {
        // ��ǥ ȸ������ ���� ȸ������ ��������� ���ϰų� ���� ������� ����
        Vector3 targetRotation = transform.eulerAngles;

        if (x) // x�� ȸ���� ������ ������ ��������� ����
        {
            targetRotation.x = rotateNum;
        }
        if (y) // y�� ȸ���� ������ ������ ��������� ����
        {
            targetRotation.y = rotateNum;
        }
        if (z) // z�� ȸ���� ������ ������ ��������� ����
        {
            targetRotation.z = rotateNum;
        }

        // ���� ȸ������ ��ǥ ȸ���� ������ ���� ���
        Vector3 startRotation = transform.eulerAngles;
        float elapsedTime = 0f;

        while (elapsedTime < rotateDuration)
        {
            // ���� ������ ���� ������ ��ǥ ȸ�������� ȸ��
            transform.eulerAngles = Vector3.Lerp(startRotation, targetRotation, elapsedTime / rotateDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }

        // ��ǥ ȸ�������� ��Ȯ�� ����
        transform.eulerAngles = targetRotation;

        // ȸ���� �Ϸ�Ǿ����Ƿ� ȸ�� �� ���¸� ����
        rotating = false;
    }
}
