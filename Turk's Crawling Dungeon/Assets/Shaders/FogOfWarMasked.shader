Shader "TCD/Unlit Fog Of War Masked"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Background ("Background", 2D) = "white" {}
        _ScrollSpeed("Scroll Speed", Float) = 0
        _Cutoff("Alpha cutoff", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags
        { 
            "RenderType" = "Transparent" 
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry" 
        }

        Blend SrcAlpha OneMinusSrcAlpha
        AlphaTest GEqual[_Cutoff]
        LOD 100

        Pass
        {

            Stencil
            {
                Ref 2
                Comp GEqual
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _Background;
            float4 _Background_ST;
            float _ScrollSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                v.uv.x += _Time * _ScrollSpeed;
                v.uv.y += _Time * _ScrollSpeed;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                i.uv.x %= _Background_ST.x;
                i.uv.y %= _Background_ST.y;

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col_0 = tex2D(_Background, i.uv);
                fixed4 col_1 = lerp(col, col_0, 0.3);
                col_1[3] = min(col[3], col_0[3]);

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col_1);

                return col_1;
            }
            ENDCG
        }
    }
}
