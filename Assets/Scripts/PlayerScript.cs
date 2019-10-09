using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{    
    public Transform target;
    public float moveSpeed;
    public void UpdateTarget(Transform target)
    {
        this.target = target;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
