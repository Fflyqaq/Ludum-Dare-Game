/****************************************************
    文件：Tools.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 17:3:58
    功能：工具人类
*****************************************************/

using UnityEngine;

public class Tools : MonoBehaviour 
{
    /// <summary>
    /// 射线检测并绘制射线
    /// </summary>
    public static RaycastHit2D RayCheck(Vector2 initPos, Vector2 direction, float distance, LayerMask layer)
    {
        Vector2 pos = initPos;
        RaycastHit2D raycast = Physics2D.Raycast(pos, direction, distance, layer);
        //检测到为红色
        Color rayColor = raycast ? Color.red : Color.green;
        Debug.DrawRay(pos, direction, rayColor, distance);

        return raycast;
    }

    /// <summary>
    /// 根据关卡num计算要加载的场景名
    /// </summary>
    /// <param name="checkPointNum"></param>
    /// <returns></returns>
    public static string UseNumCalculateSceneName(int checkPointNum)
    {
        string sceneName = "";
        if (checkPointNum < 10)
        {
            sceneName = "0" + checkPointNum + "-Game";

        }
        else
        {
            sceneName =checkPointNum + "-Game";
        }

        return sceneName;
    }

    /// <summary>
    /// 计算现在最大的可玩关卡
    /// </summary>
    /// <returns></returns>
    public static string CalculateMaxCanPlay()
    {
        int maxCanPlay = 0;
        string checkPointStr = PlayerPrefs.GetString(ConstAttribute.checkPointDBMsg);
        for (int i = 0; i < checkPointStr.Length; i++)
        {
            if (checkPointStr[i] == '1')
            {
                maxCanPlay++;
            }
        }

        string maxCanPlaySceneName = UseNumCalculateSceneName(maxCanPlay);

        return maxCanPlaySceneName;
    }

    /// <summary>
    /// 计算现在最大的可玩关卡
    /// </summary>
    /// <returns></returns>
    public static int CalculateMaxCanPlayNum()
    {
        int maxCanPlay = 0;
        string checkPointStr = PlayerPrefs.GetString(ConstAttribute.checkPointDBMsg);
        for (int i = 0; i < checkPointStr.Length; i++)
        {
            if (checkPointStr[i] == '1')
            {
                maxCanPlay++;
            }
        }


        return maxCanPlay;
    }
}