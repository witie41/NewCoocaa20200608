using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoDestroyGiftInfo : MonoBehaviour {
	void Start () {
        GetComponent<Image>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Destroy(gameObject, 4);
	}
	
}
