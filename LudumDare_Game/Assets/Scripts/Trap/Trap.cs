/****************************************************
    文件：Trap.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 19:8:40
    功能：陷阱
*****************************************************/

using UnityEngine;

public class Trap : TrapRoot
{
	private BoxCollider2D collTrap;

	private void Start(){
		collTrap = GetComponent<BoxCollider2D>();
		TrapStateChange();
	}
	

	public override void TriggerSwitch()
	{
		isCanWork = !isCanWork;

		TrapStateChange();
	}


	private void TrapStateChange()
	{
		if (isCanWork)
		{
			collTrap.enabled = true;
			GetComponent<SpriteRenderer>().sprite = ResService.Instance.LoadSprite(ConstAttribute.trapOnSpritePath, true);

		}
		else
		{
			collTrap.enabled = false;
			GetComponent<SpriteRenderer>().sprite = ResService.Instance.LoadSprite(ConstAttribute.trapOffSpritePath, true);
		}
	}
}