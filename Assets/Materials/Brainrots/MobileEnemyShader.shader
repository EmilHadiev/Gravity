Shader "Custom/MobileEnemyShader"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Base Texture", 2D) = "white" {}
        // Основной цвет будет умножаться на текстуру. Его можно менять через скрипт.
        _Color ("Base Color (Instanced)", Color) = (1,1,1,1)
        
        // Настройки фейкового света
        // Направление света зашито в коде для скорости, но силу можно менять
        _LightStrength ("Fake Light Strength", Range(0, 2)) = 1.0
        _AmbientStrength ("Ambient Brightness", Range(0, 1)) = 0.3
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // Обязательно для инстансинга
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                // Нам нужны нормали для расчета объема
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                // Передаем нормаль в мировом пространстве в фрагментный шейдер
                half3 worldNormal : TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            sampler2D _MainTex;
            half _LightStrength;
            half _AmbientStrength;
            
            // Жестко задаем направление фейкового "солнца" (сверху-справа-спереди)
            // normalize(float3(X, Y, Z))
            static const half3 _FakeLightDir = normalize(half3(0.5, 1.0, -0.5));

            // Блок инстансируемых свойств. Сюда кладем то, что меняется скриптом.
            UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
            UNITY_INSTANCING_BUFFER_END(Props)

            v2f vert (appdata v) {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv; // Для оптимизации убрали TRANSFORM_TEX, нужен [NoScaleOffset] в свойствах
                
                // Преобразуем нормаль объекта в мировое пространство
                // Используем half для мобильной оптимизации
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                UNITY_SETUP_INSTANCE_ID(i);

                // 1. Получаем базовую текстуру
                fixed4 texCol = tex2D(_MainTex, i.uv);
                
                // 2. Получаем уникальный цвет инстанса
                fixed4 instColor = UNITY_ACCESS_INSTANCED_PROP(Props, _Color);

                // 3. РАСЧЕТ ФЕЙКОВОГО СВЕТА
                // Нормализуем интерполированную нормаль
                half3 normal = normalize(i.worldNormal);
                // Считаем "скалярное произведение" (Dot Product).
                // Результат от -1 (отвернут от света) до 1 (смотрит на свет).
                half NdotL = dot(normal, _FakeLightDir);
                
                // "Оборачиваем" свет (Wrapped Diffuse).
                // Это делает тени мягче, результат от 0 до 1. Идеально для стилизации.
                half lightIntensity = NdotL * 0.5 + 0.5;
                
                // Применяем силу света и добавляем "амбиент", чтобы тени не были черными
                half finalLight = (lightIntensity * _LightStrength) + _AmbientStrength;

                // Собираем все вместе: Текстура * Цвет Инстанса * Свет
                fixed4 finalColor = texCol * instColor * finalLight;

                // Восстанавливаем альфу (если нужна)
                finalColor.a = texCol.a * instColor.a;
                
                return finalColor;
            }
            ENDCG
        }
    }
}