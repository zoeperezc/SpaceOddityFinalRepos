using UnityEngine;
using System.IO.Ports;

public class BlueMov : MonoBehaviour
{
    [SerializeField] private GameObject VictoryPanel;
    //SerialPort puerto1 = new SerialPort("COM5", 9600); //display
    // SerialPort puerto2 = new SerialPort("COM6", 9600); //demás

    //SerialPort puertoDisplay;

    //public PlayerMov playerM;

    // prefabs de la nave azul 
    public Blue[] prefabs;

    // dividimos en filas y columnnas
    //al ser un nivel menor quiero que sean menores a las del Galaga Original
    public int rows = 4;
    public int columns = 6;

    //velocidad y direccion movimiento
    public float speed = 4f;
    private Vector3 _direction = Vector2.right;

    // muertes y vivos
    public int amountKilled { get; private set; }
    public int totalBlue => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / (float)this.totalBlue;
    public float amountAlive => this.totalBlue - this.amountKilled;

    // prefab misil
    public Missile missilePrefab;

    // que tan rapido son los ataques de misiles
    public float missileAttackRate = 1f;

    private void Awake()
    {
        // for loop
        for (int row = 0; row < this.rows; row++)
        {
            float width = 1.0f * (this.columns - 1);
            float height = 1.5f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 1.5f), 0);

            for (int col = 0; col < this.columns; col++)
            {
                Blue blue = Instantiate(this.prefabs[row], this.transform);
                blue.killed += BlueKilled;

                Vector3 position = rowPosition;
                position.x += col * 1.0f;
                blue.transform.localPosition = position; // local position para que se base en la del padre
            }
        }
    }

    private void Start() 
    {
        VictoryPanel.SetActive(false);
        //puertoDisplay = FindObjectOfType<PlayerMov>().puerto1;

        //puertoDisplay.ReadTimeout = 40;
        //puertoDisplay.Open();

        //puerto2.ReadTimeout = 40;
        //puerto2.Open();

        //playerM = GetComponent<PlayerMov>();

        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }

    private void Update()
    {
        this.transform.position += _direction * this.speed * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform blue in this.transform)
        {
            if (!blue.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (_direction == Vector3.right && blue.position.x >= (rightEdge.x - 2f))
            {
                AdvanceRow();
            }

            else if (_direction == Vector3.left && blue.position.x <= (leftEdge.x + 2f))
            {
                AdvanceRow();
            }
        }

        if (amountKilled == totalBlue)
        {
            Time.timeScale = 0;
            //playerM.Arduino("victory");
            VictoryPanel.SetActive(true);
        }
    }

    private void MissileAttack()
    {
        foreach (Transform blue in this.transform)
        {
            if (!blue.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1f / (float)this.amountAlive))
            {
                Instantiate(this.missilePrefab, blue.position, Quaternion.identity);
                break;
            }
        }

    }

    private void AdvanceRow()
    {
        _direction *= -1f;
        Vector3 position = this.transform.position;
        position.y -= 1f;

        this.transform.position = position;
    }

    private void BlueKilled()
    {
        this.amountKilled++;
    }

    void FixedUpdate()
    {
        Debug.Log("Blue Spaceships Killed = " + amountKilled);
    }
}