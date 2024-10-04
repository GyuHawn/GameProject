using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstBossController : MonoBehaviour
{
    public FirstBossAnimationController animationController;
    public FirstBossStage firstBossStage;
    private PlayerMovement playerMovement;

    public GameObject player; // �÷��̾�

    public float speed; // �̵��ӵ�
    public float rotateSpeed; // ȸ���ӵ�
    public float rotateAttackSpeed; // ȸ�� ���ݼӵ�
    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��
    public Image healthBar; // ü�¹�
    public bool dying; // �������

    public bool attackPose; // ���� �غ�

    public int attackNum; // ���� Ƚ��
    public GameObject attackBullet; // �����Ѿ�
    public Transform bulletPosition; // �Ѿ� �߻� ��ġ
    public int bulletSpeed; // �Ѿ˼ӵ�

    public GameObject shield; // ��ȣ��

    public GameObject flooringEffect; // �ٴ� ���� ����Ʈ
    public GameObject explosionEffect; // ���� ����Ʈ

    public GameObject dropCube; // ��ų ť��

    public bool watching; // �÷��̾� �ֽ� ����
    public bool bossCenterPosition; // ���� ��ġ���� - false : �ʱ���ġ(firstBossStage.bossMapPosition[7]), true : �߾���ġ(firstBossStage.bossMapPosition[5])

    public GameObject clearItem; // Ŭ���� ����

    private void Awake()
    {
        animationController = GetComponent<FirstBossAnimationController>();
    }

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player");
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        speed = 10;
        rotateSpeed = 10;
        bulletSpeed = 30;
        
        maxHealth = 100;
        currentHealth = maxHealth;

        watching = true;
    }

    private void Update()
    {
        if (!dying) // ��� x
        {
            if (watching)
            {
                WatchPlayer(); // �÷��̾� �ֽ�
            }

            HealthUpdate(); // ü�¹� ������Ʈ

            if (currentHealth <= 0)
            {
                Die(); // ���
            }
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
        bossCenterPosition = true;
        animationController.Walk(); // �̵� �ִϸ��̼�
        StartCoroutine(MoveToPosition(4));
    }

    // (��ġ�̵� 2)
    void PositionReset() // �ʱ� ��ġ�� �̵�
    {
        bossCenterPosition = false;
        animationController.Walk(); // �̵� �ִϸ��̼�
        StartCoroutine(MoveToPosition(7));
    }
    
    IEnumerator MoveToPosition(int position)
    {
        // ���� ��ġ�� �̵�
        while (Vector3.Distance(transform.position, firstBossStage.bossMapPositions[position].position) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, firstBossStage.bossMapPositions[position].position, speed * Time.deltaTime);
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

        animationController.Halt();
        shield.SetActive(false);
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

        // �÷��̾� ��ġ ã�� (Y�� ����)
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        // �̵� �� ȸ��
        while (Vector3.Distance(transform.position, targetPosition) > 1f)
        {
            // Y�� ���� ȸ��
            transform.Rotate(0, rotateAttackSpeed * Time.deltaTime, 0);

            // �÷��̾� ��ġ�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, (speed * 2.5f) * Time.deltaTime);
            yield return null;
        }

        int bossPosition = bossCenterPosition ? 4 : 7; 

        // ���� ��ġ�� ���ư���
        Vector3 originalPosition = new Vector3(firstBossStage.bossMapPositions[bossPosition].position.x, transform.position.y, firstBossStage.bossMapPositions[bossPosition].position.z);
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            // ���ư��鼭 ��� ȸ��
            transform.Rotate(0, rotateAttackSpeed * Time.deltaTime, 0);
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, (speed * 2.5f) * Time.deltaTime);
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
        animationController.Halt(); // ��ũ���� ����
        watching = true;
    }

    // (������ ����)
    void Throw() // ������
    {
        animationController.Throw();

        ThrowAttack();
    }

    void ThrowAttack() // ������ ����
    {
        int bossPosition = bossCenterPosition ? 4 : 7; // ������ ���� ��ġ �ε���

        // SkillPositions �迭�� ��ȸ�ϸ� ť�� ����
        for (int i = 0; i < firstBossStage.SkillPositions.Length; i++)
        {
            if (i != bossPosition) // ������ ��ġ�� �ٸ� ��ġ���� ť�� ����
            {
                Vector3 spawnPosition = firstBossStage.SkillPositions[i].position; // ť�� ���� ��ġ
                GameObject cube = Instantiate(dropCube, spawnPosition, Quaternion.identity); // ť�� �ν��Ͻ�ȭ

                Destroy(cube, 12);
            }
        }
    }

    // �� �Ϲ� �ڼ� ------------ �� �����ڼ�

    // (����)
    void AttackPose() // ���� �ڼ�
    {      
        animationController.AttackPose();
        attackPose = true;

        //int num = Random.Range(0, 3);
        int num = 2;
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
        animationController.Halt(); // ���� ��� ����
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

        int bossPosition = bossCenterPosition ? 4 : 7;

        // 4���� �ߺ����� �ʴ� ��ġ�� ����, ������ġ ���� (���� [7])
        while (selectedPositions.Count < 4)
        {
            int randomIndex = Random.Range(0, firstBossStage.bossMapPositions.Length);
            if (randomIndex != bossPosition)
            {
                selectedPositions.Add(randomIndex);
            }
        }

        // ���õ� 4���� ��ġ�� �ٴ����� ����
        foreach (int positionIndex in selectedPositions)
        {
            Vector3 effectPosition = new Vector3(firstBossStage.bossMapPositions[positionIndex].position.x, firstBossStage.bossMapPositions[positionIndex].position.y + 0.2f, firstBossStage.bossMapPositions[positionIndex].position.z);
            GameObject flooring = Instantiate(flooringEffect, effectPosition, Quaternion.identity);
            Destroy(flooring, 7);
        }

        StartCoroutine(MoveSkillWalls(0)); // ��ų�� �̵�
        yield return new WaitForSeconds(6f);

        // ���õ� 4���� ��ġ�� ���� ����
        foreach (int positionIndex in selectedPositions)
        {
            Vector3 effectPosition = new Vector3(firstBossStage.bossMapPositions[positionIndex].position.x, firstBossStage.bossMapPositions[positionIndex].position.y + 0.2f, firstBossStage.bossMapPositions[positionIndex].position.z);
            GameObject explosion = Instantiate(explosionEffect, effectPosition, Quaternion.identity);
            Destroy(explosion, 1);
        }

        StartCoroutine(MoveSkillWalls(-20)); // ��ų�� ����

        animationController.Halt(); // ���� ��� ����
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
            while (Mathf.Abs(skillWall.transform.position.y - targetY) > 1f) // ���� ���
            {
                skillWall.transform.position = Vector3.MoveTowards(skillWall.transform.position, targetPosition, Time.deltaTime * 200f);
                yield return null;
            }
        }
    }

    



    void Die() // ���
    {
        dying = true; // ���ó��

        animationController.Die();

        StartCoroutine(DestroyBoss());
    }

    IEnumerator DestroyBoss()
    {
        yield return new WaitForSeconds(4f);

        clearItem.SetActive(true);

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    // (�ǰ�)
    private void OnTriggerEnter(Collider other)
    { 
        string[] collisionBullet = new string[] { "Bullet", "Expansion", "Penetrate" };

        if (System.Array.Exists(collisionBullet, tag => tag == other.gameObject.tag))
        {
            currentHealth -= playerMovement.damage;
        }
    }

    void HealthUpdate() // ü�¹� ������Ʈ
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
    }
}

    
