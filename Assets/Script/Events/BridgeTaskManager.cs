using UnityEngine;

public class BridgeTaskManager : MonoBehaviour
{
    [Header("Drainable Object Target")]
    public DrainableObject targetObject; // The DrainableObject to monitor

    [Header("Objects to Control")]
    public GameObject objectToEnable;  // Object A (default OFF, turns ON when color is close to red)
    public GameObject objectToDisable; // Object B (default ON, turns OFF when color is close to red)

    [Header("Color Detection Settings")]
    public float activationThreshold = 50f; // Threshold for being "close to red"

    private void Start()
    {
        // Ensure initial state: Object A is OFF, Object B is ON
        if (objectToEnable != null) objectToEnable.SetActive(false);
        if (objectToDisable != null) objectToDisable.SetActive(true);
    }

    private void Update()
    {
        if (targetObject == null) return;

        if (IsCloseToRed(targetObject.initialColor, activationThreshold))
        {
            if (objectToEnable != null) objectToEnable.SetActive(true);
            if (objectToDisable != null) objectToDisable.SetActive(false);
        }
        else
        {
            if (objectToEnable != null) objectToEnable.SetActive(false);
            if (objectToDisable != null) objectToDisable.SetActive(true);
        }
    }

    private bool IsCloseToRed(Color color, float threshold)
    {
        float redDistance = Mathf.Sqrt(
            Mathf.Pow(color.r * 255 - 255, 2) +  // Red channel difference
            Mathf.Pow(color.g * 255, 2) +       // Green channel difference
            Mathf.Pow(color.b * 255, 2)         // Blue channel difference
        );

        return redDistance < threshold;
    }
}
