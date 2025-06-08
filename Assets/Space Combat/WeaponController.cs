using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;

    private WeaponMount[] mounts;
    private Target target;
    private bool isFiring;

    private WaitForSeconds wait;
    private WaitForSeconds salvoDelay;
    void Awake()
    {
        mounts = GetComponentsInChildren<WeaponMount>();
    }

    public void Configure(WeaponData data, Target target)
    {
        weaponData = data;
        this.target = target;
        float modifyer = gameObject.tag.Equals(Target.ENEMY.ToString()) ? data.FireRateDelay : 1;
        wait = new(1f / (weaponData.FireRate * modifyer) + weaponData.SalvoDelay);

        salvoDelay = new(weaponData.SalvoDelay);

        if (!isFiring) StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        isFiring = true;
        while (true)
        {
            foreach (var mount in mounts)
            {
                StartCoroutine(ShootSalvo(mount.FirePoint));
            }
            yield return wait;
        }
    }

    IEnumerator ShootSalvo(Transform firePoint)
    {
        Debug.Log($"salvo:{weaponData.SalvoAmmount},{weaponData.SalvoDelay}");
        for (int i = 0; i < weaponData.SalvoAmmount; i++)
        {
            SpawnProjectile(firePoint);
            if (i < weaponData.SalvoAmmount - 1)
            {
                yield return salvoDelay;
            }
        }
    }

    void SpawnProjectile(Transform firePoint)
    {
        //TODO: add object pooling!
        var proj = Instantiate(weaponData.ProjectilePrefab,
                                    firePoint.position,
                                    firePoint.rotation);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        Vector3 speed = firePoint.up * weaponData.Speed;
        if (gameObject.tag.Equals(Target.ENEMY.ToString()))
        {
            speed *= weaponData.SpeedDelay;
        }

        rb.linearVelocity = speed;
        proj.GetComponent<Projectile>().Initialize(weaponData.Damage, target);
    }

}
