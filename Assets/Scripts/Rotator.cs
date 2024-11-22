using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	float time = 0;

    void Update()
    {
		time += Time.deltaTime*1.2f;
		transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.Sin(time) * 5f);
    }
}
