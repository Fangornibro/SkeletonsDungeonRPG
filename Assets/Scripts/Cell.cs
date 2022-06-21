using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int x, y;
    public Item item;
    public Cell(int X, int Y, Item Item)
    {
        x = X;
        y = Y;
        item = Item;
    }
}
