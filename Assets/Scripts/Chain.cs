using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chain : MonoBehaviour
{
    private PlayerController player;

    // Global time at object instantiation
    private float startTime;
    // How long does it take for a chain segment to appear, in seconds
    public float chainSpeed;
    // How many additional chain segments do we have active now?
    private int chainSegments = 0;

    public GameObject link1;
    public GameObject link2;
    public GameObject link3;
    public GameObject link4;
    public GameObject link5;
    public GameObject link6;
    public GameObject link7;
    public GameObject link8;
    public GameObject link9;
    public GameObject link10;

    private GameObject[] chainSegmentArray;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject.GetComponent<PlayerController>();
        player.curChains++;

        startTime = Time.time;
        chainSegmentArray = new GameObject[] { link1, link2, link3, link4, link5, link6, link7, link8, link9, link10 };
    }

    // Update is called once per frame
    void Update()
    {
        // Update number of currently existing chains
        if (player.chainDir.x == 0 && player.chainDir.y == 0)
        {
            player.curChains--;
            Destroy(this.gameObject);
        }

        // Update rotation and offset
        float rotation = Mathf.Atan2(player.chainDir.y, player.chainDir.x) * Mathf.Rad2Deg;
        float offsetX = player.chainDir.x * player.offset;
        float offsetY = player.chainDir.y * player.offset;
        this.transform.rotation = Quaternion.Euler(0,0,rotation);
        this.transform.localPosition = new Vector3(offsetX, offsetY, 0);

        // Update chain length
        // How many chain segments are we allowed to have active, by time?
        int chainsByTime = Convert.ToInt32((Time.time - startTime) / chainSpeed);
        
        // If we are allowed more segments by time and we still have remaining segments to add
        if (chainSegments < chainsByTime && chainSegments < chainSegmentArray.Length)
        {
            chainSegmentArray[chainSegments].SetActive(true);
            chainSegments++;
        }
    }
}
