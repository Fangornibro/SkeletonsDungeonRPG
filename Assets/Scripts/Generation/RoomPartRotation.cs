using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPartRotation : MonoBehaviour
{
    public enum Orientation { Bottom, Left, Right, Top }
    public Orientation orientation;

    public int getSide()
    {
        if (orientation == Orientation.Bottom)
        {
            return 0;
        }
        else if (orientation == Orientation.Left)
        {
            return 1;
        }
        else if (orientation == Orientation.Right)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
