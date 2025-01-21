using System.Collections;
using UnityEngine;

public class DrainableObject : MonoBehaviour
{
    [Header("Drain Settings")]
    public Color initialColor = Color.red; // 初始颜色
    public float drainDuration = 5f; // 颜色被抽取至白色的时间

    private SpriteRenderer spriteRenderer;
    private bool isDraining = false;
    private Coroutine drainCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = initialColor; // 初始化物体颜色
        }
    }

    public void StartDraining(ColorDrain colorDrainScript)
    {
        if (!isDraining && spriteRenderer != null)
        {
            isDraining = true;
            if (drainCoroutine != null)
            {
                StopCoroutine(drainCoroutine);
            }
            drainCoroutine = StartCoroutine(DrainToWhite());
        }
    }

    public void StopDraining()
    {
        if (isDraining)
        {
            isDraining = false;
            if (drainCoroutine != null)
            {
                StopCoroutine(drainCoroutine);
            }
        }
    }

    private IEnumerator DrainToWhite()
    {
        Color currentColor = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < drainDuration && isDraining)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / drainDuration;
            spriteRenderer.color = Color.Lerp(currentColor, Color.white, t);
            yield return null;
        }

        if (isDraining)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
