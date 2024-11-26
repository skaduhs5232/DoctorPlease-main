using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullFadeScript : MonoBehaviour
{
    internal bool fadeIn = true;
    internal bool fadeOut = false;
    internal float fadeSpeed = 0.6f;
    [SerializeField] private SpriteRenderer fader;
    private Color color;

    void Start()
    {
        fader = GetComponent<SpriteRenderer>();
        color = fader.color;
    }

    void Update()
    {
        if (fadeIn)
        {
            color.a = Mathf.Clamp01(color.a + fadeSpeed * Time.deltaTime); 
            fader.color = color;

            if (color.a >= 1f)
            {
                fadeIn = false; // Stop fading when fully visible
            }
        }

        else if (fadeOut)
        {
            color.a = Mathf.Clamp01(color.a - fadeSpeed/2.5f * Time.deltaTime); 
            fader.color = color;

            if (color.a <= 0f)
            {
                Destroy(this);
            }
        }
        
    }
}
