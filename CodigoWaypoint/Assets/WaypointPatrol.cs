using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    //Local onde sera setado os GameObject que serão os Waypoints
    public GameObject[] waypoints;

    //Ponto atual no qual o jogador está se dirigindo se inicia em 0 pois Arrays tem inicio em 0
    int currentWP = 0;

    //Velocidade do player
    float speed = 1.0f;

    //Se o player ficar a uma certa distancia um novo waypoint será setado e o player ira para ele
    float accuracy = 1.0f;

    //Velocidade de rotação do player
    float rotSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {

        //Encontra todos os GameObjects com a Tag "Waypoint" e joga na Array
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Caso o Array for 0 então não faça nada, pois sempre haverá apenas 1 waypoint
        if (waypoints.Length == 0) return;

        //Vetor com a função de guardar a posição do waypoint atual nas coordenadas x e z, não conta o y
        //Usado para fazer o player olhar para esse waypoint
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, this.transform.position.y, waypoints[currentWP].transform.position.z);

        //Vetor direção, fazendo o waypoint menos a posição atual do jogador e ajusta a rotação do player usando Quaternion.Slerp
        Vector3 direction = lookAtGoal - this.transform.position; this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);

        //Se a magnitude da direção for menor que o accuracy será calculado um novo waypoint
        if (direction.magnitude < accuracy)
        {

            //Próximo waypoint do Array
            currentWP++;

            //Se o próximo waypoint não existir, volta para a posição 0
            if (currentWP >= waypoints.Length)
            {
                //Seta o valor para 0
                currentWP = 0;
            }
        }

        //Move o player para frente constantemente baseado na speed, ira para o weypoint alvo
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
