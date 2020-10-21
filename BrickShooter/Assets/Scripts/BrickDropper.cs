using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDropper : MonoBehaviour
{
    public List<Brick> bricks = new List<Brick>();
    public static BrickDropper instace;
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

    public void DropBricks()
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            bricks[i].DropDown();
        }
    }
    public void RemoveBrick(Brick brick)
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].Equals(brick))
            {
                bricks.Remove(brick);
                if (bricks.Count == 0)
                {
                    UIService.instace.EnableGameWinPanel();
                }
                return;
            }
        }
    }
    public void DisableBrickColiders()
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            bricks[i].boxCollider.enabled = false;
        }
    }
    public void EnableBrickColiders()
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            bricks[i].boxCollider.enabled = true;
        }
    }


}
