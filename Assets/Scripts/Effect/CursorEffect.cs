using DG.Tweening;
using UnityEngine;

public class CursorEffect : MonoBehaviour
{
    [Header("Arrows")]
    public MeshRenderer[] renderers;
    public Material material;
    public float duration = 0.4f;
    public float shiftEnd = 1.0f;

    [Header("Ring")] 
    public GameObject ring;
    public float ringDuration = 0.4f;
    public float initialScale = 0.08f;
    
    Material instancedMaterial;
    Tweener arrowTweener;
    Tweener ringTweener;
    
    static readonly int shift = Shader.PropertyToID("_Shift");

    static CursorEffect _instance;

    void Awake()
    {
        instancedMaterial = new Material(material);
        foreach (MeshRenderer meshRenderer in renderers)
        {
            meshRenderer.sharedMaterial = instancedMaterial;
        }

        instancedMaterial.SetFloat(shift, -1.0f);
    }

    void OnArrowStart()
    {
        foreach (MeshRenderer meshRenderer in renderers)
        {
            meshRenderer.enabled = true;
        }
    }

    void OnArrowComplete()
    {
        //LOD 문제로 끄기
        foreach (MeshRenderer meshRenderer in renderers)
        {
            meshRenderer.enabled = false;
        }
    }

    void OnRingStart()
    {
        
    }

    void OnRingComplete()
    {
        
    }

    public static void Spawn(Vector3 pos, Transform parent)
    {
        if (_instance != null)
        {
            _instance.StopAnimation();
        }
        else
        {
            _instance = Instantiate(Resources.Load<CursorEffect>("CursorEffect"), parent);
            _instance.transform.position = pos;
            _instance.transform.rotation = Quaternion.identity;
            
            _instance.SetupAnimation();
        }
        
        _instance.StartAnimation(pos);
    }

    void SetupAnimation()
    {
        arrowTweener = instancedMaterial.DOFloat(shiftEnd, shift, duration);
        arrowTweener.OnPlay(OnArrowStart);
        arrowTweener.OnComplete(OnArrowComplete);
        arrowTweener.SetAutoKill(false);
        
        ringTweener = ring.transform.DOScale(0.0f, ringDuration);
        ringTweener.OnPlay(OnRingStart);
        ringTweener.OnComplete(OnRingComplete);
        ringTweener.SetAutoKill(false);
    }

    void StartAnimation(Vector3 pos)
    {
        _instance.transform.position = pos;
        ring.transform.localScale = Vector3.one * initialScale;

        arrowTweener.Restart(false);
        ringTweener.Restart(false);
    }

    void StopAnimation()
    {
        arrowTweener.Pause();
        ringTweener.Pause();
    }
}