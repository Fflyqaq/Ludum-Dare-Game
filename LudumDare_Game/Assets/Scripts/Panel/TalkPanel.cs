/****************************************************
    文件：TalkPanel.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 13:50:35
    功能：对话界面
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class TalkPanel : PanelRoot 
{
    public Button btnNext;
    public Text txtTalk;
    public Image imgIcon;

    #region 选项内容
    public GameObject bgChoose;
    public GameObject choose1;
    public GameObject choose2;
    #endregion

    private int checkPointNum;
    private TalkData talkData;
    //对话信息和下标
    private string[] talkInfo;
    private int index;

    protected override void InitPanel()
    {
        base.InitPanel();

        talkData = resService.GetTalkData(checkPointNum);
        talkInfo = talkData.talk.Split('#');
        btnNext.onClick.AddListener(OnNextBtnClick);
        index = 1;

        bgChoose.SetActive(false);
        SetTalk();
        gameSystem.StopPlayerMove();
    }

    private void SetTalk()
    {
        string[] talkArr = talkInfo[index].Split('|');
        if (talkArr[0] == "0")
        {
            SetSprite(imgIcon, ConstAttribute.playerSpritePath);
            SetText(txtTalk, talkArr[1]);
        }
        else if(talkArr[0] == "1")
        {
            SetSprite(imgIcon, ConstAttribute.rabbitSpritePath);
            SetText(txtTalk, talkArr[1]);
        }
        else
        {
            bgChoose.SetActive(true);
        }

    }

    public void OnNextBtnClick()
    {
        index += 1;

        if (index == talkInfo.Length)
        {
            //对话结束
            SetPanelState(false);
            gameSystem.ContinuePlayerMove();
            PlayGameSystem.Instance.gamePanel.SetPanelState();
        }
        else
        {
            SetTalk();
        }
    }

    public void SetCheckPointNum(int num)
    {
        checkPointNum = num;
    }
}