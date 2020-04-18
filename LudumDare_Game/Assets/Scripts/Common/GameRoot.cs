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
    }

    private void Init()
    {
        ResService resService = GetComponent<ResService>();
        resService.InitSvc();

        PlayGameSystem gameSystem = GetComponent<PlayGameSystem>();
        gameSystem.InitSys();
        EnterExitSystem loginExitSystem = GetComponent<EnterExitSystem>();
        loginExitSystem.InitSys();

        SceneManager.LoadScene(ConstAttribute.loginSceneName);
    }
}
