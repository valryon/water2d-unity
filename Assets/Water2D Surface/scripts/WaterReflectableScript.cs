// 2016 - Damien Mayance (@Valryon)
// Source: https://github.com/valryon/water2d-unity/
using UnityEngine;

/// <summary>
/// Automagically create a water reflect for a sprite.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class WaterReflectableScript : MonoBehaviour
{
  #region Members

  [Header("Reflect properties")]
  public Vector3 localPosition = new Vector3(0, -0.25f, 0);
  public Vector3 localRotation = new Vector3(0, 0, -180);
  public string spriteLayer = "Default";
  public int spriteLayerOrder = -5;

  private SpriteRenderer spriteSource;
  private SpriteRenderer spriteRenderer;

  #endregion

  #region Timeline

  void Awake()
  {
    GameObject reflectGo = new GameObject("Water Reflect");
    reflectGo.transform.parent = this.transform;
    reflectGo.transform.localPosition = localPosition;
    reflectGo.transform.localRotation = Quaternion.Euler(localRotation);
    reflectGo.transform.localScale = new Vector3(reflectGo.transform.localScale.x, reflectGo.transform.localScale.y, reflectGo.transform.localScale.z);

    spriteRenderer = reflectGo.AddComponent<SpriteRenderer>();
    spriteRenderer.sortingLayerName = spriteLayer;
    spriteRenderer.sortingOrder = spriteLayerOrder;
    
    spriteSource = GetComponent<SpriteRenderer>();
  }

  void OnDestroy()
  {
    if (spriteRenderer != null)
    {
      Destroy(spriteRenderer.gameObject);
    }
  }

  void LateUpdate()
  {
    if (spriteSource != null)
    {
      sprite.sprite = spriteSource.sprite;
      sprite.color = spriteSource.color;
        spriteRenderer.sprite = sprite;
      spriteRenderer.flipX = spriteSource.flipX;
      spriteRenderer.flipY = spriteSource.flipY;
      spriteRenderer.color = spriteSource.color;
    }
  }

  #endregion
}