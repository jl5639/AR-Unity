using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapturedMonsterUI : MonoBehaviour
{
    public Image portraitImage;

    public void Setup(Sprite portraitSprite)
    {
        portraitImage.sprite = portraitSprite;
    }
}
