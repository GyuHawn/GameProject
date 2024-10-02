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
    public float rotateAttackSpeed; // ȸ�� ���ݼӵ�
    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��
    public int damage; // ������

    public bool attackPose; // ���� �غ�

    public int attackNum; // ���� Ƚ��
    public GameObject attackBullet; // �����Ѿ�
    public Transform bulletPosition; // �Ѿ� �߻� ��ġ
    public int bulletSpeed; // �Ѿ˼ӵ�

    public GameObject shield; // ��ȣ��

    public GameObject flooringEffect; // �ٴ� ���� ����Ʈ
    public GameObject explosionEffect; // ���� ����Ʈ

    public bool watching; // �÷��̾� �ֽ� ����
    public bool bossCenterPosition; // ���� ��ġ���� - false : �ʱ���ġ(firstBossStage.bossMapPosition[7]), true : �߾���ġ(firstBossStage.bossMapPosition[5])

    private void Awake()
    {
        animationController = GetComponent<FirstBossAnimationController>();
    }

    private void Start()
    {
        speed = 10;
        rotateSpeed = 10;
        bulletSpeed = 30;
        
        maxHealth = 50;
        currentHealth = maxHealth;
        damage = 10;

        watching = true;

        AttackCrouch();
    }

    private void Update()
    {
        if (watching)
        {
            WatchPlayer(); // �÷��̾� �ֽ�
        }
    }

    void WatchPlayer() // �÷��̾� �ֽ�
    {
        // �÷��̾� ��ġ ���� ���� ���� ����
        Vector3 playerDirection = player.transform.position - transform.position;
        playerDirection.y = 0; // Y�� ȸ�� ����

        // �ε巴�� ȸ��
        Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    // (��ġ�̵� 1)
    void CenterMove() // �߾����� �̵�
    {
        StartCoroutine(MoveToPosition(4));
    }

    // (��ġ�̵� 2)
    void PositionReset() // �ʱ� ��ġ�� �̵�
    {
        StartCoroutine(MoveToPosition(7));
    }
    
    IEnumerator MoveToPosition(int position)
    {
        animationController.Walk(); // �̵� �ִϸ��̼�

        // ���� ��ġ�� �̵�
        while (Vector3.Distance(transform.position, firstBossStage.bossMapPosition[position].position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstBossStage.bossMapPosition[position].position, speed * Time.deltaTime);
            yield return null;
        }

        animationController.Halt();
    }

    // (��ũ���� ���)
    void Crouch() // ��ũ���� (���)
    {
        animationController.Crouch(); // ��ũ���� �ִϸ��̼�

        StartCoroutine(UnCrouch()); // ��ũ���� ����
    }
    
    IEnumerator UnCrouch() // �� ���� ����� ����
    {
        yield return new WaitForSeconds(1);
        shield.SetActive(true); // (��ȣ�� Ȱ��ȭ)
        yield return new WaitForSeconds(5);

        if (animationController.isCrouch) // ��ũ���� �� �Ͻ� ����
        {
            animationController.Crouch();
            shield.SetActive(false);
        }
    }

    // (��ũ���� ����)
    void AttackCrouch() // ��ũ���� (����)
    {
        watching = false;
        StartCoroutine(AttackCrouchRoutine());
    }

    IEnumerator AttackCrouchRoutine() // ��ũ�� �� ȸ���Ͽ� ���� �� ���ư���
    {
        rotateAttackSpeed = 1000f; // ȸ���ӵ� ����
        Quaternion initialRotation = transform.rotation; // �ʱ� ȸ�� �� ����

        animationController.Crouch(); // ��ũ���� �ִϸ��̼� ����
        yield return new WaitForSeconds(2);

        // �̵� �� ȸ��
        while (Vector3.Distance(transform.position, player.transform.position) > 1f)
        {
            // Y�� ������ ���¿��� �̵�
            Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            // Y�� ���� ȸ��
            transform.Rotate(0, rotateAttackSpeed * Time.deltaTime, 0);

            // �÷��̾� ��ġ�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, (speed * 2f) * Time.deltaTime);
            yield return null;
        }

        // ���� ��ġ�� ���ư���
        Vector3 originalPosition = new Vector3(firstBossStage.bossMapPosition[7].position.x, transform.position.y, firstBossStage.bossMapPosition[7].position.z);
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            // ���ư��鼭 ��� ȸ��
            transform.Rotate(0, rotateAttackSpeed * Time.deltaTime, 0);
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, (speed * 2f) * Time.deltaTime);
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
        rotateAttackSpeed = 0f; // ȸ���ӵ� ����
        animationController.Crouch(); // ��ũ���� ����
        watching = true;
    }


    // (����)
    void AttackPose() // ���� �ڼ�
    {      
        animationController.AttackPose();
        attackPose = true;

        //int num = Random.Range(0, 3);
        int num = 1;
        switch (num)
        {
            case 0:
                StartCoroutine(BasicAttackReady());
                break;
            case 1:
                StartCoroutine(QuickAttackReady());
                break;
            case 2:
                StartCoroutine(TauntAttackReady());
                break;
        }
    }

    // (�⺻ ����)
    IEnumerator BasicAttackReady() // ���� �غ�
    {
        yield return new WaitForSeconds(2f);
        attackNum = 3;     

        StartCoroutine(RepeatBulletAttack(1f));
    }

    // (���� ����)
    IEnumerator QuickAttackReady() // ���� ���� �غ�
    {
        yield return new WaitForSeconds(2f);
        attackNum = 10;

        StartCoroutine(RepeatBulletAttack(0.5f));
    }

    IEnumerator RepeatBulletAttack(float time) // �ݺ� �Ѿ� ����
    {
        while (attackNum >= 0)
        {
            BulletAttack(); // ���� ����
            
            yield return new WaitForSeconds(time); // ���
        }
        animationController.AttackPose(); // ���� ��� ����
    }

    void BulletAttack() // �Ѿ� ����
    {
        animationController.Attack();
        attackNum--;
        GameObject bullet = Instantiate(attackBullet, bulletPosition.position, Quaternion.identity);
        Vector3 direction = (player.transform.position - bullet.transform.position).normalized;
        bullet.transform.rotation = Quaternion.LookRotation(direction);
        bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.VelocityChange);
    }

    // (���� ����)
    IEnumerator TauntAttackReady() // ���� �غ�
    {
        yield return new WaitForSeconds(2f);

        animationController.Taunt(); // ���� �ִϸ��̼� ����

        yield return new WaitForSeconds(1f);

        StartCoroutine(CreateFlooring()); // �ٴ� ���� ����
    }

    IEnumerator CreateFlooring() // �ٴ� ���� ����
    {
        HashSet<int> selectedPositions = new HashSet<int>(); // �ٴ� ��ġ ����Ʈ

        // 4���� �ߺ����� �ʴ� ��ġ�� ����, ������ġ ���� (���� [7])
        while (selectedPositions.Count < 4)
        {
            int randomIndex = Random.Range(0, firstBossStage.bossMapPosition.Length);
            if (randomIndex != 7)
            {
                selectedPositions.Add(randomIndex);
            }
        }

        // ���õ� 4���� ��ġ�� �ٴ����� ����
        foreach (int positionIndex in selectedPositions)
        {
            Vector3 effectPosition = new Vector3(firstBossStage.bossMapPosition[positionIndex].position.x, firstBossStage.bossMapPosition[positionIndex].position.y + 0.2f, firstBossStage.bossMapPosition[positionIndex].position.z);
            GameObject flooring = Instantiate(flooringEffect, effectPosition, Quaternion.identity);
            Destroy(flooring, 7);
        }

        StartCoroutine(MoveSkillWalls(0)); // ��ų�� �̵�
        yield return new WaitForSeconds(6f);

        // ���õ� 4���� ��ġ�� ���� ����
        foreach (int positionIndex in selectedPositions)
        {
            Vector3 effectPosition = new Vector3(firstBossStage.bossMapPosition[positionIndex].position.x, firstBossStage.bossMapPosition[positionIndex].position.y + 0.2f, firstBossStage.bossMapPosition[positionIndex].position.z);
            GameObject explosion = Instantiate(explosionEffect, effectPosition, Quaternion.identity);
            Destroy(explosion, 1);
        }

        StartCoroutine(MoveSkillWalls(-20)); // ��ų�� ����

        animationController.AttackPose(); // ���� ��� ����
    }

    // ��ų�� �̵�
    IEnumerator MoveSkillWalls(float targetY)
    {
        yield return new WaitForSeconds(1.5f); // ��� �ð�

        foreach (GameObject skillWall in firstBossStage.SkillWalls)
        {
            if (skillWall == null) continue;

            Vector3 targetPosition = new Vector3(skillWall.transform.position.x, targetY, skillWall.transform.position.z);

            // y ��ǥ���� ������ ������ �̵�
            while (Mathf.Abs(skillWall.transform.position.y - targetY) > 0.01f) // ���� ���
            {
                skillWall.transform.position = Vector3.MoveTowards(skillWall.transform.position, targetPosition, Time.deltaTime * 200f);
                yield return null;
            }
        }
    }

    // (�ǰ�)
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

    
