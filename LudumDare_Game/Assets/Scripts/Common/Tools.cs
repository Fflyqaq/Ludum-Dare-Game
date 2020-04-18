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

        Color rayColor = raycast ? Color.red : Color.green;
        Debug.DrawRay(pos, direction, rayColor, distance);

        return raycast;
    }
}