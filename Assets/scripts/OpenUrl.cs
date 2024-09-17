using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    public void LinkUrl(string Urlname)
    {
        Application.OpenURL(Urlname);
    }
}
