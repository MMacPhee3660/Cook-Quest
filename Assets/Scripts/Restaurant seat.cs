using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Restaurantseat : MonoBehaviour
{

    public bool isOccupied;
    public Item orderedItem;
    public SpriteRenderer spriteRenderer;
    private float timeToLeave = 0;
    private float timeWaited = 0f;
    public Sprite customerSprite;
    void Start()
    {
        timeToLeave = UnityEngine.Random.Range(4f, 6f);
    }

    void Update()
    {
        if (isOccupied)
        {
            timeWaited += Time.deltaTime;
            if (timeWaited >= timeToLeave)
            {
                timeToLeave = UnityEngine.Random.Range(7f, 12f);
                timeWaited = 0f;
                Leave();
                }
        }
    }
    public void Leave()
    {
        isOccupied = false;
        orderedItem = null;
        spriteRenderer.sprite = customerSprite;
    }
}
