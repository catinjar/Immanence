Shader "Hidden/BleedingColors"
{
	Properties
	{
		_Intensity ("Black & White blend", Range (0, 15)) = 3
		_MainTex ("Texture", 2D) = "white" {}
		_ValueX ("Degree of bleeding colors", Range (-10, 10)) = 0.2
	}

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			half _ValueX;
			half _Intensity;

			struct v2f {
			   float4 pos : POSITION;
			   half2 uv : TEXCOORD0;
			};

			v2f vert (appdata_img v){
			   v2f o;
			   o.pos = UnityObjectToClipPos(v.vertex);
			   o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
			   return o; 
			}
			
			uniform sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_Target
			{
				float4 m = tex2D(_MainTex, i.uv);
				i.uv.x -= _ValueX * 0.01f;
				float4 l = tex2D(_MainTex, i.uv);
				i.uv.x += _ValueX * 0.01f;
				float4 r = tex2D(_MainTex, i.uv);

				float my = 0.299 * m.r + 0.587 * m.g + 0.114 * m.b;
				float lu = -0.147 * l.r - 0.289 * l.g + 0.436 * l.b;
				float rv = 0.615 * r.r - 0.515 * r.g - 0.100 * r.b;
				
				float3 mix = float3(my + 1.140 * rv, my - 0.395 * lu - 0.581 * rv, my + 2.032 * lu); 

				float4 result = m;
				result.rgb = lerp(m, mix.rgb, _Intensity);

				return result;
			}
			ENDCG
		}
	}
}
