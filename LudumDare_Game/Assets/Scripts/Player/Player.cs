/****************************************************
    文件：test.cs
    作者：Ffly
    邮箱: jitengfeiwork@gmail.com
    日期：2020/4/18 13:12:54
    功能：玩家
*****************************************************/

using UnityEngine;

public class Player : MonoBehaviour
{
    #region 移动相关
    private float xMoveDistance = 1;
    private float yMoveDistance = 1;
    private bool isCanMove = true;

    //Collider碰撞层，Box层，射线检测距离
    private LayerMask collLayerMask;
    private LayerMask boxLayerMask;
    private float checkCollDistance = 1; 
    private float checkBoxDistance = 0.5f; 
    #endregion

    void Start()
    {
        //第八层
        collLayerMask = 1 << 8;
        boxLayerMask = 1 << 9;

    }

    void Update()
    {
        PlayerWSAD();
    }

    private void PlayerWSAD()
    {
        if (Input.GetKeyDown(KeyCode.W) && CheckIfCanMove(1))
        {
            //Vector3 newPos = new Vector3(transform.position.x, transform.position.y + yMoveDistance, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y + yMoveDistance, 0);

            CheckIfCanMoveBox(1);
        }
        else if (Input.GetKeyDown(KeyCode.S) && CheckIfCanMove(2))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - yMoveDistance, 0);

            CheckIfCanMoveBox(2);
        }
        else if (Input.GetKeyDown(KeyCode.A) && CheckIfCanMove(3))
        {
            transform.position = new Vector3(transform.position.x - xMoveDistance, transform.position.y, 0);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            CheckIfCanMoveBox(3);
        }
        else if (Input.GetKeyDown(KeyCode.D) && CheckIfCanMove(4))
        {
            transform.position = new Vector3(transform.position.x + xMoveDistance, transform.position.y, 0);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            CheckIfCanMoveBox(4);
        }
    }

    /// <summary>
    /// 射线检测是否有Collider
    /// </summary>
    /// <param name="checkNum">1234为wsad方向</param>
    private bool CheckIfCanMove(int checkNum)
    {
        switch (checkNum)
        {
            case 1:
                RaycastHit2D upRay = Tools.RayCheck(transform.position, Vector2.up, checkCollDistance, collLayerMask);
                return upRay?false:true;
            case 2:
                RaycastHit2D downRay = Tools.RayCheck(transform.position, Vector2.down, checkCollDistance, collLayerMask);
                return downRay ? false:true;
            case 3:
                RaycastHit2D leftRay = Tools.RayCheck(transform.position, Vector2.left, checkCollDistance, collLayerMask);
                return leftRay ? false:true;
            case 4:
                RaycastHit2D rightRay = Tools.RayCheck(transform.position, Vector2.right, checkCollDistance, collLayerMask);
                return rightRay ? false:true;
            default:
                return false;
        }
    }

    /// <summary>
    /// 射线检测是否有箱子可以推
    /// </summary>
    /// <param name="checkNum">1234为wsad方向</param>
    private bool CheckIfCanMoveBox(int checkNum)
    {
        switch (checkNum)
        {
            case 1:
                RaycastHit2D upRay = Tools.RayCheck(transform.position, Vector2.up, checkBoxDistance, boxLayerMask);
                if (upRay)
                {
                    upRay.transform.GetComponent<Box>().BoxMove(0,1);
                }
                return upRay ? false : true;
            case 2:
                RaycastHit2D downRay = Tools.RayCheck(transform.position, Vector2.down, checkBoxDistance, boxLayerMask);
                if (downRay)
                {
                    downRay.transform.GetComponent<Box>().BoxMove(0, -1);
                }
                return downRay ? false : true;
            case 3:
                RaycastHit2D leftRay = Tools.RayCheck(transform.position, Vector2.left, checkBoxDistance, boxLayerMask);
                if (leftRay)
                {
                    leftRay.transform.GetComponent<Box>().BoxMove(-1, 0);
                }
                return leftRay ? false : true;
            case 4:
                RaycastHit2D rightRay = Tools.RayCheck(transform.position, Vector2.right, checkBoxDistance, boxLayerMask);
                if (rightRay)
                {
                    rightRay.transform.GetComponent<Box>().BoxMove(1, 0);
                }
                return rightRay ? false : true;
            default:
                return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //陷阱
        if (collision.gameObject.tag == "Trap")
        {
            Debug.Log("你死了");
            Destroy(gameObject);
        }

        //开关
        if (collision.gameObject.tag == "Switch")
        {
            Debug.Log("开关");
            collision.gameObject.GetComponent<TrapSwitch>().OpenSwitch();
        }
    }

    public void StopPlayerMove()
    {
        isCanMove = false;
    }
    public void ContinuePlayerMove()
    {
        isCanMove = true;
    }
}
