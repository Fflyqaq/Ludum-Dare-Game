/****************************************************
    文件：test.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 13:08:42
    功能：Root
*****************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance = null;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        Init();

        //PlayerPrefs.SetString(ConstAttribute.checkPointDBName, ConstAttribute.checkPointDBMsg);
        //PlayerPrefs.SetInt(ConstAttribute.maxCheckPointDBName, ConstAttribute.maxCheckPointDBMsg);

        //初始化关卡信息，再次启动，不再重新初始化
        if (PlayerPrefs.GetInt("IsSettedCheckPoint") == 0)
        {
            PlayerPrefs.SetString(ConstAttribute.checkPointDBName, ConstAttribute.checkPointDBMsg);
            PlayerPrefs.SetInt(ConstAttribute.maxCheckPointDBName, ConstAttribute.maxCheckPointDBMsg);

            PlayerPrefs.SetInt("IsSettedCheckPoint", 1);
        }
    }

    private void Init()
    {
        ResService resService = GetComponent<ResService>();
        resService.InitSvc();
        AudioService audioService = GetComponent<AudioService>();
        audioService.InitSvc();

        BattleSystem gameSystem = GetComponent<BattleSystem>();
        gameSystem.InitSys();
        EnterExitSystem loginExitSystem = GetComponent<EnterExitSystem>();
        loginExitSystem.InitSys();

        SceneManager.LoadScene(ConstAttribute.loginSceneName);
    }
}
