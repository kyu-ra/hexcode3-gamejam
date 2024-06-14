using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Ball animation and movement
    private Vector2 moveVal;
    public float speedModifier;
    public Animator ball_anim;

    // Chain animation and movement
    public GameObject chain;
    public float offset;
    public Vector2 chainDir;
    public int curChains;
    public int maxChains = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnMove(InputValue value)
    {
        moveVal = value.Get<Vector2>();
        
    }

    void OnShoot(InputValue value)
    {
        chainDir = value.Get<Vector2>();

        if (curChains < maxChains)
        {           
            float rotation = Mathf.Atan2(chainDir.y, chainDir.x) * Mathf.Rad2Deg;
            float offsetX = chainDir.x * offset;
            float offsetY = chainDir.y * offset;
            
            Instantiate(chain, new Vector3(offsetX, offsetY, 0),
                Quaternion.Euler(0,0,rotation), transform);            
        }
    }
      
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(moveVal.x, moveVal.y, 0) * speedModifier * Time.deltaTime);
        ball_anim.speed = speedModifier * (Mathf.Abs(moveVal.x) + Mathf.Abs(moveVal.y));
    }
}
