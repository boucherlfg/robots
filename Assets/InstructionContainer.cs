using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionContainer : MonoBehaviour
{
    public Player player;
    public GameObject APrefab, WPrefab, DPrefab, XPrefab;

    public void Start()
    {
        player.LineChanged += Refresh;
    }

    public void Refresh()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var letter in player.line)
        {
            switch (letter)
            {
                case 'a':
                    Instantiate(APrefab, transform);
                    break;
                case 'w':
                    Instantiate(WPrefab, transform);
                    break;
                case 'd':
                    Instantiate(DPrefab, transform);
                    break;
                case 'x':
                    Instantiate(XPrefab, transform);
                    break;
                default:
                    throw new System.Exception("instruction mistake");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        var mainRect = GetComponent<RectTransform>();
        var size = Vector2.Scale(mainRect.sizeDelta, Vector2.up);
        foreach (Transform child in transform)
        {
            var rect = child.GetComponent<RectTransform>();
            size += Vector2.Scale(rect.sizeDelta, Vector2.right);
        }
        mainRect.sizeDelta = size;
    }
}
