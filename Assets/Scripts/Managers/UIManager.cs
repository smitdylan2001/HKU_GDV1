using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class UIManager
{
    private static List<TextMeshProUGUI> _uiTextElements = new List<TextMeshProUGUI>();
    private static List<TextMeshProUGUI> _uiSpriteElements = new List<TextMeshProUGUI>();
    public static GameObject Canvas { get; set; }

    
    public static void FindCanvas()
    {
        Canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
    }


    /// <summary>
    /// Generates a piece of text on a position given by the user
    /// </summary>
    /// <param name="position"></param>
    /// <param name="text"></param>
    /// <param name="editorName"></param>
    /// <param name="parent"></param>
    /// <param name="fontSize"></param>
    public static void AddTextUIElement(Vector2 position ,string text, string editorName, Transform parent, float fontSize)
    {
        GameObject uiElementGO = new GameObject();
        TextMeshProUGUI txt = uiElementGO.AddComponent<TextMeshProUGUI>();
        RectTransform rTransform = uiElementGO.GetComponent<RectTransform>();
        if (parent != null)
        {
            uiElementGO.transform.SetParent(parent.transform);
            rTransform.localPosition = position;
        }
        else
        {
            Debug.LogError("UIManager can't add UI element without a canvas as its parent");
        }
        _uiTextElements.Add(txt);
		txt.fontSize = fontSize;
		txt.text = text;
    }
    /// <summary>
    /// Adds an image/sprite UI element
    /// </summary>
    /// <param name="position"></param>
    /// <param name="path"></param>
    /// <param name="editorName"></param>
    /// <param name="parent"></param>
    public static void AddSpriteUIElement(Vector2 position,string path ,string editorName, Transform parent)
    {
        GameObject imageGameObject = new GameObject();
        Image img = imageGameObject.AddComponent<Image>();
        img.sprite = (Sprite)Resources.Load(Application.dataPath + path);
        RectTransform rTransform = imageGameObject.GetComponent<RectTransform>();
        if (parent != null)
        {
            imageGameObject.transform.SetParent(parent.transform);
            rTransform.localPosition = position;
        }
        else
        {
            Debug.LogError("UIManager can't add UI without a canvas as its parent");
        }
    }

	/// <summary>
	/// Updates a text element with new text (for instance updating score)
	/// </summary>
	/// <param name="textMesh"></param>
	/// <param name="newText"></param>
    public static void UpdateUITextElement(TextMeshProUGUI textMesh, string newText)
    {
        textMesh.text = newText;
    }

}
