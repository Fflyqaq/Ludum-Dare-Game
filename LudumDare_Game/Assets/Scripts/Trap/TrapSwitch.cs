/****************************************************
    文件：switch.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 17:46:49
    功能：开关，可以打开对应陷阱
*****************************************************/

using UnityEngine;

public class TrapSwitch : MonoBehaviour 
{
	public Trap trap; 

	private void Start(){
		
	}
	private void Update(){
		
	}

	public void OpenSwitch()
	{
		trap.TriggerTrap();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//player
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box")
		{
			OpenSwitch();

			//PlayGameSystem.Instance.ShowNextTalk();
		}
	}
}