using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class FirstBossAnimationController : MonoBehaviour
{
    public Animator character;

    public bool isWalking = false;
    public bool isCrouch = false;
    public bool isAttacking = false;

    public void Walk() // �̵�
    {
        isWalking = !isWalking;
        character.SetBool("isWalking", isWalking);
    }

    public void Crouch() // ��ũ����
    {
        isCrouch = !isCrouch;
        character.SetBool("isCrouch", isCrouch);
    }

    public void Halt() // ����
    {
        character.SetBool("isWalking", false);
        character.SetBool("isAttacking", false);
    }

    public void AttackPose() // ���� ���
    {
        isAttacking = !isAttacking;
        character.SetBool("isAttacking", isAttacking);
    }

    public void Attack() // ����
    {
        character.SetTrigger("attack");
    }

    public void TakeDamage() // �ǰ�
    {
        character.SetTrigger("takeDamage");
    }

    public void Taunt() // ����
    {
        character.SetTrigger("taunt");
    }

    public void Throw() // ������
    {
        character.SetTrigger("throw");
    }

    public void Die() // ���
    {
        character.SetTrigger("die");
    }
}
