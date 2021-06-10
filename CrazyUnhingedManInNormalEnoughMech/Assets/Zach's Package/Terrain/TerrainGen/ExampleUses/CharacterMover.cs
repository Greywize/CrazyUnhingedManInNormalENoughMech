using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zacks.Terrain
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("How fast the player moves")]
        float speed = 10;

        [SerializeField]
        float gravity = 10;

        CharacterController cc;

        Vector3 velocity;

        public CubeGenerator terrain;

        // Start is called before the first frame update
        void Start()
        {
            cc = GetComponent<CharacterController>();
            //terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<CubeGenerator>();
            Vector3 spawnPos = terrain.GridToWorld(new Vector2Int(terrain.width / 2, terrain.length / 2));
            spawnPos.y += 2;
            transform.position = spawnPos;
            //transform.position = terrain.GridToWorld(terrain.Cubes[0, 0]);
        }

        // Update is called once per frame
        void Update()
        {
            velocity = new Vector3(Input.GetAxis("Horizontal") * speed, cc.isGrounded ? 0 : -gravity, Input.GetAxis("Vertical") * speed);

            cc.Move(velocity * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
                Debug.Log($"The players position in grid coords is {terrain.WorldToGrid(transform.position)}");
        }
    }
}
