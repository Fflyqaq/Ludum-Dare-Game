/****************************************************
    文件：ShootArrow.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/19 12:30:15
    功能：射箭
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class ShootArrow : MonoBehaviour 
{
	//弓箭和回合倒计时
	public Rigidbody2D arrow;

	public int countDowm;
	public Text txtCountDown;

	private bool isShooted = false;


	private void Start(){
		txtCountDown.text = countDowm.ToString();
	}
	private void Update(){
		
	}

	public void ReduceCountDown(int reduce = 1)
	{
		if (isShooted)
		{
			return;
		}
		countDowm -= reduce;
		txtCountDown.text = countDowm.ToString();
		if (countDowm <= 0)
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		arrow.velocity = new Vector2(-6, 0);
		//arrow.AddForce(new Vector2(-8, 0));

		isShooted = true;
	}
}