using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class GameOver : NetworkBehaviour
{
    // Start is called before the first frame update
    public PlayerController playerController;
    public TMP_Text gameOverText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndMatch()
    {
        if (!isServer)
            return;

        RpcEndGame();

        Invoke("Lobby", 5);
    }

    [ClientRpc]
    void RpcEndGame()
    {
        gameOverText = GameObject.Find("EndText").GetComponent<TMP_Text>();
        if (isLocalPlayer)
        {
           
            gameOverText.text = "You win";
            gameOverText.color = Color.green;
        }
        else
        {
            
            gameOverText.text = "Game Over";
            gameOverText.color = Color.red;
        }
        gameObject.GetComponent<MultiPlayerController>().enabled = false;
    }


    void Lobby()
    {
        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }
}
