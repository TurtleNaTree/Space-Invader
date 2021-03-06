using UnityEngine;
using UnityEngine.SceneManagement; // need to reset the scene when the player touches a invader, missile or if all invaders die
public class Invaders : MonoBehaviour
{
  public Invader[] prefabs;
  public int rows = 5;
  public int columns = 11;

  public AnimationCurve speed;
  public Projectile missilePrefab;
  public float missileAttachRate;
  public int ammountKilled { get; private set; }
  public int amountAlive => this.totalInvaders - this.ammountKilled;
  public int totalInvaders => this.rows * this.columns; // => computed or calculated property google later

  public float percentKilled => (float)this.ammountKilled / this.totalInvaders;
  private Vector3 _direction = Vector2.right;

  private void Awake()
  {
      for (int row = 0; row < this.rows; row++)
      {
          float width = 2.0f * (this.columns - 1);
          float height = 2.0f * (this.rows - 1);
          Vector3 centering = new Vector2(-width / 2, -height / 2);
          Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);

          for (int column = 0; column < this.columns; column++)
          {
              Invader invader = Instantiate(this.prefabs[row], this.transform);
              invader.killed += InvaderKilled;
              Vector3 position = rowPosition;
              position.x += column * 2.0f;
              invader.transform.localPosition = position;
          }
      }
  }

  private void Start()
  {
      InvokeRepeating(nameof(missileAttack), this.missileAttachRate, this.missileAttachRate);
  }
  private void Update()
  {
      this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

      Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
      Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);


      foreach (Transform invader in this.transform)
      {
          if (!invader.gameObject.activeInHierarchy) {
              continue;
          }

          if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f)) // the 1.0f is to add a little padding
            {
                AdvanceRow();
            }
          else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
          {
                AdvanceRow();
          }
      }
  }

  private void AdvanceRow()
      {
          _direction.x *= -1.0f;

          Vector3 position = this.transform.position;
          position.y -= 1.0f;
          this.transform.position = position;
      }

    private void missileAttack()
    {
        foreach (Transform invader in this.transform)
      {
          if (!invader.gameObject.activeInHierarchy) {
              continue;
          }

          if (Random.value < (1.0f / (float)this.amountAlive)) {
              Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
              break; // so only one missile can spawn at a time
          }
      }
    }
    private void InvaderKilled()
    {
        this.ammountKilled++;

        if (this.ammountKilled >= this.totalInvaders){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    

}
