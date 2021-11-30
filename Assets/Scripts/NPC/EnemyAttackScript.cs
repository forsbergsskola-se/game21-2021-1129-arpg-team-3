using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    private bool CanAttack = true;
    public float AttackDelay = 2;

    public void TryAttack(Animator Anim)
    {
        if (CanAttack)
        {
            AttackOnce(Anim);
        }
    }
    
    public void AttackOnce(Animator Anim)
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                Anim.SetTrigger("AttackOne");
                break;
            case 1:
                Anim.SetTrigger("AttackTwo");
                break;
            case 2:
                Anim.SetTrigger("AttackThree");
                break;
        }

        StartCoroutine(DelayAttack());
    }

    public IEnumerator DelayAttack()
    {
        CanAttack = false;
        yield return new WaitForSeconds(AttackDelay);
        CanAttack = true;
    }
}
