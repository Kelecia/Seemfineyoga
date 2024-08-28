using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limbattachmnet : MonoBehaviour
{
    public Rigidbody2D connectedBody; // Set this in the inspector to the parent body part

    void Start()
    {
        FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = connectedBody;
    }
}
