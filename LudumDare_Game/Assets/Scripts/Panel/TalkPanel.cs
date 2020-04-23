/****************************************************
    文件：TalkPanel.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 13:50:35
    功能：对话界面
*****************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TalkPanel : PanelRoot 
{
    public Button btnNext;
    public Text txtTalk;
    public Image imgIcon;

    #region 选项内容
    public GameObject bgChoose;
    public GameObject chooseGO1;
    public GameObject chooseGO2;
    #endregion

    //第几关，和这一关的第几段talk信息
    private int checkPointNum;
    private int talkID;
    private TalkData talkData;
    //一段对话信息、当前段的一句信息和下标
    private string[] talkInfo;
    private string talkInfoOne;
    private int index;
    private bool isTalkAnimPlay = false;

    private void Start()
    {
        btnNext.onClick.AddListener(OnNextBtnClick);
    }

    protected override void InitPanel()
    {
        base.InitPanel();

        //每段对话从头开始读取
        index = 1;

        bgChoose.SetActive(false);
        battleSystem.StopPlayerMove();

        //当前关卡num
        checkPointNum = battleSystem.NowCheckPointNum;
    }

    public void GetNextInfoAndPlay(int talkID)
    {
        //组合成XML中的格式
        int id = int.Parse(checkPointNum + "0" + talkID);
        Debug.Log(id);
        this.talkID = id;
        talkData = resService.GetTalkData(id);
        if (talkData != null)
        {
            talkInfo = talkData.talk.Split('#');
            SetTalk();
        }
    }

    /// <summary>
    /// 设置对话显示
    /// </summary>
    private void SetTalk()
    {
        string[] talkArr = talkInfo[index].Split('|');
        talkInfoOne = talkArr[1];
        if (talkArr[0] == "0")
        {
            SetSprite(imgIcon, ConstAttribute.playerSpritePath);

            StartCoroutine(SetTextAnim(talkInfoOne));
        }
        else if(talkArr[0] == "1")
        {
            SetSprite(imgIcon, ConstAttribute.rabbitSpritePath);

            StartCoroutine(SetTextAnim(talkInfoOne));
        }
        else if (talkArr[0] == "2")
        {
            bgChoose.SetActive(true);
            Choose(talkInfoOne);
        }
    }

    private void SetTextNoAnim(string talk)
    {
        isTalkAnimPlay = false;
        txtTalk.text = talk;
    }

    IEnumerator SetTextAnim(string talk)
    {
        isTalkAnimPlay = true;
        string temp = "";
        txtTalk.text = "";
        for (int i = 0; i < talk.Length; i++)
        {
            if (isTalkAnimPlay)
            {
                temp += talk[i];
                txtTalk.text = temp;
                yield return new WaitForSeconds(0.06f);
            }
        }
        isTalkAnimPlay = false;
    }

    private void OnNextBtnClick()
    {
        if (isTalkAnimPlay)
        {
            SetTextNoAnim(talkInfoOne);
        }
        else
        {
            index += 1;

            if (index >= talkInfo.Length)
            {
                //对话结束
                SetPanelState(false);
                battleSystem.ContinuePlayerMove();
                battleSystem.gamePanel.SetPanelState();

                CheckIfPassGame();
            }
            else
            {
                SetTalk();
            }
        }
        
    }

    #region 选择对话
    private void Choose(string talk)
    {
        // 1选项-101+2选项-102
        string[] talkChoose = talk.Split('+');
        string[] choose1 = talkChoose[0].Split('-');
        string[] choose2 = talkChoose[1].Split('-');

        //选项按钮上的字
        chooseGO1.GetComponent<Text>().text = choose1[0];
        chooseGO2.GetComponent<Text>().text = choose2[0];

        //选择后改变之后对话内容
        int id1 = int.Parse(choose1[1]);
        chooseGO1.GetComponent<Button>().onClick.AddListener(() =>
        {
            LoadTalkInfo(id1);
        });
        int id2 = int.Parse(choose2[1]);
        chooseGO2.GetComponent<Button>().onClick.AddListener(() =>
        {
            LoadTalkInfo(id2);
        });
    }

    private void LoadTalkInfo(int id)
    {
        string talk = resService.GetTalkData(id).talk;
        talkInfo = talk.Split('#');
        index = 1;
        bgChoose.SetActive(false);
        SetTalk();
    }
    #endregion

    private void CheckIfPassGame()
    {
        int tempTalkID = talkID + 1;
        TalkData data = resService.GetTalkData(tempTalkID);
        if (data == null)
        {
            //过关
            battleSystem.IsWin = true;
        }
    }
}