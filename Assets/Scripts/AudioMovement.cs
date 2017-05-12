using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Temporary class!!
public class AudioMovement : MonoBehaviour {

    RectTransform rectTransform;
    public float power;
    public bool scale = false;
    public bool rotate = false;
    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }
    
	void LateUpdate () {
        if (scale)
        {
            float scale = 1f + AudioVisualizer.bufferBands[1] * power;
            rectTransform.localScale = new Vector3(scale, scale, scale);
        }
        if ( rotate)
        {
            rectTransform.localRotation = Quaternion.Euler(rectTransform.rotation.x, rectTransform.rotation.y, (AudioVisualizer.bufferBands[0]-1)*power); 
        }
	}
}
