using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour {
    // health stages represented by eye closing
    [SerializeField] private Image eyeHealthImage;
    [SerializeField] private Sprite[] eyeStages;

    // shake intensity and duration
    public float shakeDuration = 0.5f; 
    public float shakeAmount = 5f; 

    // Stores original position of health bar
    private Vector3 originalPosition;
    
    void Start() {
        originalPosition = eyeHealthImage.transform.localPosition;
    }

    // reduces the hp bar visually and changes the color over time to red
    public void UpdateHealth(float healthPercent) {
        int stage = Mathf.Clamp(Mathf.RoundToInt(healthPercent * (eyeStages.Length - 1)), 0, eyeStages.Length - 1);
        eyeHealthImage.sprite = eyeStages[stage];
    }

    public void ShakeHealth(){
        StopAllCoroutines();
        StartCoroutine(Shake());
    }

    private IEnumerator Shake() {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration) {
            // geenrate offset based on shake amount
            float xOffset = Random.Range(-shakeAmount, shakeAmount);
            float yOffset = Random.Range(-shakeAmount, shakeAmount);

            // apply offset and increase time
            eyeHealthImage.transform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    // reset position after shaking
    eyeHealthImage.transform.localPosition = originalPosition;
    }

    // Blinks the eye whenever it gets hit
    public void Blink() {
        if (eyeHealthImage != null && eyeStages.Length > 0) {
            StartCoroutine(BlinkRoutine());
        }
    }

    private IEnumerator BlinkRoutine() {
        Sprite currentSprite = eyeHealthImage.sprite;

        // Fully closed sprite = index 0
        eyeHealthImage.sprite = eyeStages[0];
        yield return new WaitForSeconds(0.1f);

        eyeHealthImage.sprite = currentSprite;
    }
}
