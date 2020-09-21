using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class UIManager
{
    public static Dictionary<string,TextMeshProUGUI> _uiTextElements = new Dictionary<string,TextMeshProUGUI>();

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
        _uiTextElements.Add(editorName,txt);
		txt.fontSize = fontSize;
		txt.text = text;
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
