using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {


    public enum WaveForm { sin, tri, sqr, saw, inv, noise };
    public WaveForm waveform = WaveForm.sin;

    public float delay = 0; // delay time
    public float baseStart = 0.0f; //start
    public float amplitude = 1.0f; //amplitude of the wave
    public float phase = 0.0f; // start point inside on wave cycle
    public float frequency = 0.1f; // cycle frequency per second

    //keep a copy of the orignal color
    private Color originalColor;
    private Light light;

    private int x = 0;
    //Store the original color
    void Start()
    {
        light = GetComponent<Light>();
        originalColor = light.color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        x += 1;

        if (x >= delay)
        {
            light.color = originalColor * (EvalWave());
            x = 0;
        }
    }

    float EvalWave()
    {
        float x = (Time.time + phase) * frequency;
        float y;
        x = x - Mathf.Floor(x); // normalized value (0..1)

        if (waveform == WaveForm.sin)
        {
            y = Mathf.Sin(x * 2 * Mathf.PI);
        }
        else if (waveform == WaveForm.tri)
        {

            if (x < 0.5f)
            {
                y = 1.0f;
            }
            else
            {
                y = -1.0f;
            }
        }
        else if (waveform == WaveForm.saw)
        {
            y = x;
        }
        else if (waveform == WaveForm.inv)
        {
            y = 1.0f - x;
        }
        else if (waveform == WaveForm.noise)
        {
            y = 1f - (Random.value * 2);
        }
        else
        {
            y = 1.0f;
        }

        return (y * amplitude) + baseStart;
    }
}
