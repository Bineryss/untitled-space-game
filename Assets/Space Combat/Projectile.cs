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


    void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable target = collision.GetComponent<IDamageable>();
        if (target == null) return;

        Debug.Log($"target: {target.TargetType}-{this.target}; can damage {target.TargetType.Equals(this.target)}");
        if (!target.TargetType.Equals(this.target)) return;

        target?.TakeDamage(damage);
        Destroy(gameObject); //TODO add object pooling

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);//TODO add object pooling
    }

}
