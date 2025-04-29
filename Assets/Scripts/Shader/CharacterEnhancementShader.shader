// SimpleCharacterShader.shader
Shader "Custom/SimpleCharacterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0.0, 0.0, 0.0, 1.0)
        _OutlineWidth ("Outline Width", Range(0.001, 0.03)) = 0.005
        _RimColor ("Rim Color", Color) = (0.26, 0.19, 0.16, 0.0)
        _RimPower ("Rim Power", Range(0.5, 8.0)) = 3.0
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        
        // メインパス（キャラクター本体）
        Pass
        {
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _RimColor;
            float _RimPower;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                // 基本テクスチャ
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // リムライト効果
                float rim = 1.0 - saturate(dot(i.viewDir, i.worldNormal));
                col.rgb += _RimColor.rgb * pow(rim, _RimPower) * col.a;
                
                return col;
            }
            ENDCG
        }
        
        // アウトライン用のパス
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }
            Cull Front
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            float _OutlineWidth;
            fixed4 _OutlineColor;
            sampler2D _MainTex;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.uv = v.vertex;
                
                // アウトライン効果
                float3 normal = normalize(v.normal);
                float3 pos = v.vertex.xyz + normal * _OutlineWidth;
                o.pos = UnityObjectToClipPos(float4(pos, 1.0));
                
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                // テクスチャのアルファを取得してアウトラインに適用
                fixed alpha = tex2D(_MainTex, i.uv).a;
                return fixed4(_OutlineColor.rgb, alpha * _OutlineColor.a);
            }
            ENDCG
        }
    }
    FallBack "Sprites/Default"
}