using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    private bool CanAttack = true;
    public float AttackDelay = 2;

    public void TryAttack(Animator anim)
    {
        if (CanAttack)
        {
            AttackOnce(anim);
        }
    }
    
    public void AttackOnce(Animator anim)
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                anim.SetTrigger("AttackOne");
                break;
            case 1:
                anim.SetTrigger("AttackTwo");
                break;
            case 2:
                anim.SetTrigger("AttackThree");
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
