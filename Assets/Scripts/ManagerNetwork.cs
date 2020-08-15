using EZCameraShake;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNetwork : NetworkManager
{
    public GameObject PlayerPrefabB;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        // code 
        if (PlayerPrefabB)
        {
            ClientScene.RegisterPrefab(PlayerPrefabB);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("q"))
        {
            CameraShaker.Instance.ShakeOnce(2f, 2f, 0.5f,0.35f);
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position
        Transform start;
        GameObject player;

        if (numPlayers == 0)
        {
            start = GameObject.FindGameObjectWithTag("spawnA").transform;
            player = Instantiate(playerPrefab, start.position, start.rotation);
        }
        else
        {
            start = GameObject.FindGameObjectWithTag("spawnB").transform;
            player = Instantiate(PlayerPrefabB, start.position, start.rotation);

        }

        NetworkServer.AddPlayerForConnection(conn, player);

        // spawn ball if two players
        //if (numPlayers == 2)
        //{
        //    ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
        //    NetworkServer.Spawn(ball);
        //}
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }
}
