/****************************************************
    文件：Box.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 19:14:57
    功能：箱子
*****************************************************/

using UnityEngine;

public class Box : MonoBehaviour 
{

	private void Start(){
		
	}
	private void Update(){
		
	}

	public void BoxMove(float x, float y)
	{
		transform.position = new Vector3(transform.position.x + x, transform.position.y + y, 0);
	}

}