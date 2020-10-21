using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Brick : MonoBehaviour
{
    [SerializeField] private int countOnBrick; //this value is rondomized in Start
    [SerializeField] TextMeshPro countText;
    private List<Color> colors = new List<Color>();
    private Color baseColor;
    private SpriteRenderer spriteRenderer;

    public BoxCollider2D boxCollider;
    private void Start()
    {
        countOnBrick = UnityEngine.Random.Range(30, 50);
        countText.text = countOnBrick.ToString();
        spriteRenderer = GetComponent<SpriteRenderer>();

        RandomizeColor();

    }

    private void RandomizeColor()
    {
        colors.Add(Color.red);
        colors.Add(Color.blue);
        colors.Add(Color.green);

        int rand = UnityEngine.Random.Range(0, 3);
        baseColor = colors[rand];
        spriteRenderer.color = baseColor;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (countOnBrick > 1)
        {
            countOnBrick--;
            countText.text = countOnBrick.ToString();
            spriteRenderer.color = Color.Lerp(Color.white, baseColor, countOnBrick / 10f);
        }
        else
        {
            BrickDropper.instace.RemoveBrick(this);
            Destroy(gameObject);
        }


    }

    public void DropDown()
    {
        transform.position += new Vector3(0f, -0.5f, 0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            if (!UIService.instace.GameOverPanel.activeSelf)
            {
                UIService.instace.EnableGameOverPanel();
            }
        }
    }



}
