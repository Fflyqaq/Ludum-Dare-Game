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
	private LayerMask collLayerMask = 1 << 8;
	private LayerMask trapLayerMask = 1 << 10;
	private float collLayerMaskDistance = 0.7f;
	private float trapLayerMaskDistance = 0.7f;

	private void Start(){
		
	}
	private void Update(){
		
	}

	public bool BoxMove(float x, float y)
	{
		//箱子要移动的位置不能有其他coll和陷阱
		RaycastHit2D forwardCollRay = Tools.RayCheck(transform.position, new Vector2(x, y), collLayerMaskDistance, collLayerMask);
		RaycastHit2D forwardTrapRay = Tools.RayCheck(transform.position, new Vector2(x, y), trapLayerMaskDistance, trapLayerMask);
		if (!forwardCollRay && !forwardTrapRay)
		{
			transform.position = new Vector3(transform.position.x + x, transform.position.y + y, 0);
			return true;
		}

		return false;
	}


}