using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    PlayerController player;
    GameController gm;

    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private Color[] cores = { new Color32(207,16,16,255), new Color32(33,185,182,255) };
    [SerializeField]
    private Color _actualColor;
    [SerializeField]
    bool _isInTheSelectZone = false;
    [SerializeField]
    bool _leftPressed = false;
    [SerializeField]
    bool _rigthPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        GenerateColor();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isInTheSelectZone == false)
        {
            MoveFoward();
        }
        if (_leftPressed == true)
        {
            MoveLeft();
        }
        if (_rigthPressed == true)
        {
            MoveRight();
        }
    }

    void MoveFoward()
    {
        transform.position -= new Vector3(0,0,_speed * Time.deltaTime);
    }

    void MoveLeft()
    {
        transform.position -= new Vector3(_speed * Time.deltaTime, 0, 0);
    }

    void MoveRight()
    {
        transform.position += new Vector3(_speed * Time.deltaTime,0,0);
    }

    void GenerateColor()
    {
        var pos = Random.Range(0, cores.Length);
        var obj = gameObject.GetComponent<Renderer>();
        obj.material.SetColor("_Color",cores[pos]);
        _actualColor = cores[pos];
    }

    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Entrou stay");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "keyPressZone")
        {
            if(_isInTheSelectZone == false)
            {
                var i = player.GetKeyDirection();
                switch (i)
                {
                    case 0:
                        Debug.Log("Esquerda");
                        if(_actualColor == cores[0])
                        {
                            _leftPressed = true;
                            _isInTheSelectZone = true;
                            gm.AddScore();
                        }
                        else
                        {
                            gm.LoseLife();
                            DestroyEnemy();
                        }
                        break;
                    case 1:
                        Debug.Log("Direita");
                        if (_actualColor == cores[1])
                        {
                            _rigthPressed = true;
                            _isInTheSelectZone = true;
                            gm.AddScore();
                        }
                        else
                        {
                            gm.LoseLife();
                            DestroyEnemy();
                        }
                        break;
                    default:
                        Debug.Log("Apertou nada");
                        break;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrou enter");
        Debug.Log(other.gameObject.name);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colidiu com " + collision.gameObject.name);
        if(collision.gameObject.tag == "Player")
        {
            gm.LoseLife();
            DestroyEnemy();
        }
        if(collision.gameObject.tag == "Killer")
        {
            DestroyEnemy();
        }
    }

    private void OnBecameInvisible()
    {
            DestroyEnemy();
    }

}
