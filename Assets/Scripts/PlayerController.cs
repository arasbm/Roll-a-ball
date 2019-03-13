/**
 * when count is bigger than minimumPickupRequired and player reaches the target (marked by "Target" tag) then player wins the game
 * if player reaches the targer but does not have enough pickup count then they lose
 * Change log:
 * Jan 24: added jump with space key, mass and mass increase properties
 * Jan 30: fixed score display text
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce = 2.0f;
    public float xGrowRate = 0.1F;
    public float yGrowRate = 0.1F;
    public float zGrowRate = 0.1F;
    public float playerMass = 1.0f;
    public float massGrowRate = 0.1f;
    public int minimumPickupRequired = 12;
    public Text countText;
    public Text winText;
    public Text loseText;

    private Rigidbody rb;
    private int count;
    private bool gameEnded = false;
    private Vector3 jump;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        rb.mass = playerMass;
    }

    void FixedUpdate ()
    {
        if (gameEnded) return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown("space")) {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) {
        if (gameEnded) return;

        if (other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive(false);
            IncreaseCount();
        } else if (other.gameObject.CompareTag("Target")) {
            if (count > minimumPickupRequired) {
                win();
            } else {
                lose();
            }
        }
    }

    void IncreaseCount() {
        count = count + 1;
        transform.localScale += new Vector3(xGrowRate, yGrowRate, zGrowRate);
        countText.text = "Score: " + count;
        rb.mass += massGrowRate;
    }

    void win() {
        winText.gameObject.SetActive(true);
        gameEnded = true;
    }

    void lose() {
        loseText.gameObject.SetActive(true);
        gameEnded = true;
    }
}