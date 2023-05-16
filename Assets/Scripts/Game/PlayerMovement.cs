using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    private Rigidbody2D myRB;
    [SerializeField] private Vector2 vectorDeMovimientoMouse;
    [SerializeField] private Vector2 vectorDeMovimientoMouseCambio;
    [SerializeField]
    private float speed;
    private float limitSuperior;
    private float limitInferior;
    private Transform playerTransform;
    [SerializeField] public int player_lives = 4;
    public int player_points = 0;
    public PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        if (up == KeyCode.None) up = KeyCode.UpArrow;
        if (down == KeyCode.None) down = KeyCode.DownArrow;
        myRB = GetComponent<Rigidbody2D>();
        SetMinMax();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(up) && transform.position.y < limitSuperior)
        {
            myRB.velocity = new Vector2(0f, speed);
        }
        else if (Input.GetKey(down) && transform.position.y > limitInferior)
        {
            myRB.velocity = new Vector2(0f, -speed);
        }
        else
        {
            myRB.velocity = Vector2.zero;
        }
        */
    }

    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limitInferior = -bounds.y;
        limitSuperior = bounds.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Candy")
        {
            CandyGenerator.instance.ManageCandy(other.gameObject.GetComponent<CandyController>(), this);
        }
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        myRB.velocity = inputMovement * speed;
    }

    public void OnMovementMouse(InputAction.CallbackContext value)
    {
        Vector3 mouseInput = Camera.main.ScreenToWorldPoint(value.ReadValue<Vector2>());
        if (mouseInput.y < playerTransform.position.y)
        {
            vectorDeMovimientoMouseCambio.y= -vectorDeMovimientoMouse.y* speed;
            myRB.velocity = vectorDeMovimientoMouseCambio;
        }
        else if (mouseInput.y > playerTransform.position.y)
        {
            vectorDeMovimientoMouseCambio.y = vectorDeMovimientoMouse.y * speed;
            myRB.velocity = vectorDeMovimientoMouseCambio;
        }
    }
}
