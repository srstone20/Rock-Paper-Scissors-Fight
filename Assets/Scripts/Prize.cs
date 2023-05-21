using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Prize : MonoBehaviour
{
    [Header("Exhibit Information")]
    [Tooltip("This info will tell the Table how to display weapon. Needed in case weapon orientations get messed up.")]
    public Vector3 exhibitRotation = new Vector3(90, 0, 0);
    public float exhibitHeight = 0f;
    public float exhibitScale = 0.7f;
}
