
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EmojiControl : MonoBehaviour
{
    public GameObject Content;
    public GameObject EmojiOption;
    private GameObject TempEmoji;
    private string EmojidirPath;
    private DirectoryInfo Emojidir;
    private FileInfo[] EmojiFiles;
    private bool HasChange=false;

    private void Awake()
    {
        EmojidirPath = Application.persistentDataPath + "Resouce/gif";
        EnablePanel(GetComponent<Toggle>().isOn);
    }

    public void EnablePanel(bool Ison)
    {
        Content.transform.parent.parent.localScale = Ison?Vector3.one:Vector3.zero;
    }
}
