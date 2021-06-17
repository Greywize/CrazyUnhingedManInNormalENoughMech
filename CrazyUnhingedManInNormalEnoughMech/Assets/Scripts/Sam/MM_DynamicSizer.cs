using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class MM_DynamicSizer : MonoBehaviour
{
    [SerializeField]
    private bool m_dynamicWidth = false;

    [SerializeField]
    private bool m_dynamicHeight = false;

    [SerializeField]
    private float m_xPercent = 0;

    [SerializeField]
    private float m_yPercent = 0;

    [SerializeField]
    private float m_width = 0;

    [SerializeField]
    private float m_height = 0;

    RectTransform m_parent;
    RectTransform m_rect;

    bool initialized = false;

    private void Update()
    {
        //Get initial values if you don't have them
        if (!initialized)
        {
            m_parent = transform.parent.GetComponent<RectTransform>();
            m_rect = GetComponent<RectTransform>();

            m_width = m_rect.rect.width;
            m_height = m_rect.rect.height;

            initialized = true;
        }

        //Update internal % and fixed dimensions accordingly
            if (m_dynamicWidth)
            m_width = m_xPercent * m_parent.rect.width / 100;
        else
            m_xPercent = m_width / m_parent.rect.width * 100;

        if (m_dynamicHeight)
            m_height = m_yPercent * m_parent.rect.height / 100;
        else
            m_yPercent = m_height / m_parent.rect.height * 100;

        //Update external dimensions
        m_rect.sizeDelta = new Vector2(m_width, m_rect.rect.height);
        m_rect.sizeDelta = new Vector2(m_rect.rect.width, m_height);

        LayoutRebuilder.ForceRebuildLayoutImmediate(m_rect);
    }
}