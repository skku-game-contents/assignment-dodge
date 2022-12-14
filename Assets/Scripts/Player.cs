using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;

    public float speed;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletobj;

    public GameManager manager;

    public DeleteObject delete;

    [SerializeField] private Image canvasImage;

    private bool isTouchingTop = false;
    private bool isTouching = false;
    private bool isPressing = false;

    private void OnEnable()
    {
        GameManager.LevelChanged += OnLevelChanged;
        
    }

    void Update()
    {
        Move();
        Fire();
        Reload();
        SetBackgroundImage();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;
    }

    void Fire()
    {
        if(!Input.GetButton("Fire1"))
           return;

        if(curShotDelay < maxShotDelay)
            return;

        GameObject bullet = Instantiate(bulletobj, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up*10, ForceMode2D.Impulse);

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border"){
            switch(collision.gameObject.name){
                case "Top":
                    isTouchTop = true;
                    isTouching = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    isTouching = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
        else if(collision.CompareTag("Enemy"))
        {
            manager.RespawnPlayer();
            gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch(item.type){
                case "deleteteam":
                    delete.DestroyObj();
                    break;
            }
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border"){ 
            isTouching = false;
            switch(collision.gameObject.name){
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }

    private void SetBackgroundImage()
    {
        if (!isTouching || (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))) return;

        if (isTouchTop)
        {
            canvasImage.rectTransform.SetTop(-canvasImage.rectTransform.offsetMax.y + 1);
            canvasImage.rectTransform.SetBottom(canvasImage.rectTransform.offsetMin.y - 1);
        }
        if (isTouchBottom)
        {
            canvasImage.rectTransform.SetTop(-canvasImage.rectTransform.offsetMax.y - 1);
            canvasImage.rectTransform.SetBottom(canvasImage.rectTransform.offsetMin.y + 1);
        }
    }

    private void OnLevelChanged()
    {
        speed = LevelController.main.GetPlayerSpeed();
    }
}
