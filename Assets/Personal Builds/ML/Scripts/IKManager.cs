using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    public List <Joint> Joints = new();
    public float SamplingDistance;
    public float LearningRate;
    public float DistanceThreshold = 1;
    public Transform target;


    private void Awake()
    {
        Joints = GetComponentsInChildren<Joint>().ToList();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        
    }

    public Vector3 ForwardKinematics (float [] angles)
    {
        Vector3 prevPoint = Joints[0].transform.position;
        Quaternion rotation = Quaternion.identity;
        for (int i = 1; i < Joints.Count; i++)
        {
            // Rotates around a new axis
            rotation *= Quaternion.AngleAxis(angles[i - 1], Joints[i - 1].Axis);
            Vector3 nextPoint = prevPoint + rotation * Joints[i].StartOffset;
    
            prevPoint = nextPoint;
        }
        return prevPoint;
    }
    
    private void LateUpdate()
    {
        var angles = Joints.Select(x => x.transform.rotation).ToList();
        //    InverseKinematics(target.position, );
    }

    public float DistanceFromTarget(Vector3 target, float [] angles)
    {
        Vector3 point = ForwardKinematics (angles);
        return Vector3.Distance(point, target);
    }
    
    public float PartialGradient (Vector3 target, float[] angles, int i)
    {
        // Saves the angle,
        // it will be restored later
        float angle = angles[i];

        // Gradient : [F(x+SamplingDistance) - F(x)] / h
        float f_x = DistanceFromTarget(target, angles);

        angles[i] += SamplingDistance;
        float f_x_plus_d = DistanceFromTarget(target, angles);

        float gradient = (f_x_plus_d - f_x) / SamplingDistance;

        // Restores
        angles[i] = angle;

        return gradient;
    }
    
    public void InverseKinematics (Vector3 target, float [] angles)
    {
        if (DistanceFromTarget(target, angles) < DistanceThreshold)
            return;

        for (int i = Joints.Count -1; i >= 0; i --)
        {
            // Gradient descent
            // Update : Solution -= LearningRate * Gradient
            float gradient = PartialGradient(target, angles, i);
            angles[i] -= LearningRate * gradient;

            // Clamp
            angles[i] = Mathf.Clamp(angles[i], Joints[i].MinAngle, Joints[i].MaxAngle);

            // Early termination
            if (DistanceFromTarget(target, angles) < DistanceThreshold)
                return;
        }
    }
}
