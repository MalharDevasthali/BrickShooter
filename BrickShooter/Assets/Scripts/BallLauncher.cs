using System;
using UnityEngine;
using System.Threading.Tasks;

public class BallLauncher : MonoBehaviour
{


    [SerializeField] private Ball ball;
    [SerializeField] private BallsPool ballsPool;
    [SerializeField] private Transform PoolParent;

    private Vector3 worldPosition;
    private Vector3 startDragPosition;
    private Vector3 endDragPosition;

    private LinePreview linePreview;
    public bool canSpawnBalls = true;
    public int ballsOutOfScreen = 0;
    public int ballsReleased = 0;



    public static BallLauncher instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }


    }

    void Start()
    {
        linePreview = GetComponent<LinePreview>();
        worldPosition.z = 0;
    }

    void Update()
    {
        if (!canSpawnBalls) return;


        worldPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            startDrag(worldPosition);
        }
        else if (Input.GetMouseButton(0))
        {
            continueDrag(worldPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endDrag();
        }
    }

    private void startDrag(Vector3 worldPosition)
    {
        startDragPosition = worldPosition;
        linePreview.SetStartPosition(transform.position);
    }
    private void continueDrag(Vector3 worldPosition)
    {
        endDragPosition = worldPosition;
        Vector3 dir = endDragPosition - startDragPosition;
        linePreview.SetEndPosition(transform.position - dir);

    }
    private void endDrag()
    {
        canSpawnBalls = false;
        ballsOutOfScreen = 0;
        ballsReleased = 0;
        Vector3 direction = endDragPosition - startDragPosition;

        if (direction == Vector3.zero) //when there is no drag
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            direction = target - transform.position;
            direction = -direction;
        }
        direction.Normalize();
        linePreview.ClearLine();
        ShootBalls(direction);
    }
 
    private async void ShootBalls(Vector3 direction)
    {
        for (int i = 0; i < BallsPool.instace.BallsCountForLaunch; i++)
        {
            ballsReleased++;
            Ball _ball;
            if (ballsPool.GetPooledItemsList().Count > 0)
            {
                _ball = ballsPool.GetItemFromPool();

                if (IsBallPresentInPool(_ball))
                {
                    _ball.transform.position = transform.position;
                }
                else
                {
                    _ball = CreateNewBall();
                    if (i == ballsPool.BallsCountForLaunch - 1)
                    {
                        _ball.lastBall = true;
                    }
                }
            }
            else //for very first ball
            {
                _ball = CreateNewBall();
            }

            _ball.GetRigidbody2D().AddForce(-direction);
            await Task.Delay(25);
        }
    }

    private bool IsBallPresentInPool(Ball _ball)
    {
        return _ball != null && BallsPool.instace.GetPooledItemsList().Count >= BallsPool.instace.BallsCountForLaunch;
    }

    private Ball CreateNewBall()
    {
        Debug.Log("Creating New Ball");
        Ball _ball;
        _ball = Instantiate(ball, transform.position, Quaternion.identity);
        ballsPool.AddIntoPool(_ball);
        _ball.transform.SetParent(PoolParent);
        return _ball;
    }

}
