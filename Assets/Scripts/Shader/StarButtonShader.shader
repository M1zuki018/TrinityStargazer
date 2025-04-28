// StarButtonShader.shader
Shader "Custom/StarButtonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _GlowColor ("Glow Color", Color) = (0.5,0.5,1,1)
        _GlowIntensity ("Glow Intensity", Range(0,2)) = 1.0
        _OutlineWidth ("Outline Width", Range(0,0.1)) = 0.02
        _OutlineColor ("Outline Color", Color) = (0.7,0.7,1,1)
        _PulseSpeed ("Pulse Speed", Range(0,5)) = 1.0
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
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
            float4 _MainTex_ST;
            float4 _Color;
            float4 _GlowColor;
            float _GlowIntensity;
            float _OutlineWidth;
            float4 _OutlineColor;
            float _PulseSpeed;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            float4 frag (v2f i) : SV_Target
            {
                // サンプルテクスチャ
                float4 col = tex2D(_MainTex, i.uv) * _Color;
                
                // エッジ検出（アウトラインエフェクト用）
                float uvOffsetValue = _OutlineWidth; // スカラー値として宣言
                float4 edgeCol = tex2D(_MainTex, i.uv + float2(uvOffsetValue, 0)) +
                    tex2D(_MainTex, i.uv - float2(uvOffsetValue, 0)) +
                    tex2D(_MainTex, i.uv + float2(0, uvOffsetValue)) +
                    tex2D(_MainTex, i.uv - float2(0, uvOffsetValue));
                edgeCol = saturate(edgeCol);
                
                // パルスエフェクト
                float pulse = 0.5 + 0.5 * sin(_Time.y * _PulseSpeed);
                float4 glow = _GlowColor * pulse * _GlowIntensity;
                
                // 中心からの距離に基づくエフェクト
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);
                float radialGlow = smoothstep(0.5, 0.2, dist) * 0.5 * pulse;
                
                // アウトラインエフェクト
                float outline = saturate(edgeCol.a - col.a) * _OutlineColor.a;
                float4 outlineColor = outline * _OutlineColor;
                
                // 最終的な色の組み合わせ
                float4 finalColor = col + glow * col.a + outlineColor + radialGlow * _GlowColor;
                finalColor.a = col.a;
                
                return finalColor;
            }
            ENDCG
        }
    }
}