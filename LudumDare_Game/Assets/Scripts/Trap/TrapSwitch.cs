/****************************************************
    文件：switch.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 17:46:49
    功能：开关，可以打开对应陷阱
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class TrapSwitch : MonoBehaviour 
{
	public List<TrapRoot> traps = new List<TrapRoot>();

	private bool isOpen = false;

	private void Start(){
		
	}
	private void Update(){
		
	}

	/// <summary>
	/// 打开开关，改变陷阱
	/// </summary>
	public void OpenSwitch()
	{
		foreach (var item in traps)
		{
			item.TriggerSwitch();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//player
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box" || collision.gameObject.tag == "Rabbit")
		{
			OpenSwitch();

			isOpen = !isOpen;
			ChangeSprite(isOpen);
		}
	}

	private void ChangeSprite(bool open)
	{
		if (open)
		{
			GetComponent<SpriteRenderer>().sprite = ResService.Instance.LoadSprite(ConstAttribute.switchOnSpritePath, true);
		}
		else
		{
			GetComponent<SpriteRenderer>().sprite = ResService.Instance.LoadSprite(ConstAttribute.switchOffSpritePath, true);
		}
	}
}