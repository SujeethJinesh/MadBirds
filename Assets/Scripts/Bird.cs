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
        transform.position = GetBoundedDragMovement();
    }

    private Vector3 GetBoundedDragMovement()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = Mathf.Clamp(mousePos.x, _initialPosition.x - boundingBoxWidth, _initialPosition.x + boundingBoxWidth);
        mousePos.y = Mathf.Clamp(mousePos.y, _initialPosition.y - boundingBoxWidth, _initialPosition.y + boundingBoxWidth);
        mousePos.z = 0;
        return mousePos;
    }
}
