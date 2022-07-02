using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTile : MonoBehaviour
{
    public List<Sprite> sprite = new List<Sprite>();
    SpriteRenderer spriteRenderer;
    void Start()
    {
        //Grass tile randomisation
        int i = Random.Range(0, sprite.Count);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite[i];
    }
}
