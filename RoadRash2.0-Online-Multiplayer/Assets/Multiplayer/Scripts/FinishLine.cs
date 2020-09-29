using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FinishLine : NetworkBehaviour
{
    public int hitCount = 0;



    private void OnCollisionEnter(Collision collision)
    {
        if (!isServer)
            return;

        if (collision.collider.tag == "Player")
        {
            GameOver gameOver = collision.gameObject.GetComponent<GameOver>();
            gameOver.EndMatch();
        }

        NetworkAnimator.Destroy(gameObject);
    }

    
}
