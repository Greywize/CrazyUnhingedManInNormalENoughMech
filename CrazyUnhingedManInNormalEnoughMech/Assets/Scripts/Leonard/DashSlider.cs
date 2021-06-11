using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashSlider : MonoBehaviour
{
    public Slider slider;
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dashWarmup = new Vector2(player.dashWarmupTime, player.dashWarmupTime - player.initialDashWarmupTime).normalized;
        slider.value = Mathf.Clamp01(dashWarmup.y + 1);

        if (player.dashWarmupTime <= 0)
        {
            slider.value = 0;
        }
        if (player.dashing && player.didDash)
        {
            Vector2 regainValue = new Vector2(player.dashWarmupTime, player.dashWarmupTime - player.initialDashTime).normalized;
            slider.value = Mathf.Clamp01(regainValue.y + 1);
        }
    }
}
