using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossPlatformSwitcher : MonoBehaviour
{
    public MonoBehaviour editorObject;
    public MonoBehaviour deviceObject;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        editorObject.enabled = true;
        deviceObject.enabled = false;
#else
        editorObject.enabled = false;
        deviceObject.enabled = true;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
