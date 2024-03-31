Shader "Custom/NoNewTrails"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _GlowTex ("Glow (A)", 2D) = "white" {}
        _ScrollSpeed ("Scroll Speed", Range(0, 10)) = 1
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _GlowTex;
            float4 _Color;
            float _ScrollSpeed;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 offset = float2(_Time.y * _ScrollSpeed, 0);
                fixed4 col = tex2D(_MainTex, i.uv + offset) * _Color;
                col.a *= tex2D(_GlowTex, i.uv + offset).a;
                return col;
            }
            ENDCG
        }
    }
}
