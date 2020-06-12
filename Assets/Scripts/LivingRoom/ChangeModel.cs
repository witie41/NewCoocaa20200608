using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeModel : MonoBehaviour
{
    public SvrVideoControlPanel ControlPanel;
    public Slider VolumeSlider;
    [Header("播放器列表")]
    public GameObject Player2D;
    public GameObject Player3DLR;
    public GameObject Player3DTB;
    public GameObject Player180;
    public GameObject Player180LR; 
    public GameObject Player180TB;
    public GameObject Player360;
    public GameObject Player360LR;
    public GameObject Player360TB;
    public GameObject RecommenedPlayer;
    

    public bool Finish = false;
    public bool live = false;

    private void Awake()
    {
        if (live)
        {
            Player2D.GetComponent<SvrVideoPlayerDemo>().live = true;
            Player3DLR.GetComponent<SvrVideoPlayerDemo>().live = true;
            Player3DTB.GetComponent<SvrVideoPlayerDemo>().live = true;
            Player180.GetComponent<SvrVideoPlayerDemo>().live = true;
            Player180LR.GetComponent<SvrVideoPlayerDemo>().live = true;
            Player180TB.GetComponent<SvrVideoPlayerDemo>().live = true;
            Player360.GetComponent<SvrVideoPlayerDemo>().live = true;
            Player360LR.GetComponent<SvrVideoPlayerDemo>().live = true;
            Player360TB.GetComponent<SvrVideoPlayerDemo>().live = true;
        }
    }


    private GameObject currentPlayer;
    public GameObject CurrentPlayer
    {
        get
        {
            return currentPlayer; 
        }
        set
        {
            if(currentPlayer==value)
             return; 
            if (currentPlayer != null)
            {
                long CurrentPosition = 0;
                //获取播放进度
                if (!live)
                {
                    CurrentPosition = currentPlayer.GetComponent<SvrVideoPlayer>().GetCurrentPosition();
                }
                //获取播放音量
                float CurrentVolumePercent = currentPlayer.GetComponent<SvrVideoPlayer>().GetCurrentVolumePercent();
                //销毁播放器
                //currentPlayer.GetComponent<SvrVideoPlayer>().Stop();
                //currentPlayer.GetComponent<SvrVideoPlayer>().Release();
                ControlPanel.gameObject.SetActive(false);
                currentPlayer.SetActive(false);
                //新建播放器
                currentPlayer = value;
                currentPlayer.SetActive(true);
                ControlPanel.gameObject.SetActive(true);
                //currentPlayer.GetComponent<SvrVideoPlayerDemo>().PlayVideoByIndex(0);
                //连接ControlPanel与播放器
                ControlPanel.GetComponent<SvrVideoControlPanel>().SvrVideoPlayerDemo = currentPlayer.GetComponent<SvrVideoPlayerDemo>();
                //currentPlayer.GetComponent<SvrVideoPlayerDemo>().SvrVideoControlPanel = ControlPanel.GetComponent<SvrVideoControlPanel>();
                //重置音量和播放进度
                StartCoroutine(IEOnChangeModel(currentPlayer.GetComponent<SvrVideoPlayer>(), CurrentVolumePercent, CurrentPosition));
            }
            else
            {
                //新建播放器
                currentPlayer = value;
                currentPlayer.SetActive(true);
                Debug.Log(ControlPanel);
                ControlPanel.gameObject.SetActive(true);
                //currentPlayer.GetComponent<SvrVideoPlayerDemo>().PlayVideoByIndex(0);
                //连接ControlPanel与播放器
                ControlPanel.GetComponent<SvrVideoControlPanel>().SvrVideoPlayerDemo = currentPlayer.GetComponent<SvrVideoPlayerDemo>();
                //currentPlayer.GetComponent<SvrVideoPlayerDemo>().SvrVideoControlPanel = ControlPanel.GetComponent<SvrVideoControlPanel>();
                Finish = true;
                if (currentPlayer.GetComponent<SvrVideoPlayerDemo>().Finish)
                    currentPlayer.GetComponent<SvrVideoPlayerDemo>().PlayVideoByIndex(0,live);
            }

            Debug.Log(333333333);
        }
        
    }

    private IEnumerator IEOnChangeModel(SvrVideoPlayer svrVideoPlayer, float CurrentVolumePercent, long CurrentPosition)
    {
        yield return new WaitWhile(()=>svrVideoPlayer.GetPlayerState()==VideoPlayerState.Ready);
        if(CurrentPosition!=-1&& !CompareTag("LivingRoomModelChange"))
        {
            currentPlayer.GetComponent<SvrVideoPlayer>().SeekToTime(CurrentPosition);
        }
        //SDK中方法不可用（原因未知）
        VolumeSlider.value = CurrentVolumePercent;
        yield break;
    }

    void Start()
    {
        if(ControlPanel==null)
            ControlPanel = transform.parent.GetComponent<SvrVideoControlPanel>();
    }

    public void FlashVideo()
    {
        OnDisable();
        OnEnable();
    }

    private void OnDisable()
    {
        if (CurrentPlayer != null)
        {
            CurrentPlayer.GetComponent<SvrVideoPlayer>().Stop();
            CurrentPlayer.GetComponent<SvrVideoPlayer>().Release();
        }
    }

    private void OnEnable()
    {
        if (currentPlayer != null)
        {
            currentPlayer.SetActive(true);
            ControlPanel.gameObject.SetActive(true);
            currentPlayer.GetComponent<SvrVideoPlayer>().CreatVideoPlayer();
            currentPlayer.GetComponent<SvrVideoPlayer>().PreparedPlayVideo(currentPlayer.GetComponent<SvrVideoPlayerDemo>().VideoUrls[0],live);
            //连接ControlPanel与播放器
            ControlPanel.GetComponent<SvrVideoControlPanel>().SvrVideoPlayerDemo = currentPlayer.GetComponent<SvrVideoPlayerDemo>();
            currentPlayer.GetComponent<SvrVideoPlayerDemo>().SvrVideoControlPanel = ControlPanel.GetComponent<SvrVideoControlPanel>();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (!live)
            return;
        if(pause)
        {
            OnDisable();
        }
        else
        {
            OnEnable();
        }
    }

    public void ChangePlayModel()
    {
        int value = GetComponent<GvrDropdown>().value;
        switch (value)
        {
            //推荐格式
            case 0:
                {
                    CurrentPlayer = RecommenedPlayer;
                    break;
                }
            //2D播放
            case 1:
                {
                    CurrentPlayer = Player2D;
                    break;
                }
            //3D左右播放
            case 2:
                {
                    CurrentPlayer = Player3DLR;
                    break;
                }
            //3D上下播放
            case 3:
                {
                    CurrentPlayer = Player3DTB;
                    break;
                }
            //180播放
            case 4:
                {
                    CurrentPlayer = Player180;
                    break;
                }
            //180左右播放
            case 5:
                {
                    CurrentPlayer = Player180LR;
                    break;
                }
            //180上下播放
            case 6:
                {
                    CurrentPlayer = Player180TB;
                    break;
                }
            //360播放
            case 7:
                {
                    CurrentPlayer = Player360;
                    break;
                }
            //360左右播放
            case 8:
                {
                    CurrentPlayer = Player360LR;
                    break;
                }
            //360上下播放
            case 9:
                {
                    CurrentPlayer = Player360TB;
                    break;
                }
            default:
                {
                    Debug.LogError("切换格式时发生错误");
                    break;
                }
        }
    }
}