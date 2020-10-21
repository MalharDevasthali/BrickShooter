using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallsPool : MonoBehaviour
{
    public int BallsCountForLaunch;
    [SerializeField] public TextMeshProUGUI textForShootBallsCount;
    private List<BallItem> ballList = new List<BallItem>();
    public static BallsPool instace;
    private void Awake()
    {
        if (instace != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instace = this;
        }
    }
    private void Update()
    {
        textForShootBallsCount.text = Mathf.Abs((BallsCountForLaunch - BallLauncher.instance.ballsReleased)).ToString();
    }
    private void Start()
    {
        textForShootBallsCount.text = BallsCountForLaunch.ToString();
    }
    public void AddIntoPool(Ball ball)
    {
        BallItem newBall = new BallItem();
        newBall.ball = ball;
        newBall.isAvailable = false;
        ballList.Add(newBall);
    }
    public void DropAllBalls()
    {
        Debug.Log(ballList.Count);
        for (int i = 0; i < ballList.Count; i++)
        {
            ballList[i].ball.GetRigidbody2D().gravityScale = 10;
        }
    }

    public void ModifyAvaiablity(Ball ball, bool availablity)
    {
        for (int i = 0; i < ballList.Count; i++)
        {
            if (ballList[i].ball.Equals(ball))
            {
                ballList[i].isAvailable = availablity;
            }
        }
    }
    public Ball GetItemFromPool()
    {
        for (int i = 0; i < ballList.Count; i++)
        {
            if (ballList[i].isAvailable == true)
            {
                ballList[i].isAvailable = false;
                return ballList[i].ball;
            }
        }
        return null;
    }
    public List<BallItem> GetPooledItemsList()
    {
        return ballList;
    }
    public class BallItem
    {
        public bool isAvailable;
        public Ball ball;
    }
}
