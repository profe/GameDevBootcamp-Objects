using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float gunPowerupRate;

    private Player player;
    private ScoreManager scoreManager;

    private float horizontal, vertical;
    private Vector2 lookTarget;

    private float gunPowerupTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        scoreManager = GameManager.GetInstance().scoreManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GetInstance().IsPlaying())
        {
            return;
        }

        //handle mouse movement moving player direction
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        lookTarget = Input.mousePosition;

        //handle left button click with and without powerup
        if (player.HasGunPowerup() && Input.GetMouseButton(0) && gunPowerupTimer > (1.0f / gunPowerupRate))
        {
            gunPowerupTimer = 0;
            player.Shoot();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            player.Shoot();
        }
        gunPowerupTimer += Time.deltaTime;


        //handle right button click for nuke powerup
        if (scoreManager.NumNukes > 0 && Input.GetMouseButtonDown(1))
        {
            scoreManager.DecrementNukes(); //updates UI
            //play sound
            GameManager.GetInstance().PlaySound(Sound.NukePickup);
            //destory all enemies and pickups
            GameManager.GetInstance().AttackEnemies(3);
            //GameManager.GetInstance().DestroyAllPickups(); //can comment this out if want to leave pickups
        }
    }

    private void FixedUpdate()
    {
        player.Move(new Vector2(horizontal, vertical), lookTarget);
    }
}
