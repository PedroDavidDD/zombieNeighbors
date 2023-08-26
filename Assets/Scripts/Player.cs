using System.Collections;
using UnityEngine;
using Input = UnityEngine.Input;

public class Player : MonoBehaviour
{
    [SerializeField, Header("Velocidad por poción")] float speed = 2f;
    [SerializeField] float initialSpeed = 2f;
    [SerializeField] float cooldownSpeed = 2f;
    [SerializeField, Header("Balas")] GameObject pistolBulletPrefab; // Prefab de la bala de pistola
    [SerializeField, Header("Cooldown Habilidad Balas")] private float nextBulletTimeCooldown = .5f;
    [SerializeField] private float nextBulletTime;
    private float initialRotationZ;
    private Rigidbody2D rb2D;
    private Vector2 direction;
    private Vector2 input;

    [SerializeField, Header("Frisbee")] private GameObject frisbeePrefab;
    [SerializeField, Header("Punto de disparo")] private GameObject triggerPoint;
    [SerializeField] private float cantidadFrisbee = 0.4f;
    [SerializeField, Header("Cooldown Habilidad Frisbee")] private float nextFrisbeeTimeCooldown = 3f;
    [SerializeField] private float nextFrisbeeTime;

    [SerializeField, Header("Vivo")] public bool isLive = true;


    void Start()
    {
        // Almacenar la rotación inicial en el eje Z del personaje
        initialRotationZ = transform.eulerAngles.z;
        rb2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isLive)
        {

            LookAtMouse();

            Move();
            if (nextBulletTime > 0)
            {
                // Reducir el tiempo de cooldown
                nextBulletTime -= Time.deltaTime;
            }
            if (nextBulletTime <= 0)
            {
                if (Input.GetMouseButtonDown(0)) // El 0 representa el botón izquierdo del mouse
                {
                    // Instanciar la bala de pistola en la posición del jugador
                    GameObject newPistolBullet = Instantiate(pistolBulletPrefab,
                                                             triggerPoint.transform.position,
                                                             Quaternion.Euler(0f, 0f, transform.eulerAngles.z));
                    // Configurar el tiempo de cooldown
                    nextBulletTime = nextBulletTimeCooldown;
                }
            }

            if (nextFrisbeeTime <= 0)
            {
                if (Input.GetMouseButtonDown(1)) // El 1 representa el botón derecho del mouse
                {
                    // Crear el Frisbee
                    for (float i = 0.1f; i < cantidadFrisbee; i += 0.1f)
                    {
                        Invoke(nameof(SpawnFrisbee), i);
                    }
                    nextFrisbeeTime = nextFrisbeeTimeCooldown;
                }
            }
            else
            {
                nextFrisbeeTime -= Time.deltaTime;
            }

        }
    }
    private void Move()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        direction = input.normalized; 
    }
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + direction * speed * Time.fixedDeltaTime);
    }
    private void LookAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = mousePosition - transform.position;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Aplicar la rotación solo en el eje Z
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void IncreaseSpeed(float increaseSpeed)
    {
        speed += increaseSpeed;

        StartCoroutine(ResetSpeedWithCooldown());
    }

    private IEnumerator ResetSpeedWithCooldown()
    {
        yield return new WaitForSeconds(cooldownSpeed);

        speed = initialSpeed;
    }
    private void SpawnFrisbee()
    {
        GameObject newFrisbee = Instantiate(frisbeePrefab,
                                               triggerPoint.transform.position,
                                               Quaternion.Euler(0f, 0f, transform.eulerAngles.z));
    }

}
