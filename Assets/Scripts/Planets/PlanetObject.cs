using UnityEngine;
using UnityEngine.UI;

namespace SpaceExploration.Planets
{
    public class PlanetObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Text text;

        public void Init(Sprite s, int rating, bool showText=true)
        {
            spriteRenderer.sprite = s;
            text.text = rating.ToString();
            ShowText(showText);
        }

        public void ShowText(bool show)
        {
            text.enabled = show;
        }
    }
}