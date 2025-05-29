using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject player;
    private AudioSource bgm;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // ค้นหา GameObject ชื่อ "BGMusic"
        GameObject bgmObject = GameObject.Find("BGMusic");
        if (bgmObject != null)
        {
            bgm = bgmObject.GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boarder")
        {
            Destroy(this.gameObject);
        }
        else if (collision.tag == "Player")
        {
            if (bgm != null && bgm.isPlaying)
            {
                bgm.Stop(); // หยุด BGM ก่อนทำลาย Player
            }

            Destroy(player.gameObject);
        }
    }
}
