using System.Collections;
using UnityEngine;

public class MissileMovement : MonoBehaviour
{
    private Projectile projectile;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        projectile = GetComponent<Projectile>();
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(CorrectMovement());
    }

    private Vector3 FindTargetPosition()
    {
        return Vector3.up;
    }

    private IEnumerator CorrectMovement()
    {
        // Debug.Log(projectile.Target);
        yield return new WaitForSeconds(1f);
    }

}
