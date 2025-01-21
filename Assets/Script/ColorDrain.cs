using System.Collections;
using UnityEngine;

public class ColorDrain : MonoBehaviour
{
    [Header("Sprites to Change")]
    public SpriteRenderer[] sprites; // 用于存储4个SpriteRenderer

    [Header("Settings")]
    public float colorChangeDuration = 5f; // 渐变持续时间

    private bool isDraining = false;
    private Color targetColor = Color.white;
    private Coroutine colorChangeCoroutine;

    private void OnTriggerStay(Collider collision) // 适配3D环境
    {
        if (collision.CompareTag("drain") && Input.GetKey(KeyCode.E))
        {
            if (!isDraining)
            {
                isDraining = true;

                // 从drain物体获取颜色值
                SpriteRenderer drainRenderer = collision.GetComponent<SpriteRenderer>();
                if (drainRenderer != null)
                {
                    targetColor = drainRenderer.color;
                }

                // 找到一个白色的Sprite进行颜色变化
                foreach (var sprite in sprites)
                {
                    if (sprite.color == Color.white)
                    {
                        if (colorChangeCoroutine != null)
                        {
                            StopCoroutine(colorChangeCoroutine);
                        }

                        colorChangeCoroutine = StartCoroutine(ChangeColor(sprite));
                        break;
                    }
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (isDraining)
            {
                isDraining = false;

                if (colorChangeCoroutine != null)
                {
                    StopCoroutine(colorChangeCoroutine);
                }
            }
        }
    }

    private IEnumerator ChangeColor(SpriteRenderer sprite)
    {
        Color initialColor = sprite.color;
        float elapsedTime = 0f;

        while (elapsedTime < colorChangeDuration && isDraining)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / colorChangeDuration;
            sprite.color = Color.Lerp(initialColor, targetColor, t);
            yield return null;
        }

        if (isDraining)
        {
            sprite.color = targetColor;
        }
    }
}
