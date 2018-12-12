Shader "Unlit/ParticleShader"
{
	Properties
	{
		_PlayerPos("Player position", Vector) = (0, 0, 0, 0)
		_FadeInValue("Fade in Value", float) = 100.0
		_FadeDuration("Fade duration", float) = 300.0
	}
	SubShader
	{
		//Tags { "RenderType"="Opaque" }
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }

		ZWrite Off
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
			};

			struct vertToFrag
			{
				float4 vertex : SV_POSITION;
				float3 worldPosition : TEXCOORD1;
			};

			Vector _PlayerPos;
			float _FadeInValue;
			float _FadeDuration;
			
			vertToFrag vert (appdata v)
			{
				vertToFrag output;				
				output.vertex = UnityObjectToClipPos(v.vertex);

				float3 worldPos = mul(unity_ObjectToWorld, output.vertex).xyz;
				output.worldPosition = worldPos;

				return output;
			}
			
			fixed4 frag (vertToFrag i) : SV_Target
			{
				float dist = distance(i.worldPosition, _PlayerPos);
				
				if(dist < _FadeInValue)
				{
					fixed4 col;
					col = fixed4(1, 0.84, 0, 1);
					return col;
				}
				else if (dist > _FadeInValue)
				{
					fixed4 col;
					float alphaV = abs((dist - _FadeInValue) / _FadeDuration);
					col = fixed4(1, 0.84, 0, alphaV);
					return col;
				}
				else if(dist > _FadeInValue + _FadeDuration)
				{ 
					fixed4 col;
					col = fixed4(1, 0.84, 1, 0);
					return col;
				}	
				return fixed4(1, 0, 0, 1);
			}
			ENDCG
		}
	}
}
