Shader "Unlit/Dissolve"
{
    Properties
    {
        _MainTex ("Albedo Texture", 2D) = "white" {}
        _Glow ("Glow", Color) = (1,1,1,1)
        _Dissolve ("Dissolve Threshold", Range(0, 1)) = 0.5
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}
        _ExtrudeAmount ("Extrude Amount", float) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _DissolveTex;
            float4 _MainTex_ST;
            float _Dissolve;
            float _ExtrudeAmount;
            float4 _Glow; 

            v2f vert (appdata v)
            {
                v2f o;
                // v.vertex.xyz += v.normal.xyz * _ExtrudeAmount * sin(_Time.y);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                // fixed4 col = tex2D(_MainTex, i.uv);
                // col.y = _Noise;
                // return col;

                float4 textureColour = tex2D(_MainTex, i.uv);
			    float4 dissolveColour = tex2D(_DissolveTex, i.uv);
				clip(dissolveColour.rgb - _Dissolve);
                return textureColour * _Glow;
            }
            ENDCG
        }
    }
}
