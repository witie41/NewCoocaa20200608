using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGiftInfo : MonoBehaviour {

    float time = 0;
    float deltaTime = 1;
    public GameObject GiftInfo;

	void Update () {
		if(time>=deltaTime)
        {
            time = 0;
            deltaTime = Random.Range(0.5f, 2f);
            Instantiate(GiftInfo, transform);
        }
        time += Time.deltaTime;
	}
}
