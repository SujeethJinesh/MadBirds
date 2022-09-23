using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _birdWasLaunched;
    private float _timeSittingAround;
    private Vector3 _boundingBox;

    [SerializeField] private float _launchPower = 300;
    [SerializeField] private float timeout = 2;
    [SerializeField] private float boundingBoxWidth = 10;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, _initialPosition);

        if (_birdWasLaunched && rigidbody2D.velocity.magnitude <= 0.2)
        {
            _timeSittingAround += Time.deltaTime;
        }
        if (_timeSittingAround > timeout)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        spriteRenderer.color = Color.red;
        lineRenderer.enabled = true;
        _initialPosition = transform.position;
    }

    private void OnMouseUp()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody2D rigidbody =  GetComponent<Rigidbody2D>();
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer.color = Color.white;

        Vector2 directionToInitialPosition = _initialPosition - transform.position;

        rigidbody.AddForce(directionToInitialPosition * _launchPower);
        rigidbody.gravityScale = 1;
        _birdWasLaunched = true;
        lineRenderer.enabled = false;
    }

    private void OnMouseDrag()
    {
        // Get the bounding box positions
        //Vector3 initialPosition = Camera.main.ScreenToWorldPoint(_initialPosition);
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //_boundingBox.x = Mathf.Clamp(mousePos.x, initialPosition.x - boundingBoxWidth, initialPosition.x + boundingBoxWidth);
        //_boundingBox.y = Mathf.Clamp(mousePos.y, initialPosition.y - boundingBoxWidth, initialPosition.y + boundingBoxWidth);
        //_boundingBox.z = 0;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
}
