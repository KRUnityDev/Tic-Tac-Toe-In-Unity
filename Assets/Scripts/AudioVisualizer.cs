using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVisualizer : MonoBehaviour {

    AudioSource audioSource;
    public RectTransform[] bars;
    public float barsStretchingForce = 1f;
    float[] samples;
    public float[] freqBands;
    public static float[] bufferBands;
    public float[] bufferDecrease;
    public float[] freqBandsScale;

    public RectTransform transform;

	void Awake () {
        samples = new float[512];

        freqBands = new float[8];
        bufferBands = new float[8];
        bufferDecrease = new float[8];

        this.audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	void Update () {
        GetSpectrumData();
        MakeSpectrumBands();
        ScaleFreqBands();
        CalculateBuffer();
        SetBarsScale();
    }

    void GetSpectrumData()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void MakeSpectrumBands()
    {
        int count = 0;
        for(int i = 1; i <= 8; i++)
        {
            freqBands[i - 1] = 0f;
            float k = Mathf.Pow(2,i);
            for (int n = 0; n < k; n++)
            {
                freqBands[i-1] += samples[count];
                count++;
            }
            freqBands[i-1] = freqBands[i-1] / Mathf.Pow(i, 2);
        }
    }

    void CalculateBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBands[i] > bufferBands[i])
            {
                bufferBands[i] = freqBands[i];
                bufferDecrease[i] = 0.005f;
            }
            if(freqBands[i] < bufferBands[i])
            {
                bufferBands[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
       }
    }

    void ScaleFreqBands()
    {
        for(int i = 0; i<8; i++)
        {
            freqBands[i] *= freqBandsScale[i];
        }
    }

    void SetBarsScale()
    {
        for(int i = 0; i < 8; i++)
        {
            if (bufferBands[i] < 0f) bufferBands[i] = 0;
            bars[i].localScale = new Vector3(bars[i].localScale.x, bufferBands[i] * barsStretchingForce, bars[i].localScale.z);
        }
    }

}
