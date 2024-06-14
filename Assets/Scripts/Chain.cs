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
    // Link prefab
    public GameObject link;
    // Maximum links after the base link
    public int linkMax;
    // Transform position offset so links line up properly
    public float linkOffset;
    private GameObject[] chainSegmentArray;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject.GetComponent<PlayerController>();
        player.curChains++;

        startTime = Time.time;
        chainSegmentArray = new GameObject[linkMax];

        float chainOriginRotation = Mathf.Atan2(player.chainDir.y, player.chainDir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < linkMax; i++)
        {
            float curLinkOffset = (linkOffset * (i + 1)) + player.offset;
            Debug.Log(this.transform.eulerAngles);
            float posX = Mathf.Cos(chainOriginRotation) * curLinkOffset;
            float posY = Mathf.Sin(chainOriginRotation) * curLinkOffset;
            Debug.Log(posX + ", " + posY);
            Vector3 position = new Vector3(posX, posY, 0);
            
            chainSegmentArray[i] = Instantiate(link, new Vector3(player.chainDir.x * curLinkOffset, player.chainDir.y * curLinkOffset, 0), 
                this.transform.rotation, transform);
           
        }
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
        float chainOriginRotation = Mathf.Atan2(player.chainDir.y, player.chainDir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0,0,chainOriginRotation);
        this.transform.localPosition = new Vector3(player.chainDir.x * player.offset, player.chainDir.y * player.offset, 0);

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
