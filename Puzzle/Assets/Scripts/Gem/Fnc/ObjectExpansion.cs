using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExpansion : MonoBehaviour
{
    public ExpansionConversion gun; // ���� ���� Ȯ��
    public float scaleChangeDuration; // ������ ��ȭ �ð�
    public float freezeDuration; // ������Ʈ ���� �ð�

    private void Start()
    {
        gun = GameObject.Find("Gun").GetComponent<ExpansionConversion>();

        scaleChangeDuration = 2f;
        freezeDuration = 3f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ExpansionBullet"))
        {
            CheckCubeInfor cube = gameObject.GetComponent<CheckCubeInfor>();
            if (cube.expansion)
            {
                // ������Ʈ�� ũ�� ����
                HandleCollision();
            }
            else { }
        }
    }

    void HandleCollision() // ũ�� ������ ������ �� ȸ�� ����, ������Ʈ �ִ� ���� �� Ȯ��
    {
        // ��ġ, ȸ�� ����
        Vector3 originalPosition = gameObject.transform.position;
        Quaternion originalRotation = gameObject.transform.rotation;

        CheckCubeInfor check = gameObject.GetComponent<CheckCubeInfor>();
        // ũ�� ����
        if (gun.plus)
        {
            if (check.currentValue < check.expansValue)
            {
                check.currentValue++;
                StartCoroutine(ScaleOverTime(gameObject, gameObject.transform.localScale * 2));
                check.weight = check.weight * 2;
            }
        }
        else
        {
            if (check.currentValue > check.reducedValue)
            {
                check.currentValue--;
                StartCoroutine(ScaleOverTime(gameObject, gameObject.transform.localScale * 0.5f));
                check.weight = check.weight * 0.5f;
            }
        }

        StartCoroutine(FixedPostion()); // ������ ����

        // ����� ��ġ, ȸ������ ����
        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
    }

    IEnumerator ScaleOverTime(GameObject obj, Vector3 targetScale) // ũ�� ����
    {
        Vector3 initialScale = obj.transform.localScale;
        float elapsed = 0f;

        while (elapsed < scaleChangeDuration)
        {
            float progress = elapsed / scaleChangeDuration;
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // ���� ������ ����
        obj.transform.localScale = targetScale;
    }

    IEnumerator FixedPostion() // ������ ����
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        // �����ǰ� ȸ���� ����
        if (rb != null)
        {
            // ���� �ӵ�, ȸ���� ����
            Vector3 originalVelocity = rb.velocity;
            Vector3 originalAngularVelocity = rb.angularVelocity;

            // ������, ȸ�� ���� (�ӵ��� ȸ������ 0���� ����)
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // ���� �ð� ���� ������, ȸ���� ����
            yield return new WaitForSeconds(freezeDuration);

            // ���� �ӵ�, ȸ���� ����
            rb.velocity = originalVelocity;
            rb.angularVelocity = originalAngularVelocity;
        }
        else
        {
            // Kinematic ������ ���
            yield return new WaitForSeconds(freezeDuration);
        }
    }
}
   