using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potion : Item
{
    // Start is called before the first frame update
    public float Value;
    protected GameObject EffectHealth;
    protected float level;

    protected override void Start()
    {
        base.Start();
        EffectHealth = (GameObject)Resources.Load("Effect/EffectPotions");
        Collect = SFXManager.PotionCollect;
        level = float.Parse(gameObject.name.Substring(gameObject.name.IndexOf("Potion") - 1, 1));

    }
    protected void MakeEffect(GameObject Player,string NamePotion)
    {
        var main = EffectHealth.GetComponent<ParticleSystem>().main;
        switch (NamePotion)
        {
            case "Health":
                main.startColor = new Color(0.9058824f, 0.2352941f, 0.2235294f, 0.8f);
                break;
            case "Speed":
                main.startColor = new Color(0.3215686f, 0.2196079f, 0.4509804f, 0.8f);
                break;
            case "Jump":
                main.startColor = new Color(0.3882353f, 0.6509804f, 0.1607843f, 0.8f);
                break;
            case "Dame":
                main.startColor = new Color(1f, 0.6509804f, 0.2901961f, 0.8f);
                break;
        }
        main.duration = 0.5f * level;
        GameObject effect = Instantiate(EffectHealth, Player.transform);
        Object.Destroy(effect, 2f);
    }
}
