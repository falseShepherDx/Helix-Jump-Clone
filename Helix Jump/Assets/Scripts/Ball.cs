using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject splashPrefab;
    [SerializeField] private float fallAccelerator;
    public int passedRingCount = 0;
    public static Ball instance;
    [SerializeField] private ParticleSystem bloodSplashParticle;
    [Header("Sound")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip jumpAudio;
    [SerializeField] private AudioClip trapAudio;
    [SerializeField] private AudioClip winAudio;
    [SerializeField] private AudioClip breakPlatformAudio;
    
    [Header("Menus")]
    public GameObject RestartMenu;
    public GameObject FinishMenu;

    private void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        bloodSplashParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        //increase fall speed overtime by increasing gravity
        Physics.gravity *= Time.deltaTime * fallAccelerator;
        
        
    }
    
    private void OnCollisionEnter(Collision other)
    {   
        if (passedRingCount > 2)
        {
            if (!other.gameObject.CompareTag("LastRing"))
            {
                GameObject ringParent = other.gameObject.transform.parent.gameObject;
                Destroy(ringParent);
                passedRingCount = 0;
                _audioSource.PlayOneShot(breakPlatformAudio);
                Destructible.Instance.ExplodePlatform(ringParent);
                if (other.gameObject.CompareTag("Trap")) return;//for not to die hocam
            }
        }
        //reset gravity to default when ball collides with something
        Physics.gravity = new Vector3(0f, -9.81f, 0f);
        if (other.gameObject.CompareTag("Safe"))
        {
            bloodSplashParticle.Play();
            rb.velocity = Vector3.up * jumpForce;
            _audioSource.PlayOneShot(jumpAudio);
            GameObject splash = Instantiate(splashPrefab, transform.position +new Vector3(0,-0.22f,0), transform.rotation);
            splash.transform.SetParent(other.gameObject.transform);
        }
        
        else if (other.gameObject.CompareTag("Trap"))
        {
            rb.isKinematic = true;
            _audioSource.PlayOneShot(trapAudio);
            Cursor.lockState = CursorLockMode.None;
            RestartMenu.SetActive(true);
           

        }
        else if (other.gameObject.CompareTag("LastRing"))
        {
            Debug.Log("Next Level");
            // why code doesnt enter here?s
            rb.isKinematic = true;
            _audioSource.PlayOneShot(winAudio);
            Cursor.lockState = CursorLockMode.None;
            FinishMenu.SetActive(true);
            
        }
        
    }
}
