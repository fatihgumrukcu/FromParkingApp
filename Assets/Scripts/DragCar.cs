using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class DragCar : MonoBehaviour
{
    private Vector3 dragStartPos;
    private Vector3 objectStartPos;
    private bool isDragging = false;
    private Camera mainCamera;
    private Rigidbody2D rb;

    public bool isVertical;
    public bool isShortCar;
    public bool isLongCar; // ✅ **Yatay uzun arabalar için eklendi**
    public float baseSpeed = 5f;  
    public float maxSpeed = 12f;  
    public float acceleration = 8f;  

    private CinemachineImpulseSource impulseSource;
    private Vector2 targetVelocity;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        impulseSource = GetComponentInChildren<CinemachineImpulseSource>();

        if (impulseSource == null)
        {
            Debug.LogWarning("⚠️ CinemachineImpulseSource bulunamadı! Kameranın sallanması için bileşeni kontrol et.");
        }
        else
        {
            ResetImpulse();
        }
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * acceleration);
        }
        else
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.fixedDeltaTime * acceleration);
        }
    }

    void OnMouseDown()
    {
        if (mainCamera == null) return;

        dragStartPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        dragStartPos.z = 0;
        objectStartPos = transform.position;
        isDragging = false;
    }

    void OnMouseDrag()
    {
        if (mainCamera == null) return;

        Vector3 currentMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentMousePos.z = 0;
        Vector3 movement = (currentMousePos - dragStartPos);

        if (!isDragging && movement.sqrMagnitude > 0.002f)
        {
            isDragging = true;
        }

        if (isDragging)
        {
            Vector2 moveDirection = Vector2.zero;

            if (isShortCar)
            {
                moveDirection = new Vector2(movement.x, 0);
            }
            else if (isVertical)
            {
                moveDirection = new Vector2(0, movement.y);
            }
            else if (isLongCar) // ✅ **Yatay uzun arabalar sadece X ekseninde hareket edecek**
            {
                moveDirection = new Vector2(movement.x, 0);
            }
            else
            {
                moveDirection = new Vector2(movement.x, movement.y);
            }

            targetVelocity = moveDirection.normalized * Mathf.Clamp(moveDirection.magnitude * baseSpeed, 0, maxSpeed);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (impulseSource != null)
        {
            impulseSource.m_ImpulseDefinition.m_AmplitudeGain = 0.1f;
            impulseSource.m_ImpulseDefinition.m_FrequencyGain = 0.3f;
            impulseSource.GenerateImpulse();
            Invoke(nameof(ResetImpulse), 0.5f);
        }
    }

    void ResetImpulse()
    {
        if (impulseSource != null)
        {
            impulseSource.m_ImpulseDefinition.m_AmplitudeGain = 0f;
            impulseSource.m_ImpulseDefinition.m_FrequencyGain = 0f;
        }
    }

    public void RestartGame()
    {
        ResetImpulse();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
