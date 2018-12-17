Shader "Unlit/Dissolver"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "yellow" {}
        _SimpleNoise("TextureNoise", 2D) = "white"{}
        _Color1("edge color", Color) = (1, 0.84, 1, 0)
        _Color2("edge color2", Color) = (1.0,1.0,1.0,1.0)
        _Threshold("cut out level", Range(0.0,1.0))= 0.3
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        Lighting Off
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
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			
			float4 _MainTex_ST;
            float4 _Color1;
            float4 _Color2;
            float _Threshold;
           
            sampler2D _MainTex;
            sampler2D _SimpleNoise;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				
                
                o.vertex = UnityPixelSnap (o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 col = tex2D(_MainTex, i.uv);
				float noise = tex2D(_SimpleNoise, i.uv).b;
                

                clip(noise - _Threshold);

                if(noise < _Threshold + 0.030)
                    col =lerp(_Color1, _Color2, 0.030 );

                return col;
                
                
			}
			ENDCG
		}
	}
}
