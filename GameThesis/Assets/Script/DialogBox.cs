using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    public void SetupDailogText(string text)
    {
        dialogText.text = text;
    }

}
