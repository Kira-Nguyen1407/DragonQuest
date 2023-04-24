using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPositions;
    // Start is called before the first frame update
    void Start()
    {
        // Save the initial positions of all enemies
        initialPositions = new Vector3[enemies.Length];

        for(int i = 0; i < enemies.Length; i++){
            if(enemies[i] != null){
                initialPositions[i] = enemies[i].transform.position;
            }
        }
    }

    public void ActivateRoom(bool _status){
        for(int i = 0; i < enemies.Length; i++){
            if(enemies[i] != null){
                // Debug.Log("Deactivating Spike Head trap in the room");
                // Debug.Log($"_status: {_status}");
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPositions[i];
            }
        }
        // if(enemies.Length > 0){
        //     Debug.Log("Spike Head trap exits in the room");
        // }
        // else{
        //     Debug.Log("There are no traps in the room");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
