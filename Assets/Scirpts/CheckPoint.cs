using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    
    
    { 
        if(other.tag == "Player")
        {
            RespawnController.instance.SetSpawn(transform.position);

        }
    
    
    }





   /* // Start is called before the first frame update
    private Transform respawnPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            SetRespawnPoint(other.transform.position);
        }
    }

    

    void SetRespawnPoint(Vector3 position)
    {

        respawnPosition = new Vector3(position.x, position.y, position.z);

        if(playerAnimator != null)
        {
            playerAnimator.SetTrigger("isSittingDown");
        }
    }
    public Vector3 GetRespawnPosition()
    {
        return respawnPosition;
    }
    */
}
