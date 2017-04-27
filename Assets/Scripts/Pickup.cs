﻿using UnityEngine;

public class Pickup : Entity
{
    new SpriteRenderer renderer;
    static MaterialPropertyBlock materialProperties;
    bool _selected = false;

    public static Pickup Create(Vector3 position, string code)
    {
        var gameObject = new GameObject("pickup " + code);
        gameObject.transform.position = position;
        var spritesheet = DC6.Load(@"data\global\items\flp" + code + ".dc6");
        var animator = gameObject.AddComponent<SpriteAnimator>();
        animator.sprites = spritesheet.GetSprites(0);
        animator.loop = false;
        var pickup = gameObject.AddComponent<Pickup>();
        return pickup;
    }

    private void Awake()
    {
        if (materialProperties == null)
            materialProperties = new MaterialPropertyBlock();
    }

    protected override void Start()
    {
        base.Start();
        renderer = GetComponent<SpriteRenderer>();
    }

    public override bool selected
    {
        get { return _selected; }
        set
        {
            if (_selected != value)
            {
                _selected = value;
                Materials.SetRendererHighlighted(renderer, _selected);
            }
        }
    }

    public override Vector2 titleOffset
    {
        get { return new Vector2(0, 24); }
    }

    public override Bounds bounds
    {
        get { return renderer.bounds; }
    }

    public override void Operate(Character character = null)
    {
        Destroy(gameObject);
    }

    private void OnRenderObject()
    {
        MouseSelection.Submit(this);
    }
}