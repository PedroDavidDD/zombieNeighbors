using System.Collections;
using UnityEngine;
using Input = UnityEngine.Input;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float initialSpeed = 2f;
    [SerializeField] GameObject pistolBulletPrefab; // Prefab de la bala de pistola
    private float initialRotationZ;
    private Rigidbody2D rb2D;
    private Vector2 direction;
    private Vector2 input;

    [SerializeField] float cooldownSpeed = 3f;

    [SerializeField, Header("Frisbee")] private GameObject frisbeePrefab;
    [SerializeField, Header("Punto de disparo")] private GameObject triggerPoint;
    [SerializeField, Header("Cooldown Habilidades")] private float nextSkillTimeCooldown = 3f;
    [SerializeField] private float nextSkillTime;

    [SerializeField] public bool isLive = true;
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

            if (Input.GetMouseButtonDown(0)) // El 0 representa el botón izquierdo del mouse
            {
                // Instanciar la bala de pistola en la posición del jugador
                GameObject newPistolBullet = Instantiate(pistolBulletPrefab,
                                                         triggerPoint.transform.position,
                                                         Quaternion.Euler(0f, 0f, transform.eulerAngles.z));
            }

            if (Input.GetMouseButtonDown(1)) // El 1 representa el botón derecho del mouse
            {
                // creo el frisbee
                GameObject newFrisbee = Instantiate(frisbeePrefab,
                                                    triggerPoint.transform.position,
                                                    Quaternion.Euler(0f, 0f, transform.eulerAngles.z));
            }

            if (nextSkillTime <= 0)
            {
                nextSkillTime = nextSkillTimeCooldown;
            }
            else
            {
                nextSkillTime -= Time.deltaTime;
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

}
