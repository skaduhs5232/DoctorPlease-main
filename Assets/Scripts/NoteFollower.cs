using UnityEngine;

public class ButtonFollowLetter : MonoBehaviour
{
    public Transform letterTransform;   // The Transform of the letter sprite
    private RectTransform rectTransform; // The RectTransform of the button
    private Canvas canvas;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    void LateUpdate()
    {
        if (letterTransform != null)
        {
            Vector2 canvasPosition;
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(letterTransform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform, 
                screenPoint, 
                canvas.worldCamera, 
                out canvasPosition);

            rectTransform.anchoredPosition =  new Vector2(canvasPosition.x, canvasPosition.y - 200); 
        }
    }

    // Set this function to call when instantiating the button
    public void SetLetterTransform(Transform letter)
    {
        letterTransform = letter;
    }
}
