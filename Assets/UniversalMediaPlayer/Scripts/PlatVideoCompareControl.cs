using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlatVideoCompareControl : MonoBehaviour {

    [SerializeField]
    private GameObject gm1;
    [SerializeField]
    private GameObject gm2;
    [SerializeField]
    private GameObject gm3;
    [SerializeField]
    private GameObject gm4;
    [SerializeField]
    private GameObject video_1080;
    [SerializeField]
    private float _changerate;
    [SerializeField]
    private float _accelerate;
    [SerializeField]
    private int _durtime;
    [SerializeField]
    private int _1080time;

    private bool m_show { get; set; }

    private System.Timers.Timer timer_start;
    private System.Timers.Timer timer_all;
    private bool change1080 = false;
    private bool durcomplete = false;
    private float defalutchangerate;

    private Vector3 v1;
    private Vector3 v2;
    private Vector3 v3;
    private Vector3 v4;
    private Vector3 v5;

    // Use this for initialization
    void Start () {
        m_show = true;
        timer_start = new System.Timers.Timer(_1080time);
        timer_start.Elapsed += Timer_start_Elapsed;
        timer_start.AutoReset = true;
        timer_start.Enabled = true;
        timer_all = new System.Timers.Timer(_durtime);
        timer_all.Elapsed += Timer_all_Elapsed;
        timer_all.AutoReset = true;
        timer_all.Enabled = true;
        defalutchangerate = _changerate;

        v1 = gm1.transform.position;
        v2 = gm2.transform.position;
        v3 = gm3.transform.position;
        v4 = gm4.transform.position;
        v5 = new Vector3(1, 1, 1);
    }
    
    // Update is called once per frame
    void Update () {

        // 快捷键;
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log("F2 key was pressed.");
            if(m_show)
            {
                gm1.transform.position = new Vector3(1000, 100, 100);
                gm2.transform.position = new Vector3(1000, 100, 100);
                gm3.transform.position = new Vector3(1000, 100, 100);
                gm4.transform.position = new Vector3(1000, 100, 100);
                m_show = false;
            }
            else
            {
                gm1.transform.position = v1;
                gm2.transform.position = v2;
                gm3.transform.position = v3;
                gm4.transform.position = v4;
                m_show = true;
            }
        }

        if ((video_1080.transform.localScale.x > 0.5) && change1080)
        {
            video_1080.transform.localScale = v5;
            _changerate += _accelerate;
            v5.x -= _changerate;
            v5.y -= _changerate;
        }

        // 如果整个周期到了，还原1080相机;
        if (durcomplete && (video_1080.transform.localScale.x <= 1))
        {
            video_1080.transform.localScale = v5;
            _changerate += _accelerate;
            v5.x += _changerate;
            v5.y += _changerate;
            
            if ((video_1080.transform.localScale.x == 1) && (video_1080.transform.localScale.y == 1))
            {
                durcomplete = false;
                change1080 = false;
            }
        }
    }

    private void Timer_all_Elapsed(object sender, ElapsedEventArgs e)
    {
        durcomplete = true;
        change1080 = false;
        timer_start.Enabled = true;
        _changerate = defalutchangerate;
        Debug.Log("Timer all Elapsed!");
    }

    private void Timer_start_Elapsed(object sender, ElapsedEventArgs e)
    {
        change1080 = true;
        durcomplete = false;
        timer_start.Enabled = false;
        Debug.Log("Timer start Elapsed!");
    }
}
