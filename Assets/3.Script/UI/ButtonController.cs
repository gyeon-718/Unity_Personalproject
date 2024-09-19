using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void ActiveScreen(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void DisactiveScreen(GameObject obj)
    {
        obj.SetActive(false);
    }
}
