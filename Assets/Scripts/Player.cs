using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int speed = 5;
    private Block[,] MyGrid;
    
    private void Update()
    {
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(new Vector3(x,y,0));
        
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.3f, LayerMask.GetMask("Crate"));
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.3f, LayerMask.GetMask("Crate"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.3f, LayerMask.GetMask("Crate"));
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.3f, LayerMask.GetMask("Crate"));

        if (hitUp.collider)
        {
            if (MyGrid[(int)hitUp.transform.position.x, (int)hitUp.transform.position.y + 1].Type != 1 &&
                MyGrid[(int)hitUp.transform.position.x, (int)hitUp.transform.position.y + 1].Type != 2)
            {
                hitUp.transform.position = new Vector3(hitUp.transform.position.x,hitUp.transform.position.y + 1,-0.01f); 
            }
        }
        if (hitDown.collider)
        {
            if (MyGrid[(int)hitDown.transform.position.x, (int)hitDown.transform.position.y - 1].Type != 1 &&
                MyGrid[(int)hitDown.transform.position.x, (int)hitDown.transform.position.y - 1].Type != 2)
            {
                hitDown.transform.position = new Vector3(hitDown.transform.position.x,hitDown.transform.position.y - 1,-0.01f);
            }
        }
        if (hitRight.collider)
        {
            if (MyGrid[(int)hitRight.transform.position.x + 1, (int)hitRight.transform.position.y].Type != 1 &&
                MyGrid[(int)hitRight.transform.position.x + 1, (int)hitRight.transform.position.y].Type != 2)
            {
                hitRight.transform.position = new Vector3(hitRight.transform.position.x + 1,hitRight.transform.position.y,-0.01f);
            }
        }
        if (hitLeft.collider)
        {
            if (MyGrid[(int)hitLeft.transform.position.x - 1, (int)hitLeft.transform.position.y].Type != 1 &&
                MyGrid[(int)hitLeft.transform.position.x - 1, (int)hitLeft.transform.position.y].Type != 2)
            {
                hitLeft.transform.position = new Vector3(hitLeft.transform.position.x - 1,hitLeft.transform.position.y,-0.01f);
            }
        }
    }
    
    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.CompareTag("Crate"))
    //    {
    //        if (col.transform.position.x <= transform.position.x && transform.position.y > col.transform.position.y -0.5f && transform.position.y < col.transform.position.y +0.5f)
    //        {
    //            col.transform.position = new Vector3(col.transform.position.x - 1,col.transform.position.y,col.transform.position.z);
    //        }
    //        else if (col.transform.position.x >= transform.position.x && transform.position.y > col.transform.position.y -0.5f && transform.position.y < col.transform.position.y +0.5f)
    //        {
    //            col.transform.position = new Vector3(col.transform.position.x + 1,col.transform.position.y,col.transform.position.z);
    //        }
    //        else if (col.transform.position.y <= transform.position.y && transform.position.x > col.transform.position.x -0.5f && transform.position.x < col.transform.position.x +0.5f)
    //        {
    //            col.transform.position = new Vector3(col.transform.position.x,col.transform.position.y - 1,col.transform.position.z);
    //        }
    //        else if (col.transform.position.y >= transform.position.y && transform.position.x > col.transform.position.x -0.5f && transform.position.x < col.transform.position.x +0.5f)
    //        {
    //            col.transform.position = new Vector3(col.transform.position.x,col.transform.position.y + 1,col.transform.position.z);
    //        }
    //    }
    //}
//
    public void SetMyGrid(Block[,] grid)
    {
        MyGrid = grid;
    }
}
