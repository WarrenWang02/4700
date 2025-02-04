using System.Collections;
using UnityEngine;

public class ColorPlatformManager : MonoBehaviour
{
    [Header("Drainable Object Targets")]
    public DrainableObject redTarget;
    public DrainableObject greenTarget;
    public DrainableObject blueTarget;
    
    [Header("Platform References")]
    public Transform redPlatform;
    public Transform greenPlatform;
    public Transform bluePlatform;
    
    [Header("Height Settings")]
    public float minHeight = 96f; // Lowest possible height
    public float maxHeight = 102f; // Highest possible height
    public float defaultHeight = 96f; // Default starting height
    
    public float colorThresholdMin = 10f;
    public float colorThresholdMax = 60f;
    public float moveSpeed = 2f; // Speed of platform animation
    
    private void Start()
    {
        // Ensure platforms start at default height
        if (redPlatform != null) redPlatform.localPosition = new Vector3(redPlatform.localPosition.x, defaultHeight, redPlatform.localPosition.z);
        if (greenPlatform != null) greenPlatform.localPosition = new Vector3(greenPlatform.localPosition.x, defaultHeight, greenPlatform.localPosition.z);
        if (bluePlatform != null) bluePlatform.localPosition = new Vector3(bluePlatform.localPosition.x, defaultHeight, bluePlatform.localPosition.z);
    }

    private void Update()
    {
        if (redTarget != null) StartCoroutine(AdjustPlatform(redPlatform, CalculateHeight(redTarget.initialColor, Color.red)));
        if (greenTarget != null) StartCoroutine(AdjustPlatform(greenPlatform, CalculateHeight(greenTarget.initialColor, Color.green)));
        if (blueTarget != null) StartCoroutine(AdjustPlatform(bluePlatform, CalculateHeight(blueTarget.initialColor, Color.blue)));
        // Debug.Log($"Moving red to height: {CalculateHeight(redTarget.initialColor, Color.red)}"); // Debug log
        // AdjustPlatform(redPlatform, 91);//debug
    }

    private float CalculateHeight(Color objectColor, Color referenceColor)
    {
        float colorDistance = Mathf.Sqrt(
            Mathf.Pow(objectColor.r * 255 - referenceColor.r * 255, 2) +
            Mathf.Pow(objectColor.g * 255 - referenceColor.g * 255, 2) +
            Mathf.Pow(objectColor.b * 255 - referenceColor.b * 255, 2)
        );
        
        if (colorDistance >= colorThresholdMax) {
            //Debug.Log("too difference " + defaultHeight); // Debug message
            return defaultHeight;
        }
        
        if (colorDistance <= colorThresholdMin) return maxHeight;
        
        return defaultHeight + (maxHeight - defaultHeight) * ((colorThresholdMax - colorDistance) / (colorThresholdMax - colorThresholdMin));
    }

    private IEnumerator AdjustPlatform(Transform platform, float targetHeight)
    {
        //Debug.Log($"Moving {platform.name} to height: {targetHeight}"); // Debug log
        Vector3 startPosition = platform.localPosition;
        Vector3 targetPosition = new Vector3(startPosition.x, targetHeight, startPosition.z);

        float elapsedTime = 0f;
        float duration = 1f / moveSpeed; // Adjust movement duration based on speed

        while (elapsedTime < duration)
        {
            float t = Mathf.Clamp01(elapsedTime / duration); // Ensure t is between 0 and 1
            platform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        platform.localPosition = targetPosition; // Ensure it reaches the exact final position
    }


}