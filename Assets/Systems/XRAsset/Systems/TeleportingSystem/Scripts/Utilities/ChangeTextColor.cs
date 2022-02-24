using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextColor : MonoBehaviour
{
    [Tooltip("Color should be given in HEX format without forwarding # symbol")]
    //[SerializeField] private string stringColor;
    [SerializeField] public string stringColor;    
    [SerializeField] public Text textArea;

    public void SetColor(string colorToSet)
    {
        Color outputColor = new Color();
        ColorUtility.TryParseHtmlString("#"+colorToSet, out outputColor);
        textArea.color = outputColor;
    }

    string ValidateColorString(string colorStringToVerify)
    {
        return "";
        //need implementetion to verify if given string is correct hex color
        //if # symbol is given it should be truncated
    }
}
