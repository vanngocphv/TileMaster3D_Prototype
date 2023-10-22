using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer imageTop;
    [SerializeField] private SpriteRenderer imageBottom;

    [SerializeField] private int moveSpeed = 10;

    private int tileValue;
    private Vector3 targetPosition = Vector3.zero;
    private bool isSelected = false;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition != Vector3.zero && targetPosition != this.transform.position)
        {

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * moveSpeed);

            if (this.transform.position == targetPosition)
            {
                targetPosition = Vector3.zero;
            }
        }
    }

    // Move Tile
    public void SetTargetPosition(Vector3 targetPos)
    {
        this.targetPosition = targetPos;
    }

    // Set tile value when create in initial
    public void SetTileValue(int tileValue)
    {
        this.tileValue = tileValue;
    }
    public void SetTileImage(Sprite image)
    {
        imageTop.sprite = image;
        imageBottom.sprite = image;
    }
    public int GetTileValue()
    {
        return this.tileValue;
    }

    // is Selected?
    public void SetIsSelected()
    {
        this.isSelected = true;
    }
    public void ActiveKinematic()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
    }
    public bool IsSelected()
    {
        return this.isSelected;
    }

    // Hide/Show the game object if trigger
    public void HideGameObject() { 
        this.gameObject.SetActive(false); 
    }
    public void ShowGameOBject() { 
        this.gameObject.SetActive(true); 
    }
}
