using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [Header("Flash Settings")]
    public Color flashColor = Color.white;
    public int numberOfFlashes = 1;
    public float flashDuration = 0.15f;
    public AnimationCurve flashCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public Target TargetType { get; set; }

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isFlashing = false;


    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int ammount)
    {

        Debug.Log($"taking damage: {ammount}");
        TriggerFlash();
    }

    public void TriggerFlash()
    {
        if (!isFlashing)
        {
            StartCoroutine(SmoothFlashRoutine());
        }
    }

    private IEnumerator SmoothFlashRoutine()
    {
        isFlashing = true;

        for (int i = 0; i < numberOfFlashes; i++)
        {
            // Flash to white
            yield return StartCoroutine(LerpColor(originalColor, flashColor, flashDuration));
            // Back to original
            yield return StartCoroutine(LerpColor(flashColor, originalColor, flashDuration));
        }

        isFlashing = false;
    }

    private IEnumerator LerpColor(Color fromColor, Color toColor, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = flashCurve.Evaluate(elapsedTime / duration);
            spriteRenderer.color = Color.Lerp(fromColor, toColor, t);
            yield return null;
        }

        spriteRenderer.color = toColor;
    }
}
