using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int speed = 5;
    private Block[,] MyGrid;
    [SerializeField] private Stack<Move> _moves;
    private Crate grabbedObj;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _moves = new Stack<Move>();
    }

    private void Update()
    {
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, LayerMask.GetMask("Crate"));
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, LayerMask.GetMask("Crate"));
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, LayerMask.GetMask("Crate"));
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, LayerMask.GetMask("Crate"));

        if (x != 0 || y != 0)
        {
            anim.SetBool("walk",true);
        }
        else
        {
            anim.SetBool("walk", false);
        }
        
        transform.Translate(new Vector3(x,y,0));

        if (Input.GetKeyDown(KeyCode.A) && _moves.Count > 0)
        {
            Move move = _moves.Peek();
            move._transform.position = move._position;
            _moves.Pop();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!grabbedObj)
            {
                if (hitUp.collider)
                {
                    grabbedObj = hitUp.transform.GetComponent<Crate>();
                    _moves.Push(new Move(grabbedObj.transform, grabbedObj.transform.position));
                    grabbedObj.transform.SetParent(transform);
                }
                else if (hitDown.collider)
                {
                    grabbedObj = hitDown.transform.GetComponent<Crate>();
                    _moves.Push(new Move(grabbedObj.transform, grabbedObj.transform.position));
                    grabbedObj.transform.SetParent(transform);
                }
                else if (hitRight.collider)
                {
                    grabbedObj = hitRight.transform.GetComponent<Crate>();
                    _moves.Push(new Move(grabbedObj.transform, grabbedObj.transform.position));
                    grabbedObj.transform.SetParent(transform);
                }
                else if (hitLeft.collider)
                {
                    grabbedObj = hitLeft.transform.GetComponent<Crate>();
                    _moves.Push(new Move(grabbedObj.transform, grabbedObj.transform.position));
                    grabbedObj.transform.SetParent(transform);
                }
            }
            else
            {
                if (MyGrid[(int)Mathf.Round(grabbedObj.transform.position.x), (int)Mathf.Round(grabbedObj.transform.position.y)].Type != 1 &&
                    MyGrid[(int)Mathf.Round(grabbedObj.transform.position.x), (int)Mathf.Round(grabbedObj.transform.position.y)].Type != 2)
                {
                    grabbedObj.transform.SetParent(GameManager.GM.transform);
                    grabbedObj.transform.position = new Vector2(Mathf.Round(grabbedObj.transform.position.x), Mathf.Round(grabbedObj.transform.position.y));
                    grabbedObj = null;
                }
            }
        }

        if (hitUp.collider && hitUp.distance < 0.2f)
        {
            if (MyGrid[(int)hitUp.transform.position.x, (int)hitUp.transform.position.y + 1].Type != 1 &&
                MyGrid[(int)hitUp.transform.position.x, (int)hitUp.transform.position.y + 1].Type != 2)
            {
                _moves.Push(new Move(hitUp.transform, hitUp.transform.position));
                hitUp.transform.position = new Vector2(hitUp.transform.position.x,hitUp.transform.position.y + 1);
            }
        }
        else if (hitDown.collider && hitDown.distance < 0.2f)
        {
            if (MyGrid[(int)hitDown.transform.position.x, (int)hitDown.transform.position.y - 1].Type != 1 &&
                MyGrid[(int)hitDown.transform.position.x, (int)hitDown.transform.position.y - 1].Type != 2)
            {
                _moves.Push(new Move(hitDown.transform, hitDown.transform.position));
                hitDown.transform.position = new Vector2(hitDown.transform.position.x,hitDown.transform.position.y - 1);
            }
        }
        else if (hitRight.collider && hitRight.distance < 0.2f)
        {
            if (MyGrid[(int)hitRight.transform.position.x + 1, (int)hitRight.transform.position.y].Type != 1 &&
                MyGrid[(int)hitRight.transform.position.x + 1, (int)hitRight.transform.position.y].Type != 2)
            {
                _moves.Push(new Move(hitRight.transform, hitRight.transform.position));
                hitRight.transform.position = new Vector2(hitRight.transform.position.x + 1,hitRight.transform.position.y);
            }
        }
        else if (hitLeft.collider && hitLeft.distance < 0.2f)
        {
            if (MyGrid[(int)hitLeft.transform.position.x - 1, (int)hitLeft.transform.position.y].Type != 1 &&
                MyGrid[(int)hitLeft.transform.position.x - 1, (int)hitLeft.transform.position.y].Type != 2)
            {
                _moves.Push(new Move(hitLeft.transform, hitLeft.transform.position));
                hitLeft.transform.position = new Vector2(hitLeft.transform.position.x - 1,hitLeft.transform.position.y);
            }
        }
    }

    public void SetMyGrid(Block[,] grid)
    {
        MyGrid = grid;
    }
}
