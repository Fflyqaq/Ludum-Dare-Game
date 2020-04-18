/****************************************************
    文件：Rabbit.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 17:27:37
    功能：兔子
*****************************************************/

using UnityEngine;

public class Rabbit : MonoBehaviour 
{
	private void Start(){
		
	}
	private void Update(){
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Trap")
		{
			Debug.Log("你死了");
			Destroy(this);
		}
	}
}