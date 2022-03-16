using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffectManager : MonoBehaviour
{
    private Image img;
    private float speed;
    private Color startColor;
    private Color endColor;

    void Start()
    {
        img = GetComponent<Image>();
        startColor = new Color(1, 1, 1, 1);
        endColor = new Color(1, 1, 1, 0);
        speed = 2.0f;

        img.color = startColor;
        StartCoroutine("ChangeColor");

    }

    private IEnumerator ChangeColor()
    {
        float tick = 0f;

        while (img.color != endColor)
        {
            tick += Time.deltaTime * speed;
            img.color = Color.Lerp(startColor, endColor, tick);
            yield return null;
        }
        Destroy(this.gameObject);
    }

}
