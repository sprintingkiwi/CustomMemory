using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    public GameObject[] effects;

    // Shaking
    float accelerometerUpdateInterval = 1.0f / 60.0f;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the
    // filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds = 1.0f;
    // This next parameter is initialized to 2.0 per Apple's recommendation,
    // or at least according to Brady! ;)
    public float shakeDetectionThreshold;
    float lowPassFilterFactor;
    Vector3 lowPassValue;

    // Use this for initialization
    void Start ()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;

        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold || Input.GetKeyDown(KeyCode.E))
        {
            // Perform your "shaking actions" here. If necessary, add suitable
            // guards in the if check above to avoid redundant handling during
            // the same shake (e.g. a minimum refractory period).
            Debug.Log("Shake event detected at time " + Time.time);
            int n = Random.Range(0, effects.Length);
            Vector3 pos = new Vector3(Random.Range(-30, 30), Random.Range(-18, 18), 0f);
            Instantiate(effects[n], pos, Quaternion.identity);
        }
	}
}
