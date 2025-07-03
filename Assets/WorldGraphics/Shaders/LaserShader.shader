Shader "Custom/EmissionShaderURP"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _EmissionIntensity ("Emission Intensity", Range(0, 50)) = 1
        _ScatterIntensity ("Scatter Intensity", Range(0, 15)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 100

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            // Properties
            float4 _BaseColor;
            float4 _EmissionColor;
            float _EmissionIntensity;
            float _ScatterIntensity;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 normalWS : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            Varyings vert (Attributes v)
            {
                Varyings o;
                o.positionCS = TransformObjectToHClip(v.positionOS);
                o.normalWS = TransformObjectToWorldNormal(v.normalOS);
                o.uv = v.uv;
                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                // Base color
                half4 baseColor = _BaseColor;

                // Calculate emission
                half3 emission = _EmissionColor.rgb * _EmissionIntensity;

                // Get the main light direction and normalize
                Light mainLight = GetMainLight();
                half3 lightDirection = normalize(mainLight.direction);

                // Scatter effect based on light direction and surface normal
                half scatterFactor = saturate(dot(i.normalWS, lightDirection));
                half3 scatterColor = scatterFactor * _ScatterIntensity * baseColor.rgb;

                // Final color output
                return half4(baseColor.rgb + emission + scatterColor, baseColor.a);
            }
            ENDHLSL
        }
        
    }

    FallBack "Diffuse"
}
