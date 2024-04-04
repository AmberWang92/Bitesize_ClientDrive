using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * 8);
    }
}
