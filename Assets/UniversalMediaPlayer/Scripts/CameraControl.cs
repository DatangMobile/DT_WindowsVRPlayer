using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Camera cam1080
    {
        get
        {
            return _cam1080;
        }
        set
        {
            _cam1080 = value;
        }
    }
    public Camera cam4k
    {
        get
        {
            return _cam4k;
        }
        set
        {
            _cam4k = value;
        }

    }

    [SerializeField]
    private Camera _cam1080;
    [SerializeField]
    private Camera _cam4k;
    [SerializeField]
    private float _changerate;
    [SerializeField]
    private float _accelerate;
    [SerializeField]
    private int _durtime;
    [SerializeField]
    private int _1080time;

    private Rect rect1080 = new Rect(0, 0, 1, 1);
    private Rect rect4k = new Rect();
    private System.Timers.Timer timer_start;
    private System.Timers.Timer timer_all;
    private bool change1080 = false;
    private bool durcomplete = false;

    // Use this for initialization
    void Start ()
    {
        cam1080.rect = rect1080;
        timer_start = new System.Timers.Timer(_1080time);
        timer_start.Elapsed += Timer_start_Elapsed;
        timer_start.AutoReset = true;
        timer_start.Enabled = true;
        timer_all = new System.Timers.Timer(_durtime);
        timer_all.Elapsed += Timer_all_Elapsed;
        timer_all.AutoReset = true;
        timer_all.Enabled = true;
    }
    
    // Update is called once per frame
    void Update ()
    {
        if ((cam1080.rect.height > 0.5) && change1080)
        {
            _changerate += _accelerate;
            rect1080.height -= _changerate;
            rect1080.width -= _changerate;
            cam1080.rect = rect1080;
        }
        // 如果整个周期到了，还原1080相机;
        if(durcomplete)
        {
            cam1080.rect = new Rect(0, 0, 1, 1);
            durcomplete = false;
            change1080 = false;
        }
    }

    private void Timer_start_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        // 如果还在整个展示周期中，则不还原;
        change1080 = true;
        timer_start.Enabled = false;
        Debug.Log("Timer start Elapsed!");
    }

    private void Timer_all_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        durcomplete = true;
        timer_start.Enabled = true;
        rect1080 = new Rect(0, 0, 1, 1);
        Debug.Log("Timer all Elapsed!");
    }

}
