using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageableOR
{
    void TakeDamage(int damage);

    Transform GetTransform();
}
