using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private Rigidbody myRigidbody;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;

    public GunController theGun;

    public Text ammoText;

    public int enemiesKilledNumber = 0;
    public static PlayerController instance;
    public Text enemiesKilledText;

    [SerializeField]
    GameObject WallsLevel1;

    [SerializeField]
    GameObject WallsLevel2;

    [SerializeField]
    GameObject Exit;

    public GameObject enemy;

    public Text finalText;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        for ( int i = 0; i < 3; i++)
        {
            SpawnNewEnemy(5, 9);
        }

        instance = this;

        finalText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if (Input.GetMouseButtonDown(0))
        {
            theGun.isFiring = true;    
        }

        if (Input.GetMouseButtonUp(0))
        {
            theGun.isFiring = false;
        }
        setAmmoText();
        setEnemiesKilledText();
    }

    void FixedUpdate()
    {
        myRigidbody.velocity = moveVelocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);

            theGun.ammo += 20;
        }

        if (other.gameObject.CompareTag("FinalDoor"))
        {
            Destroy(other.gameObject);
            finalText.text = "YOU WON";
            Invoke("LoadMenu", 2);

        }
    }

    void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    void setAmmoText()
    {
        ammoText.text = "Ammo: " + theGun.ammo.ToString();
    }

    void setEnemiesKilledText()
    {
        enemiesKilledText.text = "Enemies killed: " + enemiesKilledNumber.ToString();
    }

    public void EnemyKilled()
    {
        if(enemiesKilledNumber == 3)
        {
            Destroy(WallsLevel1);
            for ( int i = 0; i < 7; i++)
            {
                SpawnNewEnemy(20, 30);
            }
        }
        if (enemiesKilledNumber == 10)
        {
            Destroy(WallsLevel2);
            for (int i = 0; i < 10; i++)
            {
                SpawnNewEnemy(20, 30);
            }
        }
        if (enemiesKilledNumber == 20)
        {
            Debug.Log("finished");
            Destroy(Exit);
        }
    }


    void SpawnNewEnemy(int min, int max)
    {
        Vector3 vector = new Vector3(Random.Range(-9.0f, 9.0f), 0.35f, Random.Range(-9.0f, 9.0f)).normalized * Random.Range(min, max);
        Instantiate(enemy, vector, Quaternion.identity);
    }
}
