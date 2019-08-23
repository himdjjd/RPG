using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private Transform[] exitPoints;

    public void Shoot(int exitIndex)
    {
        SpellScript s = Instantiate(arrowPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();

        s.Initialize(MyTarget.MyHitbox, damage, this);
    }

}
