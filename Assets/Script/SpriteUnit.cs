using UnityEngine;

public class SpriteUnit : MonoBehaviour
{
    public Color unitColor = Color.white; // 当前颜色
    public int unitValue = 0; // 当前单位值，最大为5

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    public void SetUnitValue(Color color, int value)
    {
        unitColor = color;
        unitValue = Mathf.Clamp(value, 0, 5);
        UpdateColor();
    }

    public void RemoveUnitValue(int amount)
    {
        unitValue = Mathf.Max(0, unitValue - amount);
        if (unitValue == 0)
        {
            unitColor = Color.white; // 恢复为白色
        }
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (spriteRenderer != null)
        {
            float intensity = unitValue / 5f;
            spriteRenderer.color = Color.Lerp(Color.white, unitColor, intensity);
        }
    }
}
