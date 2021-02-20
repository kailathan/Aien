using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    float timeCounter = 0;

    public float speed;
    public float width;
    public float height;

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * speed;
        float x = Mathf.Cos (timeCounter) * width;
        float y = Mathf.Sin (timeCounter) * height;


        transform.position = new Vector3(x, y, transform.position.z);
    }
}
