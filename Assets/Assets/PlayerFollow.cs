using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerFollow : MonoBehaviour

{
    public GameObject cam;
    public float speed = 2f;
    public float jumpspeed = 5f;
    public Transform circle;
    public float radius;
    public LayerMask layerMask;
    public int bound;
    private Vector3 campos;
    private bool isOnGround;
    private GameObject player;
    private Rigidbody2D rb;
    private bool camActivated = false;
    private GameObject camera;
    private bool restrictMovement = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        isOnGround = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(circle.position, radius, layerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isOnGround = true;
            }
        }
        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 5 && isOnGround == true)
        {
            createInstance();
            chase(player);
            if ((player.transform.position.y - transform.position.y) >= 2 && isOnGround == true)
            {
                jumptowards(player);
            }
        }
        if(restrictMovement == true)
        {
            player.transform.position = new Vector2(Mathf.Clamp(player.transform.position.x, camera.transform.position.x - bound, camera.transform.position.x + bound), player.transform.position.y);
        }
    }

    private void createInstance()
    {
        if (camActivated == false)
        {
            campos = new Vector3((transform.position.x + player.transform.position.x) / 2, (transform.position.y + player.transform.position.y) / 2, -10);
            camera = Instantiate(cam, campos, Quaternion.identity);
            camera.GetComponent<CinemachineVirtualCamera>().Priority = 20;
            camActivated = true;
            restrictMovement = true;
        }
    }
    public IEnumerator destroyInstance()
    {
        if(camera.gameObject != null)
        {
            camera.SetActive(false);
            yield return new WaitForSeconds(2f);
            Destroy(camera);
        }
    }
    private void chase(GameObject obj)
    {

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(obj.transform.position.x, transform.position.y), speed * Time.deltaTime);
    }

    private void jumptowards(GameObject obj)
    {
        rb.velocity = new Vector2(obj.transform.position.x - transform.position.x, 4);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(circle.position.x, circle.position.y, 0), radius);
    }
}
