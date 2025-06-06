using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;

    private WeaponMount[] mounts;
    private Target target;
    private bool isFiring;
    void Awake()
    {
        mounts = GetComponentsInChildren<WeaponMount>();
    }

    public void Configure(WeaponData data, Target target)
    {
        weaponData = data;
        this.target = target;
        if (!isFiring) StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        isFiring = true;
        var wait = new WaitForSeconds(1f / weaponData.FireRate * weaponData.SalvoDelay);
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
        for (int i = 0; i < weaponData.SalvoAmmount; i++)
        {
            SpawnProjectile(firePoint);
            if (i < weaponData.SalvoAmmount - 1)
            {
                yield return new WaitForSeconds(weaponData.SalvoDelay);
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
        rb.linearVelocity = firePoint.up * weaponData.Speed;
        proj.GetComponent<Projectile>().Initialize(weaponData.Damage, target);
    }

}
