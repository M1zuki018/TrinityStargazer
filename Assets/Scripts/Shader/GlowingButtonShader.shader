// GlowingButtonShader.shader
Shader "Custom/GlowingButtonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (0,0,0,1)
        _GlowColor ("Glow Color", Color) = (0.5,0.5,0.5,1)
        _GlowIntensity ("Glow Intensity", Range(0,2)) = 0.5
        _EdgeGlowStrength ("Edge Glow Strength", Range(0,5)) = 1.0
        _PulseSpeed ("Pulse Speed", Range(0,5)) = 1.0
        _PulseAmount ("Pulse Amount", Range(0,0.5)) = 0.1
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
            float _EdgeGlowStrength;
            float _PulseSpeed;
            float _PulseAmount;
            
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
                
                // エッジ検出用の距離計算
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);
                
                // エッジでの発光効果
                float edgeGlow = smoothstep(0.4, 0.5, dist) * _EdgeGlowStrength;
                
                // 時間に基づくパルス効果
                float pulse = _PulseAmount * sin(_Time.y * _PulseSpeed);
                
                // 発光効果を適用
                float glowEffect = edgeGlow + pulse;
                float4 glowColor = _GlowColor * _GlowIntensity * glowEffect;
                
                // 最終カラーの計算
                float4 finalColor = col + glowColor;
                finalColor.a = col.a; // アルファ値を維持
                
                return finalColor;
            }
            ENDCG
        }
    }
}