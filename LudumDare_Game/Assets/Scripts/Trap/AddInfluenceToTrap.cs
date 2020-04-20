/****************************************************
    文件：AddInfluenceToTrap.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/19 17:34:20
    功能：向触发陷阱时添加   (触发对话影响)
*****************************************************/

using UnityEngine;

public class AddInfluenceToTrap : MonoBehaviour 
{
	private bool isTriggered = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//被触发时候会显示对话，只会显示一次
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box")
		{
			if (isTriggered == false)
			{
				BattleSystem.Instance.ShowNextTalk();
				isTriggered = true;
			}
		}
	}
}