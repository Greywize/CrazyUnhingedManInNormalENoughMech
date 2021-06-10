using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPlatformSwitcher : MonoBehaviour
{
    public Behaviour[] editorObjects;
    public Behaviour[] deviceObjects;

    void Awake()
    {
#if UNITY_EDITOR
        foreach (Behaviour bo in editorObjects)
        {
            bo.enabled = true;
        }
        foreach (Behaviour bo in deviceObjects)
        {
            bo.enabled = false;
        }
#else
        foreach (Behaviour bo in editorObjects)
        {
            bo.enabled = false;
        }
        foreach (Behaviour bo in deviceObjects)
        {
            bo.enabled = true;
        }
#endif
    }
}
