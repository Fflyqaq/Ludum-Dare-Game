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
	//一个开关最多只触发一次
	private bool isTriggered = false;
	//是否是触发游戏结束的
	public bool isTriggerEndGame = false;

	public Tags tag1;
	public Tags tag2;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//这关是否玩过
		bool isPlayed = false;
		isPlayed = Tools.CheckIfPlayedThisCheckPoint(BattleSystem.Instance.NowCheckPointNum);


		//玩过这关(只有玩过，endgame才有用)
			//结束游戏
			//什么都不发生
		//没玩过
			//正常
		if (isPlayed)
		{
			if (collision.gameObject.tag == tag1.ToString() || collision.gameObject.tag == tag2.ToString())
			{
				if (isTriggered == false)
				{
					if (isTriggerEndGame)
					{
						BattleSystem.Instance.IsWin = true;
					}
					else
					{


					}
				}
			}
		}
		else
		{
			if (collision.gameObject.tag == tag1.ToString() || collision.gameObject.tag == tag2.ToString())
			{
				if (isTriggered == false)
				{
					BattleSystem.Instance.ShowNextTalk();
					isTriggered = true;

					MapManager.Instance.CheckIfPassGame();
				}
			}
		}
	}
}