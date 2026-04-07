using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;
    public float fallMultiplier = 2.5f;
    public float dashTimeScale = 2.0f;
    public float moveSpeed = 5f;

    [Header("Awakening Ability")]
    public float awakenDuration = 5f;
    public float shockwaveRadius = 10f;
    public float auraRadius = 2.5f;
    private bool isAwakened = false;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool isGrounded = false;
    private float minX;
    private float maxX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (Camera.main != null)
        {
            float zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
            Vector3 leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance));
            Vector3 rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zDistance));

            float halfWidth = col != null ? col.bounds.extents.x : 0.5f;
            minX = leftBottom.x + halfWidth;
            maxX = rightTop.x - halfWidth;
        }
        else
        {
            minX = -8f;
            maxX = 8f;
        }
    }

    void Update()
    {
        CheckGrounded();

        if (Input.GetKeyDown(KeyCode.A) && !isAwakened)
        {
            StartCoroutine(AwakenPowerRoutine());
        }

        if (isAwakened)
        {
            DestroySurroundingObstacles(auraRadius);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }

        if (rb != null && rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        }

        if (rb != null)
        {
            Vector2 vel = rb.linearVelocity;

            if (Input.GetKey(KeyCode.S))
            {
                Time.timeScale = dashTimeScale;
                vel.x = transform.position.x < maxX ? moveSpeed : 0f;
            }
            else
            {
                Time.timeScale = 1f;
                vel.x = transform.position.x > minX ? -moveSpeed : 0f;
            }

            rb.linearVelocity = vel;

            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            transform.position = pos;
        }
    }

    private void CheckGrounded()
    {
        isGrounded = false;
        if (col == null) return;

        Bounds bounds = col.bounds;
        float rayLength = 0.15f;

        Vector2 left = new Vector2(bounds.min.x + 0.05f, bounds.min.y);
        Vector2 center = new Vector2(bounds.center.x, bounds.min.y);
        Vector2 right = new Vector2(bounds.max.x - 0.05f, bounds.min.y);

        RaycastHit2D hitLeft = Physics2D.Raycast(left, Vector2.down, rayLength);
        RaycastHit2D hitCenter = Physics2D.Raycast(center, Vector2.down, rayLength);
        RaycastHit2D hitRight = Physics2D.Raycast(right, Vector2.down, rayLength);

        if (IsValidGround(hitLeft) || IsValidGround(hitCenter) || IsValidGround(hitRight))
        {
            isGrounded = true;
        }
    }

    private bool IsValidGround(RaycastHit2D hit)
    {
        return hit.collider != null && hit.collider != col && !hit.collider.isTrigger;
    }

    private System.Collections.IEnumerator AwakenPowerRoutine()
    {
        isAwakened = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr != null ? sr.color : Color.white;
        Vector3 originalScale = transform.localScale;

        // 1. 역동적인 히트 스톱 (시간 정지 & 극단적 스케일업 & 화면 쉐이크)
        float prevTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        if (sr != null) sr.color = Color.white; // 순간적인 섬광 효과
        transform.localScale = originalScale * 1.5f; // 크게 부풀어 오름

        StartCoroutine(CameraShake(0.5f, 0.4f));

        yield return new WaitForSecondsRealtime(0.1f); // 시간 정지 0.1초 멈춤 효과

        // 2. 대폭발과 함께 슬로우 모션
        Time.timeScale = 0.3f;
        if (sr != null) sr.color = Color.red;
        DestroySurroundingObstacles(shockwaveRadius, 1.5f); // 1.5배 크기의 폭발 파티클

        yield return new WaitForSecondsRealtime(0.3f);

        // 3. 시간 및 크기 복원
        transform.localScale = originalScale;
        if (Input.GetKey(KeyCode.S)) Time.timeScale = dashTimeScale;
        else Time.timeScale = 1f;

        // 4. 각성 지속 상태 (역동적인 스케일 맥박, 오라 색상, 잔상)
        float elapsed = 0f;
        float effectTimer = 0f;

        while (elapsed < awakenDuration)
        {
            elapsed += Time.deltaTime;
            effectTimer += Time.deltaTime;

            // 색상 깜빡임 고속화
            if (sr != null)
            {
                sr.color = Color.Lerp(new Color(1f, 0.2f, 0.2f), Color.yellow, Mathf.PingPong(Time.time * 20f, 1f));
            }

            // 플레이어 크기가 심장박동처럼 쿵쾅거림
            float pulse = 1f + Mathf.Sin(Time.time * 25f) * 0.15f;
            transform.localScale = originalScale * pulse;

            // 0.05초 단위로 플레이어 잔상(AfterImage) 생성
            if (effectTimer > 0.05f)
            {
                effectTimer = 0f;
                CreateAfterImage(sr);
            }

            yield return null;
        }

        // 5. 각성 종료
        if (sr != null) sr.color = originalColor;
        transform.localScale = originalScale;
        isAwakened = false;
    }

    private System.Collections.IEnumerator CameraShake(float duration, float magnitude)
    {
        if (Camera.main == null) yield break;
        Vector3 originalPos = Camera.main.transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            Camera.main.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        Camera.main.transform.localPosition = originalPos;
    }

    private void CreateAfterImage(SpriteRenderer sr)
    {
        if (sr == null) return;
        GameObject afterImage = new GameObject("AfterImage");
        afterImage.transform.position = transform.position;
        afterImage.transform.localScale = transform.localScale;

        SpriteRenderer aisr = afterImage.AddComponent<SpriteRenderer>();
        aisr.sprite = sr.sprite;
        aisr.color = new Color(1f, 0f, 0f, 0.6f); // 반투명 붉은색 잔상
        aisr.sortingOrder = sr.sortingOrder - 1;

        StartCoroutine(FadeOutAfterImage(afterImage, aisr));
    }

    private System.Collections.IEnumerator FadeOutAfterImage(GameObject obj, SpriteRenderer sr)
    {
        float t = 0f;
        Color c = sr.color;
        while (t < 0.3f)
        {
            t += Time.unscaledDeltaTime;
            c.a = Mathf.Lerp(0.6f, 0f, t / 0.3f);
            if (sr != null) sr.color = c;
            yield return null;
        }
        Destroy(obj);
    }

    private void DestroySurroundingObstacles(float radius, float particleScale = 0.5f)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in colliders)
        {
            if (hit.gameObject != this.gameObject)
            {
                if (hit.CompareTag("Obstacle") || hit.CompareTag("Enemy") || hit.isTrigger)
                {
                    SpawnExplosionParticle(hit.transform.position, particleScale);
                    Destroy(hit.gameObject);
                }
            }
        }
    }

    private void SpawnExplosionParticle(Vector3 pos, float sizeMultiplier = 1f)
    {
        GameObject explosion = new GameObject("ExplosionParticle");
        explosion.transform.position = pos;

        // 파티클 시스템 동적 생성
        ParticleSystem ps = explosion.AddComponent<ParticleSystem>();

        var main = ps.main;
        main.duration = 1f;
        main.startColor = new ParticleSystem.MinMaxGradient(Color.red, Color.yellow);
        main.startLifetime = new ParticleSystem.MinMaxCurve(0.3f, 0.7f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(5f * sizeMultiplier, 15f * sizeMultiplier);
        main.startSize = new ParticleSystem.MinMaxCurve(0.2f * sizeMultiplier, 0.6f * sizeMultiplier);
        main.maxParticles = 100;
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        var emission = ps.emission;
        emission.rateOverTime = 0;
        emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, (short)(30 * sizeMultiplier)) });

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.5f;

        var sizeOverLifetime = ps.sizeOverLifetime;
        sizeOverLifetime.enabled = true;
        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f, 0f); // 점점 작아지게 연출

        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        // 기본 2D 스프라이트 머티리얼을 찾아 할당하여 네모난 픽셀 파티클 룩을 연출
        Shader defaultShader = Shader.Find("Sprites/Default");
        if (defaultShader != null) renderer.material = new Material(defaultShader);

        ps.Play();
        Destroy(explosion, 1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shockwaveRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, auraRadius);
    }
}
