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
	//一个开关最多只触发一次对话剧情
	private bool isTriggered = false;

	public Tags tag1;
	public Tags tag2;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//被触发时候会显示对话，只会显示一次
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box" || collision.gameObject.tag == "Rabbit")
		{
			if (isTriggered == false)
			{
				BattleSystem.Instance.ShowNextTalk();
				isTriggered = true;

				MapManager.Instance.CheckIfPassGame();
			}
		}

		//string tagStr1 = tag1.ToString();
		//string tagStr2 = tag2.ToString();

		////被触发时候会显示对话，只会显示一次
		//if (collision.gameObject.tag == tagStr1 || collision.gameObject.tag == tagStr2)
		//{
		//	if (isTriggered == false)
		//	{
		//		BattleSystem.Instance.ShowNextTalk();
		//		isTriggered = true;

		//		MapManager.Instance.CheckIfPassGame();
		//	}
		//}
	}
}