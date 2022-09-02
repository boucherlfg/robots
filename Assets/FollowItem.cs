using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowItem : MonoBehaviour
{
    public Transform toFollow;
    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        pos = toFollow.position;
        pos.z = -10;
        transform.position = pos;
    }
}
