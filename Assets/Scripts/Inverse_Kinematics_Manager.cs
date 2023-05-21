using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverse_Kinematics_Manager : MonoBehaviour
{
    public Joints Rshoulder;

    public Joints Rhand;

    public GameObject target;

    public Joints Lshoulder;

    public Joints Lhand;

    public float threshold = 0.05f;

    public float rate = 5;

    public int steps = 20;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < steps; ++i)
        {
            if (GetDistance(Rhand.transform.position, target.transform.position) > threshold)
            {
                Joints current = Rshoulder;
                while (current != null)
                {
                    float slope = CalculateSlope(current, Rhand);
                    current.Rotate(-slope * rate);
                    current = current.GetChild();
                }
            }
            if (GetDistance(Lhand.transform.position, target.transform.position) > threshold)
            {
                Joints current = Lshoulder;
                while (current != null)
                {
                    float slope = CalculateSlope(current, Lhand);
                    current.Rotate(-slope * rate);
                    current = current.GetChild();
                }
            }
        }
    }

    float CalculateSlope(Joints _joint, Joints hand)
    {
        float deltaTheta = 0.01f;
        float distance1 = GetDistance(hand.transform.position, target.transform.position);

        _joint.Rotate(deltaTheta);

        float distance2 = GetDistance(hand.transform.position, target.transform.position);

        _joint.Rotate(-deltaTheta);

        return (distance2 - distance1) / deltaTheta;
    }

    float GetDistance(Vector3 _point1, Vector3 _point2)
    {
        return Vector3.Distance(_point1, _point2);
    }
}
