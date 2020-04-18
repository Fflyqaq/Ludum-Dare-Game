/****************************************************
    文件：GameSystem.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 14:7:9
    功能：游戏内系统
*****************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameSystem : SystemRoot
{
    public static PlayGameSystem Instance;

    public TalkPanel talkPanel;
    public GamePanel gamePanel;

    private Player player;


    public override void InitSys()
    {
        base.InitSys();
        Instance = this;

    }

    /// <summary>
    /// 要进入的关卡名和关卡ID
    /// </summary>
    public void EnterGame(string sceneName, int sceneNum)
    {
        SceneManager.LoadScene(sceneName);
        EnterExitSystem.Instance.HideChoosePanel();
        ShowTalkPanel(sceneNum);
    }

    /// <summary>
    /// 显示对应关卡的对话
    /// </summary>
    public void ShowTalkPanel(int sceneNum)
    {
        talkPanel.SetCheckPointNum(sceneNum);
        talkPanel.SetPanelState();
    }

    public void StopPlayerMove()
    {
        //初始化Player
        player = ResService.Instance.LoadPrefab(ConstAttribute.playerPrefabPath,true).GetComponent<Player>();
        player.transform.position = new Vector3(-0.5f, -0.5f, 0);
        player.transform.localScale = new Vector3(1.53f, 1.53f, 1.53f);

        player.StopPlayerMove();
    }
    public void ContinuePlayerMove()
    {
        player.ContinuePlayerMove();
    }

}