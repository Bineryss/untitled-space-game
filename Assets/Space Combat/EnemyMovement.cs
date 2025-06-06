using System.Collections;
using UnityEngine;

public class TopEnemyController : MonoBehaviour, IMovementPattern
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float patrolPointRadius = 2f; // How close to get before new point
    public float spawnOffset = 1f; // Spawn above screen

    [Header("References")]
    public Rigidbody2D rb;
    private Vector2 currentPatrolPoint;

    // Screen bounds for top half
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    void CalculateTopHalfBounds()
    {
        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Top half bounds (screen center to top)
        minX = -camWidth;
        maxX = camWidth;
        minY = 0; // Screen center
        maxY = camHeight;
    }

    void SetNewRandomPatrolPoint()
    {
        currentPatrolPoint = new Vector2(
            Random.Range(minX + 1f, maxX - 1f), // Keep within X bounds
            Random.Range(minY + 1f, maxY - 1f)  // Keep within top half Y
        );
    }

    private IEnumerator Move()
    {
        while (true)
        {

            Vector2 direction = (currentPatrolPoint - rb.position).normalized;
            rb.linearVelocity = direction * moveSpeed;

            // Check distance to patrol point
            if (Vector2.Distance(rb.position, currentPatrolPoint) < patrolPointRadius)
            {
                SetNewRandomPatrolPoint();
            }
            yield return new WaitForFixedUpdate();
        }
    }

    void OnDrawGizmos()
    {
        // Visualize patrol area in Scene view
        Gizmos.color = Color.yellow;
        Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, 0);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 0);
        Gizmos.DrawWireCube(center, size);
    }

    public void StartMovement(float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = speed;
        CalculateTopHalfBounds();
        SetNewRandomPatrolPoint();

        StartCoroutine(Move());
    }
}
