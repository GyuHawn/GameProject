using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public PlayerMovement player;

    public int damage; // ������

    public int maxHealth; // �ִ� ü��
    public int currentHealth; // ���� ü��
   
    void Start()
    {
        if(player == null)
        {
            player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }

        currentHealth = maxHealth;
    }

    
    void Update()
    {
        Die(); // ���
    }

    
    void Die() // ���
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= player.damage;
        }
    }
}
