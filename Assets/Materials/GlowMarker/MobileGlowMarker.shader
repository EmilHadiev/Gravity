Shader "Custom/MobileGlowMarker"
{
    Properties
    {
        _MainColor ("Glow Color", Color) = (0, 1, 1, 1)
        _RimPower ("Rim Power", Range(0.5, 8.0)) = 3.0
        _AlphaPower ("Transparency", Range(0.0, 1.0)) = 0.5
        _HeightFade ("Height Fade", Range(0.0, 5.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Blend SrcAlpha One 
        ZWrite Off 
        Cull Off 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            // 1. Эта строка включает поддержку GPU Instancing в Unity
            #pragma multi_compile_instancing
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                
                // 2. Добавляем ID инстанса во входные данные
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
                float3 normal : NORMAL;
                
                // 3. Добавляем ID инстанса в выходные данные вертекса
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            float4 _MainColor;
            float _RimPower;
            float _AlphaPower;
            float _HeightFade;

            v2f vert (appdata v)
            {
                v2f o;

                // 4. Настраиваем ID инстанса (обязательно в начале шейдера)
                UNITY_SETUP_INSTANCE_ID(v);
                // Передаем ID дальше в фрагментный шейдер
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 5. Настраиваем ID инстанса в пиксельном шейдере
                UNITY_SETUP_INSTANCE_ID(i);

                float3 normal = normalize(i.normal);
                float3 viewDir = normalize(i.viewDir);

                float rim = 1.0 - saturate(dot(viewDir, normal));
                float glow = pow(rim, _RimPower);
                float verticalFade = i.uv.y * _HeightFade; 

                fixed4 col = _MainColor;
                col.a = glow * _AlphaPower * verticalFade;
                col.rgb *= col.a * 2.0; 

                return col;
            }
            ENDCG
        }
    }
}