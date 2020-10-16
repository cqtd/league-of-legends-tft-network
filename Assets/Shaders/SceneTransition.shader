Shader "Transition/Radial Transition"
{
    Properties
    {
        _Pivot ("Pivot", Vector) = (0.5, 0.5, 0, 0)
        [HideInInspector] _ScreenRatio ("Screen Ratio", Vector) = (1, 0.563, 0, 0)
        _Radius ("Radius" , Range(0, 2)) = 0
        _Color ("Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        LOD 100

        Pass
        {
            CGPROGRAM

            #pragma target 3.5
            #pragma vertex vert
            #pragma fragment frag Lambert alpha
            #pragma multi_compile_fog
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float4 _Pivot;
            float4 _ScreenRatio;
            float4 _Color;
            float _Radius;

            v2f vert (appdata v)
            {
                   v2f o;
                   o.vertex = UnityObjectToClipPos(v.vertex);
                   o.uv = v.uv;

                   UNITY_TRANSFER_FOG(o,o.vertex);
                   return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float2 texCoord = (i.uv - float2(_Pivot.x, _Pivot.y)) * _ScreenRatio.xy;
                float distance = sqrt(pow(texCoord.x, 2) + pow(texCoord.y, 2));
                float radial = distance;

                if (radial < _Radius) discard;

                return _Color.rgba;
            }
            ENDCG
        }
    }
}