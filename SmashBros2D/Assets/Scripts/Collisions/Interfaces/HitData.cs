// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

namespace SmashBros2D
{
    public class HitData
    {
        public int damage;
        
        public Vector2 hitPoint  ;
        public Vector2 hitNormal ;

        public IHurtbox     hurtbox     ;
        public IHitDetector hitDetector ;

        public bool Validate()
        {
            if (hurtbox != null)
            {
                if (hurtbox.CheckHit(this))
                {
                    if (hurtbox.hurtResponder == null || hurtbox.hurtResponder.CheckHit(this))
                    {
                        if (hitDetector.hitResponder == null || hitDetector.hitResponder.CheckHit(this))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }


    public enum HurtboxType
    {
        Player = 1 << 0 ,
        Enemy = 1 << 1,
        Ally = 1 << 2
    }
    [System.Flags]
    public enum HurtboxMask
    {
        None = 0,
        Player = 1 << 0,
        Enemy = 1 << 1,
        Ally = 1 << 2
    }

    public interface IHitResponder
    {
        int Damage { get; }

        public bool CheckHit(HitData data);

        public void Response(HitData data);
    }

    public interface IHitDetector
    {
        public IHitResponder hitResponder {get; set; }
        
        public void CheckHit(HitData data);
    }

    public interface IHurtResponder
    {
        public bool CheckHit(HitData data);

        public void Response(HitData data);
    }

    public interface IHurtbox
    {
        public bool           active        { get ; }
        public GameObject     owner         { get ; }
        // public Transform      transform     { get ; }
        public HurtboxType    type          { get ; }
        public IHurtResponder hurtResponder { get ; set; }
        
        public bool CheckHit(HitData hitData);
    }
}
