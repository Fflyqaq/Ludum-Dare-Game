/****************************************************
    文件：BattleSystem.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 14:7:9
    功能：游戏内系统
*****************************************************/

using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSystem : SystemRoot
{
    public static BattleSystem Instance;

    public TalkPanel talkPanel;
    public GamePanel gamePanel;
    public WinPanel winPanel;
    public LosePanel losePanel;

    private Player player;
    private Rabbit rabbit;

    //当前第几关，本关地图数据，剩余步数
    private int nowCheckPointNum;
    public int NowCheckPointNum { get { return nowCheckPointNum; } }
    private MapData mapData = null;
    private MapManager mapManager;

    //这一关的第几段信息
    private int talkID;

    private bool isWin = false;
    public bool IsWin { set { isWin = value; } }
    private bool isLose = false;
    public bool IsLose { set { isLose = value; } }

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;

    }

    private void Update()
    {
        if (isWin)
        {
            WinThisCheckPoint(nowCheckPointNum + 1);
            isWin = false;
        }
        if (isLose)
        {
            StopPlayerMove();
            StartCoroutine(DelayTime(1.8f,()=> 
            {
                LoseThisCheckPoint();
                isLose = false;
            }));
        }
    }

    IEnumerator DelayTime(float second,Action action)
    {
        yield return new WaitForSeconds(second);
        action();
    }

    /// <summary>
    /// 要进入的关卡名和关卡ID
    /// </summary>
    public void EnterGame(string sceneName, int sceneNum)
    {
        ResService.Instance.AsyncLoadScene(sceneName, () =>
        {
            mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
            mapManager.InitMap(mapData);

            InitMapData();

            if (!Tools.CheckIfPlayedThisCheckPoint(sceneNum))
            {
                ShowNextTalk();
            }

            if (sceneNum == 5)
            {
                AudioService.Instance.PlayBGMusic(ConstAttribute.AudioBGM5);
            }
            else
            {
                AudioService.Instance.PlayBGMusic(ConstAttribute.AudioBGM1_4);
            }

            gamePanel.SetPanelState();
        });

        talkID = 1;

        EnterExitSystem.Instance.HideChoosePanel();
        
        nowCheckPointNum = sceneNum;
    }

    public void InitMapData()
    {
        mapData = ResService.Instance.GetMapData(nowCheckPointNum);
        gamePanel.SetWinText(mapData.winCondition);

        if (player == null)
        {
            player = ResService.Instance.LoadPrefab(ConstAttribute.playerPrefabPath, true).GetComponent<Player>();
            player.transform.position = mapData.playerBornPos;
        }

        if (rabbit == null)
        {
            rabbit = ResService.Instance.LoadPrefab(ConstAttribute.rabbitPrefabPath, true).GetComponent<Rabbit>();
            rabbit.transform.position = mapData.rabbitBornPos;
            rabbit.GetComponent<Rabbit>().SetMoveDir((int)mapData.rabbitBornRota.x, (int)mapData.rabbitBornRota.y);
        }
    }

    #region 封装显示隐藏
    public void ShowTalkPanel()
    {
        talkPanel.SetPanelState();
    }

    public void CloseTalkAndGamePanel()
    {
        talkPanel.SetPanelState(false);
        gamePanel.SetPanelState(false);
    }

    public void GameBackToLoginPanel()
    {
        gamePanel.SetPanelState(false);
    }
    #endregion

    /// <summary>
    /// 开始下一段的对话
    /// </summary>
    public void ShowNextTalk()
    {
        talkPanel.SetPanelState();
        talkPanel.GetNextInfoAndPlay(talkID);
        talkID ++ ;
    }


    public void StopPlayerMove()
    {
        player.StopPlayerMove();
        mapManager.CloseAllArrows();
        //Time.timeScale = 0;
    }
    public void ContinuePlayerMove()
    {
        player.ContinuePlayerMove();
        mapManager.OpenAllArrows();
        //Time.timeScale = 1;
    }

    public void RabbitMove()
    {
        if (rabbit != null)
        {
            rabbit.Move();
        }
    }

    /// <summary>
    /// 本关win
    /// </summary>
    private void WinThisCheckPoint(int checkPointNum)
    {
        int maxCheckPoint = PlayerPrefs.GetInt(ConstAttribute.maxCheckPointDBName);
        if (checkPointNum > maxCheckPoint)
        {
            //通关
            StopPlayerMove();
            winPanel.SetPanelState();
            CloseTalkAndGamePanel();
            return;
        }
        //10000 to 11000      11000-11000  要看本关是第几关
        string str = PlayerPrefs.GetString(ConstAttribute.checkPointDBName);
        char[] cTemp = str.ToCharArray();
        cTemp[checkPointNum - 1] = '1';
        str = new string(cTemp);

        //Regex regex = new Regex("0");
        //str = regex.Replace(str, "1", 1);

        PlayerPrefs.SetString(ConstAttribute.checkPointDBName, str);


        StopPlayerMove();
        winPanel.SetPanelState();
        CloseTalkAndGamePanel();
    }
    /// <summary>
    /// 本关lose
    /// </summary>
    private void LoseThisCheckPoint()
    {
        StopPlayerMove();
        losePanel.SetPanelState();
        CloseTalkAndGamePanel();
    }
}