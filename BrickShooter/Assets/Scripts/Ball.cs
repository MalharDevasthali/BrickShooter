using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Ball : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    private Rigidbody2D rb2d;
    public bool lastBall = false;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = rb2d.velocity.normalized * moveSpeed;
    }
    public Rigidbody2D GetRigidbody2D()
    {
        return rb2d;
    }
    private async void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Down"))
        {
            rb2d.gravityScale = 0;
            BallsPool.instace.ModifyAvaiablity(this, true);
            rb2d.velocity = Vector3.zero;


            await Task.Delay(50);
            BallLauncher.instance.ballsOutOfScreen++;
            BallLauncher.instance.ballsReleased--;


            if (BallLauncher.instance.ballsOutOfScreen == BallsPool.instace.BallsCountForLaunch)
            {
                BallLauncher.instance.canSpawnBalls = true;
                BrickDropper.instace.DropBricks();
                BrickDropper.instace.EnableBrickColiders();
            }
        }
    }
}
