using UnityEngine;

public class Invaders : MonoBehaviour
{
  public Invader[] prefabs;
  public int rows = 5;
  public int columns = 11;

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
              Vector3 position = rowPosition;
              position.x += column * 2.0f;
              invader.transform.position = position;
          }
      }
  }
}
