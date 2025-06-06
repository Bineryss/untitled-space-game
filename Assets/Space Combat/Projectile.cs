using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private Target target;
    public Target Target => target;
    public void Initialize(int dmg, Target target)
    {
        damage = dmg;
        this.target = target;
    }

    void OnCollisionEnter(Collision col)
    {
        Damageable target = col.collider.GetComponent<Damageable>();
        Debug.Log($"target: {target}");
        if (!target.GetTargetType().Equals(this.target)) return;

        target?.TakeDamage(damage);
        Destroy(gameObject); //TODO add object pooling
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);//TODO add object pooling
    }

}
