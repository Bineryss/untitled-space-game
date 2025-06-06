using System;
using UnityEngine;

public class Health : MonoBehaviour, Damageable
{
    public Target GetTargetType()
    {
        if (Enum.TryParse(gameObject.tag, out Target target))
        {
            return target;
        }
        return Target.OBJECT;
    }

    public void TakeDamage(int ammount)
    {
        Debug.Log($"taking damage: {ammount}");
    }
}
