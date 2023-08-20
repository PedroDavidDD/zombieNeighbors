using System;
using UnityEngine;

public class MoveZombie : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f; // Velocidad del Zombie (puedes ajustarla en el Inspector)

    private Transform player; // Referencia al transform del Player
    private Rigidbody2D rb2D;
    [SerializeField] private Transform objRadius; // Referencia al transform del Player

    [SerializeField] private float currentRadius = 1.5f;

    [SerializeField] private bool hasPlayerInsideRadius = false;

    [SerializeField] private float cooldownSinceLastCheck = 2f;
    [SerializeField] private float timeSinceLastCheck = 0f;
    private bool radiusIncreased = false;
    [SerializeField] private float originalRadius = 1.5f;
    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private bool alwaysFollowPlayer = false;

    [SerializeField] private float cooldownFollow = 20f;
    [SerializeField] private float timeFollow;

    private enum TypeZombie { runner, idle, line }
    [SerializeField] private TypeZombie typeZombie = TypeZombie.line;

    [SerializeField, Header("Patrullar")] private Transform[] puntosDeMovimiento;
    [SerializeField] private bool CanWalkForRoom = true;
    private int siguientePunto;
    [SerializeField] private float minDistancia = .5f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Buscamos el Player por su tag
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        // Movemos el Zombie hacia el Player
        if (player != null)
        {
            // Detectar si el Player está dentro del radio del objRadius
            Collider2D[] playerCollider = Physics2D.OverlapCircleAll(objRadius.position, currentRadius);

            if (playerCollider != null)
            {
                if (typeZombie == TypeZombie.line)
                {
                    // Seguir al Player desde un inicio
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                    DirectionToPlayer();
                }
                else
                {
                    foreach (Collider2D collider in playerCollider)
                    {
                        // Comprueba si el collider es del tag "Player"
                        if (collider.CompareTag("Player"))
                        {
                            if (typeZombie == TypeZombie.runner)
                            {
                                // Seguir al Player cuando se acerca al jugador [ Por un tiempo determinado ]
                                alwaysFollowPlayer = true;
                            }
                            else
                            {
                                alwaysFollowPlayer = false;
                                // Seguir al Player cuando se acerca al jugador [ Sin tiempo determinado ]
                                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                                DirectionToPlayer();
                            }

                            hasPlayerInsideRadius = true;
                            CanWalkForRoom = false;
                            //currentRadius = originalRadius; // Reinicia el radio a su valor original si el Player está dentro
                            timeSinceLastCheck = 0f; // Reinicia el contador de tiempo                       
                        }
                        else
                        {
                            hasPlayerInsideRadius = false;
                            //CanWalkForRoom = true;
                        }
                    }


                    // Walk for the room
                    WalkForRoom();
                    // Ingrementar el radio usual por el doble cada cierto tiempo
                    PlayerInsideRadius();
                    // Seguir al Player y despues de 3s, parar.
                    AlwaysFollowPlayer();
                }
            }

        }

    }

    private void DirectionToPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.z = 0; // Mantener el mismo valor en Z para que el Zombie no rote en ese eje
        directionToPlayer.Normalize(); // Normalizar la dirección para obtener un vector unitario
                                       // Rotar el Zombie para que mire hacia el jugador
        transform.right = directionToPlayer;
    }

    private void PlayerInsideRadius()
    {
        if (!hasPlayerInsideRadius)
        {
            timeSinceLastCheck += Time.deltaTime;

            // Si han pasado 2 segundos y el radio no ha sido aumentado
            if (timeSinceLastCheck >= cooldownSinceLastCheck)
            {
                // Aumenta el radio por 2 si no es el radio inicial ya aumentado
                if (currentRadius == originalRadius && radiusIncreased == false)
                {
                    currentRadius *= 2f;
                }
                // Si ya es el radio inicial aumentado, reinicia al valor original
                else if (currentRadius != originalRadius && radiusIncreased == true)
                {
                    currentRadius = originalRadius;
                }
                timeSinceLastCheck = 0f; // Reinicia el contador de tiempo
                radiusIncreased = !radiusIncreased; // Marca que el radio fue aumentado para evitar que se aumente múltiples veces
                CanWalkForRoom = true; // Puede volver a recorrer la sala despues del tiempo
            }
        }
    }
    private void AlwaysFollowPlayer()
    {
        if (alwaysFollowPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            DirectionToPlayer();

            if (timeFollow >= cooldownFollow)
            {
                alwaysFollowPlayer = false;
                timeFollow = 0f;
            }
            else
            {
                timeFollow += Time.deltaTime;
            }
        }
    }
    
    private void WalkForRoom()
    {
        if (puntosDeMovimiento.Length > 0)
        {
            if (CanWalkForRoom)
            {
                // Para que el objeto se mueva a la nueva posición
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    puntosDeMovimiento[siguientePunto].position,
                    speed * Time.deltaTime);
                /* 
                 * Para ir al siguiente punto: distancia entre dos objetos
                 * Si el Mounstro llega al primer punto, siga el siguiente
                */
                if (Vector2.Distance(transform.position, puntosDeMovimiento[siguientePunto].position) < minDistancia)
                {

                    siguientePunto += 1;

                    if (siguientePunto >= puntosDeMovimiento.Length)
                    {
                        siguientePunto = 0;
                        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    }
                }
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(objRadius.position, currentRadius);
    }
}
