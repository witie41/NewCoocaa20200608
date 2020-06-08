using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TESTVideo : MonoBehaviour
{
    // Start is called before the first frame update
    SvrVideoPlayer video;
    Text a;
    int time = 0;
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    private void Awake()
    {
        Debug.Log("Awake");
    }
    void Start()
    {
        Debug.Log("Start");
        video = transform.parent.GetComponentInChildren<SvrVideoPlayer>();
        a = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        time++;
        if (time < 30)
            return;
        time = 0;
        a.text = video.GetPlayerState().ToString();
    }
}
