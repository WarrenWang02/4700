using System.Collections;
using UnityEngine;

public class ColorDrain : MonoBehaviour
{
    [Header("Sprites to Change")]
    public SpriteRenderer[] sprites; // 用于存储4个SpriteRenderer

    [Header("Settings")]
    public float colorChangeDuration = 5f; // 渐变持续时间

    private bool isDraining = false;

    private void OnTriggerStay(Collider collision)
    {
        DrainableObject drainable = collision.GetComponent<DrainableObject>();
        if (drainable != null && Input.GetKey(KeyCode.E))
        {
            if (!isDraining)
            {
                isDraining = true;
                drainable.StartDraining(this);

                // 找到第一个空闲（单位值为0）的 Sprite
                foreach (var sprite in sprites)
                {
                    var spriteUnit = sprite.GetComponent<SpriteUnit>();
                    if (spriteUnit != null && spriteUnit.unitValue == 0) // 检查单位值是否为0
                    {
                        StartCoroutine(ChangeColorGradually(spriteUnit, drainable.initialColor, drainable));
                        return; // 找到一个空闲 Sprite 后立即退出
                    }
                }

                // 如果没有可用的 Sprite，停止 DrainableObject 的吸取
                drainable.StopDraining();
                isDraining = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            if (isDraining)
            {
                isDraining = false;
                drainable?.StopDraining();
            }
        }
    }

    private IEnumerator ChangeColorGradually(SpriteUnit spriteUnit, Color targetColor, DrainableObject drainable)
    {
        Color initialColor = spriteUnit.unitColor;
        float elapsedTime = 0f;

        while (elapsedTime < colorChangeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / colorChangeDuration;
            spriteUnit.SetUnitValue(Color.Lerp(initialColor, targetColor, t), Mathf.Clamp(spriteUnit.unitValue + 1, 0, 5));
            yield return null;
        }

        // 最终设置颜色和单位值
        spriteUnit.SetUnitValue(targetColor, 5);

        // 允许下一次吸取
        isDraining = false;

        // 停止当前的 DrainableObject 吸取
        drainable.StopDraining();
    }
}
