using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talent : MonoBehaviour
{
    private Image sprite;

    [SerializeField]
    private Text countText;

    [SerializeField]
    private int maxCount;

    private int currentCount;

    private void Awake()
    {
        sprite = GetComponent<Image>();

    }

    public bool Click()
    {
        if (currentCount < maxCount)
        {
            currentCount++;
            countText.text = $"{currentCount}/{maxCount}";
            return true;
        }

        return false;
    }

    public void Lock()
    {
        sprite.color = Color.gray;
        countText.color = Color.gray;
    }

    public void Unlock()
    {
        sprite.color = Color.white;
        countText.color = Color.white;
    }
}
