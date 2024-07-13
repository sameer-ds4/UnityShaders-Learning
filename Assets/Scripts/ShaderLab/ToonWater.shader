Shader "Pixel Sorcerer/Toon Water"
{
    Properties
    {	
        _DepthGradientShallow("Depth Gradient Shallow", Color) = (0.3, 0.8, 0.9, 0.7)
        _DepthGradientDeep("Depth Gradient Deep", Color) = (0.08, 0.4, 1, 0.7)
        _DepthMaxDistance("Depth Max Distance", Float) = 1
        _SurfaceNoise("Surface Noise", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screenPosition : TEXCOORD2;
                float2 noiseUV : TEXCOORD0;
            };

            sampler2D _SurfaceNoise;
            float4 _SurfaceNoise_ST;

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPosition = ComputeScreenPos(o.vertex);
                o.noiseUV = TRANSFORM_TEX( v.uv, _SurfaceNoise);

                return o;
            }

            float4 _DepthGradientDeep;
            float4 _DepthGradientShallow;
            
            float _DepthMaxDistance;

            sampler2D _CameraDepthTexture;

            float4 frag (v2f i) : SV_Target
            {
                float existingDepth01 = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPosition)).r;
                float existingDepthLinear = LinearEyeDepth(existingDepth01);

                float depthDifference = existingDepthLinear - i.screenPosition.w;

                float waterDepthDifference01 = saturate(depthDifference / _DepthMaxDistance);
                float4 waterColor = lerp(_DepthGradientShallow, _DepthGradientDeep, waterDepthDifference01);

                return waterColor;
            }
            ENDCG
        }
    }
}