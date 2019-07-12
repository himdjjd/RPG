using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathState : IState
{
    private Stack<Vector3> path;

    private Vector3 destination;

    private Vector3 current;

    private Vector3 goal;

    private Transform transform;

    private Enemy parent;

    private Vector3 targetPos;

    public void Enter(Enemy parent)
    {
        this.parent = parent;

        this.transform = parent.transform.parent;

        targetPos = Player.MyInstance.MyCurrentTile.position;

        if (targetPos != parent.MyCurrentTile.position) //Makes sure that we aren't standing on the target position
        {
            path = parent.MyAstar.Algorithm(parent.MyCurrentTile.position, targetPos);
        }
        if (path != null)
        {
            current = path.Pop();
            destination = path.Pop();
            this.goal = parent.MyCurrentTile.position;
        }
        else
        {
            parent.ChangeState(new EvadeState());
        }
        


    }

    public void Exit()
    {
        path = null;
    }

    public void Update()
    {
        if (path != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, 2 * Time.deltaTime);

           
            Vector3Int dest = parent.MyAstar.MyTilemap.WorldToCell(destination);
            Vector3Int cur = parent.MyAstar.MyTilemap.WorldToCell(current);

            float distance = Vector2.Distance(destination, transform.position);

            if (cur.y > dest.y)
            {
                parent.Direction = Vector2.down;
            }
            else if (cur.y < dest.y)
            {
                parent.Direction = Vector2.up;
            }
            if (cur.y == dest.y)
            {
                if (cur.x > dest.x)
                {
                    parent.Direction = Vector2.left;
                }
                else if (cur.x < dest.x)
                {
                    parent.Direction = Vector2.right;
                }
            }
            if (distance <= 0f)
            {
                if (path.Count > 0)
                {
                    current = destination;
                    destination = path.Pop();

                    if (targetPos != Player.MyInstance.MyCurrentTile.position) //Then the player has moved
                    {
                        parent.ChangeState(new PathState());
                    }
                }
                else
                {
                    path = null;

                    parent.ChangeState(new PathState());
                }
            }
        }
    }
}
