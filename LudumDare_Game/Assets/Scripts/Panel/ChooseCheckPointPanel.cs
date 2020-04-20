/****************************************************
    文件：ChooseChackPointPanel.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 15:11:6
    功能：关卡选择界面
*****************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCheckPointPanel : PanelRoot 
{
    public Button btnBack;

    public Button[] checkPointBtns;

    private void Start()
    {
        btnBack.onClick.AddListener(OnBackBtnClick);
    }

    protected override void InitPanel()
    {
        base.InitPanel();

        InitChooseBtn();
        ShowCanPlayCheckPoint();
    }

    private void InitChooseBtn()
    {
        //for (int i = 0; i < checkPointBtns.Length; i++)
        //{
        //    checkPointBtns[i].onClick.AddListener(() =>
        //    {
        //        OnPlayGameBtnClick(i+1);
        //    });
        //}

        checkPointBtns[0].onClick.AddListener(() =>
        {
            OnPlayGameBtnClick(1);
        });
        checkPointBtns[1].onClick.AddListener(() =>
        {
            OnPlayGameBtnClick(2);
        });
        checkPointBtns[2].onClick.AddListener(() =>
        {
            OnPlayGameBtnClick(3);
        });
        checkPointBtns[3].onClick.AddListener(() =>
        {
            OnPlayGameBtnClick(4);
        });
        checkPointBtns[4].onClick.AddListener(() =>
        {
            OnPlayGameBtnClick(5);
        });
    }

    /// <summary>
    /// 只显示可玩的关卡
    /// </summary>
    private void ShowCanPlayCheckPoint()
    {
        string cp = PlayerPrefs.GetString(ConstAttribute.checkPointDBName);
        int index = 0;
        foreach (Button go in checkPointBtns)
        {
            GameObject close = go.transform.Find("ImgClose").gameObject;
            if (cp[index] == '1')
            {
                close.SetActive(false);
                go.enabled = true;
            }
            else
            {
                close.SetActive(true);
                go.enabled = false;
            }
            index++;
        }
    }

    private void OnBackBtnClick()
    {
        SetPanelState(false);
        enterExitSystem.ChooseBackToLoginPanel();
    }
    /// <summary>
    /// 选择关卡，进入游戏
    /// </summary>
    /// <param name="checkPointNum">第几关</param>
    private void OnPlayGameBtnClick(int checkPointNum)
    {
        string sceneName = Tools.UseNumCalculateSceneName(checkPointNum);
        //string sceneName = resService.GetMapData(checkPointNum).sceneName;

        BattleSystem.Instance.EnterGame(sceneName, checkPointNum);
    }
}