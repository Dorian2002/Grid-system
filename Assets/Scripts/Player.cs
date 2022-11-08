using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int speed = 5;
    private Block[,] MyGrid;
    [SerializeField] private Stack<Move> _moves;

    private void Awake()
    {
        _moves = new Stack<Move>();
    }

    private void Update()
    {
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(new Vector3(x,y,0));

        if (Input.GetKeyDown(KeyCode.A) && _moves.Count > 0)
        {
            Move move = _moves.Peek();
            move._transform.position = move._position;
            _moves.Pop();
        }
        
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.3f, LayerMask.GetMask("Crate"));
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.3f, LayerMask.GetMask("Crate"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.3f, LayerMask.GetMask("Crate"));
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.3f, LayerMask.GetMask("Crate"));

        if (hitUp.collider)
        {
            if (MyGrid[(int)hitUp.transform.position.x, (int)hitUp.transform.position.y + 1].Type != 1 &&
                MyGrid[(int)hitUp.transform.position.x, (int)hitUp.transform.position.y + 1].Type != 2)
            {
                _moves.Push(new Move(hitUp.transform, hitUp.transform.position));
                hitUp.transform.position = new Vector3(hitUp.transform.position.x,hitUp.transform.position.y + 1,-0.01f);
            }
        }
        if (hitDown.collider)
        {
            if (MyGrid[(int)hitDown.transform.position.x, (int)hitDown.transform.position.y - 1].Type != 1 &&
                MyGrid[(int)hitDown.transform.position.x, (int)hitDown.transform.position.y - 1].Type != 2)
            {
                _moves.Push(new Move(hitDown.transform, hitDown.transform.position));
                hitDown.transform.position = new Vector3(hitDown.transform.position.x,hitDown.transform.position.y - 1,-0.01f);
            }
        }
        if (hitRight.collider)
        {
            if (MyGrid[(int)hitRight.transform.position.x + 1, (int)hitRight.transform.position.y].Type != 1 &&
                MyGrid[(int)hitRight.transform.position.x + 1, (int)hitRight.transform.position.y].Type != 2)
            {
                _moves.Push(new Move(hitRight.transform, hitRight.transform.position));
                hitRight.transform.position = new Vector3(hitRight.transform.position.x + 1,hitRight.transform.position.y,-0.01f);
            }
        }
        if (hitLeft.collider)
        {
            if (MyGrid[(int)hitLeft.transform.position.x - 1, (int)hitLeft.transform.position.y].Type != 1 &&
                MyGrid[(int)hitLeft.transform.position.x - 1, (int)hitLeft.transform.position.y].Type != 2)
            {
                _moves.Push(new Move(hitLeft.transform, hitLeft.transform.position));
                hitLeft.transform.position = new Vector3(hitLeft.transform.position.x - 1,hitLeft.transform.position.y,-0.01f);
            }
        }
    }

    public void SetMyGrid(Block[,] grid)
    {
        MyGrid = grid;
    }
}
