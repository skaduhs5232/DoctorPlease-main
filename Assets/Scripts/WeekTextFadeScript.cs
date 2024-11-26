using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeekTextFadeScript : MonoBehaviour
{
    internal bool fadeIn = true;
    internal bool fadeOut = false;
    internal float fadeSpeed = 0.4f;
    [SerializeField] private Text text;
    private Color color;

    void Start()
    {
        text = GetComponent<Text>();
        color = text.color;
    }

    void Update()
    {
        if (fadeIn)
        {
            color.a = Mathf.Clamp01(color.a + fadeSpeed * Time.deltaTime); 
            text.color = color;

            if (color.a >= 1f)
            {
                fadeIn = false; // Stop fading when fully visible
            }
        }

        else if (fadeOut)
        {
            color.a = Mathf.Clamp01(color.a - fadeSpeed * Time.deltaTime); 
            text.color = color;

            if (color.a <= 0f)
            {
                Destroy(this);
            }
        }
        
    }
}
