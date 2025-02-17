using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class Distancebasedhaptic : MonoBehaviour
{
    [SerializeField] private GameObject leftController;
    [SerializeField] private GameObject rightController;
    [SerializeField] private HapticImpulsePlayer leftImpulse;
    [SerializeField] private HapticImpulsePlayer rightImpulse;
    [SerializeField] private float alertDistance = 5f;
    [Range(0f, 1f)]
    [SerializeField] private float intensity;
    private float leftIntensity;
    private float rightIntensity;

    private void Start()
    {
        leftImpulse = leftController.GetComponent<HapticImpulsePlayer>();
        rightImpulse = rightController.GetComponent<HapticImpulsePlayer>();
        leftIntensity = intensity;
        rightIntensity = intensity;
    }

    void Update()
    {
        if (leftController == null || rightController == null)
        {
            Debug.LogWarning("Please assign both objects in the Inspector.");
            return;
        }


        float leftDistance = Vector3.Distance(this.transform.position, leftController.transform.position);
        float rightDistance = Vector3.Distance(this.transform.position, rightController.transform.position);




        if (leftDistance <= alertDistance)
        {
            leftIntensity = 1 - leftDistance / alertDistance;
            leftImpulse.SendHapticImpulse(leftIntensity, (float)0.1);
        }
        if (rightDistance <= alertDistance)
        {
            rightIntensity = 1 - rightDistance / alertDistance;
            rightImpulse.SendHapticImpulse(rightIntensity, (float)0.1);
        }
    }
}