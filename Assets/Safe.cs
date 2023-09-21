using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{

    public SpriteRenderer renderer;
    private IEnumerator MakeColorRed(SpriteRenderer renderer)
    {
        float duration = 0.1f;
        Color startColor = renderer.color;
        Color targetColor = Color.red;
        renderer.color = targetColor;

        yield return new WaitForSeconds(duration);
        renderer.color = startColor;

    }

    public void SafeHit()
    {
        StartCoroutine(MakeColorRed(renderer));

    }
}
