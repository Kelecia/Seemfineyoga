using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    public string url;  // URL to open

    public void OpenWebsite()
    {
        Application.OpenURL(url);
    }
}
