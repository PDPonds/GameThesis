using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialImage : MonoBehaviour
{
    public Image image;

    public void SetupImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

}
