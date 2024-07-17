using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterManager : MonoBehaviour
{
    public static GameMasterManager instance;
    public GameObject playerCharacter;
    Vector2 playerSpawnPoint = new Vector2(-10.25f, -1.38f);
    [SerializeField] GameObject doorObject;
    [SerializeField] int currentFloor = 0;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

       playerCharacter = GameObject.Find("PlayerCharacter");
    }

    private void Start()
    {
        ResetGameState();
    }

    public void PlayerDies()
    {
        Debug.Log("Player dies");
    }

    public void EnterDoor()
    {
        FadePanelTrigger.instance.StartFade();

    }


    //Triggered from fadepanel
    public void NewFloor()
    {
        ResetGameState();

        currentFloor++;
        UIManager.instance.RefreshFloorCounter(currentFloor);
        EnemySpawner.instance.SpawnFloorMonsters(currentFloor);
        ObstacleManager.instance.SpawnObstacles();
    }

    void ResetGameState()
    {
        ObstacleManager.instance.ClearObstacles();
        playerCharacter.transform.position = playerSpawnPoint;
        doorObject.SetActive(true);

    }

    public void AllEnemiesDead()
    {
        doorObject.SetActive(false);
    }
}
