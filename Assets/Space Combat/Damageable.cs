using UnityEngine;

public interface Damageable
{
    public void TakeDamage(int ammount);
    public Target GetTargetType();
}
