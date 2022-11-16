using UnityEngine;
using System.IO.Ports;

public class LilacMov : MonoBehaviour
{
    [SerializeField] private GameObject VictoryPanel;

    SerialPort puertoDisplay;
    SerialPort puertoExtras;

    // prefabs de la nave azul 
    public Lilac[] prefabs;

    // dividimos en filas y columnnas
    //al ser un nivel menor quiero que sean menores a las del Galaga Original
    public int rows = 4;
    public int columns = 6;

    //velocidad y direccion movimiento
    public AnimationCurve speed;
    private Vector3 _direction = Vector2.right;

    // muertes y vivos
    public int amountKilled { get; private set; }
    public int totalLilac => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / (float)this.totalLilac;
    public float amountAlive => this.totalLilac - this.amountKilled;

    // prefab misil, es bullet porque comparten scripts
    public Missile missilePrefab;

    // que tan rapido son los ataques de misiles
    public float missileAttackRate = 1f;

    //public PlayerMov playerM;

    private void Awake()
    { 
        // for loop
        for (int row = 0; row < this.rows; row++)
        {
            float width = 1.5f * (this.columns - 1);
            float height = 1.5f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 1.5f), 0);

            for (int col = 0; col < this.columns; col++)
            {
                Lilac lilac = Instantiate(this.prefabs[row], this.transform);
                lilac.killed += LilacKilled;

                Vector3 position = rowPosition;
                position.x += col * 1.5f;
                lilac.transform.localPosition = position; // local position para que se base en la del padre
            }
        }
    }

    private void Start()
    {
        VictoryPanel.SetActive(false);
        //puertoDisplay = FindObjectOfType<PlayerMov>().puerto1;
        puertoExtras = FindObjectOfType<PlayerMov>().puerto2;

        //puertoDisplay.ReadTimeout = 40;
        //puerto1.Open();

        puertoExtras.ReadTimeout = 40;
        puertoExtras.Open();

        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);

        //playerM = GetComponent<PlayerMov>();

    }

    private void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform lilac in this.transform)
        {
            if (!lilac.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (_direction == Vector3.right && lilac.position.x >= (rightEdge.x - 2f))
            {
                AdvanceRow();
            }

            else if (_direction == Vector3.left && lilac.position.x <= (leftEdge.x + 2f))
            {
                AdvanceRow();
            }
        }

        if (amountKilled == totalLilac)
        {
            Time.timeScale = 0;
            //playerM.Arduino("victory");
            VictoryPanel.SetActive(true);
        }

    }

    private void MissileAttack()
    {
        foreach (Transform lilac in this.transform)
        {
            if (!lilac.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1.0f / (float)this.amountAlive))
            {
                Instantiate(this.missilePrefab, lilac.position, Quaternion.identity);
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

    private void LilacKilled()
    {
        this.amountKilled++;
    }

    void FixedUpdate()
    {
        Debug.Log("Lilac Spaceships Killed = " + amountKilled);
    }
}