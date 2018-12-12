Shader "Unlit/ParticleShader"
{
	Properties
	{
		_PlayerPos("Player position", Vector) = (0, 0, 0, 0)
		_FadeInValue("Fade in Value", float) = 40.0
		_FadeDuration("Fade duration", float) = 50.0
	}
	SubShader
	{
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
				fixed4 col;
				float dist = distance(i.worldPosition, _PlayerPos);
				
				//Completely transparent
				if (dist > _FadeInValue + _FadeDuration)
					col = fixed4(1, 0.84, 1, 0);
				
				//Fade in
				else if (dist > _FadeInValue)
				{
					float alphaV = abs((dist - _FadeInValue) / _FadeDuration);
					col = fixed4(1, 0.84, 0, 1 - alphaV);
				}

				//Completely opaque
				else				
					col = fixed4(1, 0.84, 0, 1);

				return col;				
			}
			ENDCG
		}
	}
}
