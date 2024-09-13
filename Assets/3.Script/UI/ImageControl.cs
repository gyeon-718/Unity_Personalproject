using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageControl : MonoBehaviour
{
    public Sprite[] images;
    public Image targetImage;


    public void ChangeImage(int index)
    {
        if (index >= 0 && index < images.Length)
        {
            targetImage.sprite = images[index];
        }
    }
}
