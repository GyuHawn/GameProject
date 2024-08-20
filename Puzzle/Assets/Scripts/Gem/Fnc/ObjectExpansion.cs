using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExpansion : MonoBehaviour
{
    public ExpansionConversion gem; // ���� ���� Ȯ��
    public float scaleChangeDuration = 5.0f; // ������ ��ȭ �ð�
    public float freezeDuration = 5.0f; // ������Ʈ ���� �ð�

    private void Start()
    {
        gem = GameObject.Find("Gun").GetComponent<ExpansionConversion>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        string[] checkTags = { "GrabObject" }; // �ʿ�� �߰�

        if (collision.gameObject.CompareTag("GrabObject"))
        {
            CheckCubeInfor check = collision.gameObject.GetComponent<CheckCubeInfor>();

            if (check.expansion)
            {
                // ������Ʈ�� ũ�� ����
                StartCoroutine(HandleCollision(collision.gameObject));
            }
            else { }
        }
    }

    void ScaleOverTime(GameObject obj, Vector3 targetScale, float duration) // ũ�� ����
    {
        Vector3 initialScale = obj.transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
        }

        // ���� ������ ����
        obj.transform.localScale = targetScale;
    }

    private IEnumerator HandleCollision(GameObject hitObject) // ũ�� ������ ������ �� ȸ�� ����, ������Ʈ �ִ� ���� �� Ȯ��
    {
        Rigidbody rb = hitObject.GetComponent<Rigidbody>();

        // ��ġ, ȸ�� ����
        Vector3 originalPosition = hitObject.transform.position;
        Quaternion originalRotation = hitObject.transform.rotation;

        CheckCubeInfor check = hitObject.GetComponent<CheckCubeInfor>();
        // ũ�� ����
        if (gem.plus)
        {
            if (check.currentValue < check.expansValue)
            {
                check.currentValue++;
                ScaleOverTime(hitObject, hitObject.transform.localScale * 2, scaleChangeDuration);
                check.weight = check.weight * 2;
            }
        }
        else
        {
            if (check.currentValue > check.reducedValue)
            {
                check.currentValue--;
                ScaleOverTime(hitObject, hitObject.transform.localScale * 0.5f, scaleChangeDuration);
                check.weight = check.weight * 0.5f;
            }
        }

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

        // ����� ��ġ, ȸ������ ����
        hitObject.transform.position = originalPosition;
        hitObject.transform.rotation = originalRotation;
    }
}
