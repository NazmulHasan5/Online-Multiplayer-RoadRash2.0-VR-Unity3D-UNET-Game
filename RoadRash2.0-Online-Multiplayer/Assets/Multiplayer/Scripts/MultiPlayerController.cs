using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class MultiPlayerController : NetworkBehaviour
{
    public Transform cameraTransform;
    [SyncVar]
    public float speed = 0;
    public GameObject playerCamera;
    public float sideMovePerFrame = .05f;
    public GameObject gvrEmulator;
    public TMP_Text gameOverText;
    public float speedPerFrame = .005f;

    public bool gameOver = false;

    [SyncVar] 
    public bool win = true;
    [SyncVar]
    public int position = 1;

    void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(playerCamera);
            Destroy(gvrEmulator);
     
            Destroy(this);
        }

        gvrEmulator.GetComponent<GvrEditorEmulator>().enabled = true;
        //playerCamera = Camera.main;
        //playerCamera.transform.SetParent(gameObject.transform);
        //cameraTransform = playerCamera.transform;
    }

   
    void FixedUpdate()
    {
        if (!gameOver)
        {
            speed += Time.deltaTime * speedPerFrame;
            if (cameraTransform.rotation.y > .1f)
            {
                if (transform.position.x < -2.5f)
                {
                    transform.position = new Vector3(transform.position.x + sideMovePerFrame, transform.position.y, transform.position.z + speed);

                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);

                }

            }
            else if (cameraTransform.rotation.y < -0.1f)
            {
                //frontRotate = Quaternion. (handle.rotation.x, 25f, handle.rotation.z);
                if (transform.position.x > -11.5f)
                {
                    transform.position = new Vector3(transform.position.x - sideMovePerFrame, transform.position.y, transform.position.z + speed);

                    //handle.rotation = Quaternion.Lerp(handle.rotation, (handle.rotation.x f, 25f, handle.rotation.z f), Time.deltaTime * 2);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);

                }

            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
            }
        }
       
        
    }

    void updateState(int newPosition)
    {
        if (!isLocalPlayer)
        {
            win = false;
            position = newPosition;

        }
    }

   

    [Command]
    void CmdInfoUpdate(bool state, int newPosition)
    {
        win = state;
        position = newPosition;
    }

    [ClientCallback]
    void TransmitInfo()
    {
        if (isLocalPlayer)
        {
            CmdInfoUpdate(win, position);
            updateState(position);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (isLocalPlayer )
        {

            if (collision.collider.tag == "Obstracal")
            {

                speed -= .009f;
                SpeedUpdate(speed);
            }
               

        }
       
    }

    [Command]
    private void SpeedUpdate(float newSpeed)
    {
        speed = newSpeed;
    }

    /*
    [ClientRpc]
    void RpcWinCall()
    {
        gameOver = true;
        if (isLocalPlayer)
        {
            gameOverText.text = "You Win";
            gameOverText.color = Color.green;
        }
        else
        {
            gameOverText.text = "Game Over" ;
            gameOverText.color = Color.red;
        }
    }

    void Lobby()
    {
        FindObjectOfType<NetworkLobbyManager>().ServerReturnToLobby();
    }
    */
}
