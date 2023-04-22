using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    [SerializeField] public float direction;
    [SerializeField] public float lifeTime;
    public float waitToDeactivate;

    private BoxCollider2D boxCollider2D;
    private Animator animator;
    [SerializeField] private AudioClip fireballExplode;

    // Start is called before the first frame update
    void Start()
    {
        // projCollider = GetComponent<BoxCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        hit = false;
        speed = 7.0f;
        lifeTime = 5.0f;
        waitToDeactivate = 0;
        // direction = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(hit) return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        waitToDeactivate = waitToDeactivate + Time.deltaTime;
        if(waitToDeactivate >= lifeTime){
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        hit = true;
        // boxCollider2D = GetComponent<BoxCollider2D>();
        // if(boxCollider2D != null) {
        //     boxCollider2D.enabled = false;
        // }
        // else{
        //     Debug.Log("boxCollider2D is null");
        // }
        animator.SetTrigger("explode");
        SoundManager.instance.PlaySound(fireballExplode);

        if(other.tag == "Enemy"){
            if(other.GetComponent<Health>() != null){
                if(!other.GetComponent<Health>().isHurting){
                    other.GetComponent<Health>().TakeDamage(1);
                }
            }
            
        }
    }

    public void SetDirection(float _direction) {
        direction = _direction;
        hit = false;
        waitToDeactivate = 0;


        float localScaleX = transform.localScale.x;

        if(Mathf.Sign(localScaleX) != Mathf.Sign(this.direction)){
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        // boxCollider2D = GetComponent<BoxCollider2D>();
        // if(boxCollider2D != null){
        //     boxCollider2D.enabled = true;
        // }
        // else{
        //     Debug.Log("boxCollider2D is null");
        // }
        gameObject.SetActive(true);
    }

    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
