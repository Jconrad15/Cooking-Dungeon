using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    [SerializeField]
    private Light l;

    private float minimumIntensity = 1.2f;
    private float maximumIntensity = 2f;

    private float rangeDifference;
    private float timeElapsed = 0;
    private float speed;

    private void Start()
    {
        rangeDifference = maximumIntensity - minimumIntensity;
        l.intensity = minimumIntensity;
    }

    private void Update()
    {
        speed = Random.Range(0.5f, 4f);

        float scalar = Mathf.PingPong(timeElapsed, rangeDifference);
        IncrementTime();


        l.range = minimumIntensity + scalar;
    }

    private void IncrementTime()
    {
        timeElapsed = 
            (Time.deltaTime * speed * Random.Range(-0.5f, 2f))
            + timeElapsed;
        //reset timeelapsed if too big
        if (timeElapsed > 100000) { timeElapsed = 0f; }
    }
}
