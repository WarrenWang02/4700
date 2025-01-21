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

    public void SetTargetColor(Color color)
    {
        targetColor = color;
    }

    private void OnTriggerStay(Collider collision)
    {
        DrainableObject drainable = collision.GetComponent<DrainableObject>();
        if (drainable != null && Input.GetKey(KeyCode.E))
        {
            if (!isDraining)
            {
                isDraining = true;
                drainable.StartDraining(this);

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
                if (drainable != null)
                {
                    drainable.StopDraining();
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
