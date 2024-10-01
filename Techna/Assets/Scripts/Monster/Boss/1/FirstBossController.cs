using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossController : MonoBehaviour
{
    public FirstBossAnimationController animationController;
    public FirstBossStage firstBossStage;

    public GameObject player; // �÷��̾�

    public float speed; // �̵��ӵ�
    public float rotateSpeed; // ȸ���ӵ�
    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��

    public GameObject shield; // ��ȣ��

    private void Awake()
    {
        animationController = GetComponent<FirstBossAnimationController>();
    }

    private void Start()
    {
        speed = 10;

        // animationController.Rest();

        AttackPose();
    }

    void CenterMove() // �߾����� �̵�
    {
        StartCoroutine(MoveToPosition(4));
    }

    void PositionReset() // �ʱ� ��ġ�� �̵�
    {
        StartCoroutine(MoveToPosition(7));
    }
    
    IEnumerator MoveToPosition(int position)
    {
        animationController.Walk(); // �̵� �ִϸ��̼�

        // ���� ��ġ�� �̵�
        while (Vector3.Distance(transform.position, firstBossStage.bossMovePosition[position].position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstBossStage.bossMovePosition[position].position, speed * Time.deltaTime);
            yield return null;
        }

        animationController.Halt();
    }

    void Crouch() // ��ũ���� (���)
    {
        animationController.Crouch(); // ��ũ���� �ִϸ��̼�
        shield.SetActive(true); // (��ȣ�� Ȱ��ȭ)

        StartCoroutine(UnCrouch()); // ��ũ���� ����
    }
    
    IEnumerator UnCrouch() // ��ũ���� ����
    {
        yield return new WaitForSeconds(5);

        if (animationController.isCrouch) // ��ũ���� �� �Ͻ� ����
        {
            animationController.Crouch();
            shield.SetActive(false);
        }
    }

    void AttackCrouch() // ��ũ���� (����)
    {
        StartCoroutine(AttackCrouchRoutine());
    }

    IEnumerator AttackCrouchRoutine() // ��ũ���� ȸ���Ͽ� ���� �� ���ư���
    {
        rotateSpeed = 1000f; // ȸ���ӵ� ����
        Quaternion initialRotation = transform.rotation; // �ʱ� ȸ�� �� ����

        animationController.Crouch(); // ��ũ���� �ִϸ��̼� ����
        yield return new WaitForSeconds(2);

        // �̵� �� ȸ��
        while (Vector3.Distance(transform.position, player.transform.position) > 0.1f)
        {
            // Y�� ���� ȸ��
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

            // �÷��̾� ��ġ�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (speed * 1.5f) * Time.deltaTime);
            yield return null;
        }

        // ���� ��ġ�� ���ư���
        Vector3 originalPosition = firstBossStage.bossMovePosition[7].position;
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            // ���ư��鼭 ��� ȸ��
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, (speed * 1.5f) * Time.deltaTime);
            yield return null;
        }

        // ���� �� õõ�� ȸ�� ���� �� ���� ȸ�������� ��Ȯ�� ���߱�
        float decelerationDuration = 2f;
        float decelerationStartTime = Time.time;
        Quaternion finalRotation = initialRotation;

        while (Quaternion.Angle(transform.rotation, finalRotation) > 0.01f)
        {
            float t = (Time.time - decelerationStartTime) / decelerationDuration;
            transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, t);
            yield return null;
        }

        transform.rotation = finalRotation; // ���������� ���� ȸ�� ������ ����

        // ��ũ���� ����
        rotateSpeed = 0f; // ȸ���ӵ� ����
        animationController.Crouch(); // ��ũ���� ����
    }

    void AttackPose() // ���� �ڼ�
    {
        animationController.AttackPose();
    }

    private void OnCollisionEnter(Collision collision)
    {
        string[] collisionBullet = new string[] { "Bullet", "Expansion", "Penetrate" };

        if (System.Array.Exists(collisionBullet, tag => tag == collision.gameObject.tag))
        {
            currentHealth -= 5;

            animationController.TakeDamage(); // �ǰ� (Ȯ���ʿ�)
        }
    }
}

    
