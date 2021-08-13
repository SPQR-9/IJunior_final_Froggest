using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class VictoryChest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.enabled = false;
            Player player = playerMover.gameObject.GetComponent<Player>();
            player.Win();
            gameObject.SetActive(false);
        }
    }
}
