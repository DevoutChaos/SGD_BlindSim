Shader "Custom/DistortNoiseBlur"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			float random(float p) { return frac(sin(p)*10000.); }
			float noise(float2 p) { return random(p.x + p.y*10000.); }
			float2 sw(float2 p) { return float2(floor(p.x), floor(p.y)); }
			float2 se(float2 p) { return float2(ceil(p.x), floor(p.y)); }
			float2 nw(float2 p) { return float2(floor(p.x), ceil(p.y)); }
			float2 ne(float2 p) { return float2(ceil(p.x), ceil(p.y)); }

			float smoothNoise(float2 p)
			{
				float2 inter = smoothstep(0., 1., frac(p));
				float s = lerp(noise(sw(p)), noise(se(p)), inter.x);
				float n = lerp(noise(nw(p)), noise(ne(p)), inter.x);
				return lerp(s, n, inter.y);
				return noise(nw(p));
			}

			float movingNoise(float2 p)
			{
				float total = 0.0;
				total += smoothNoise(p - _Time.x);
				total += smoothNoise(p*2. + _Time.x) / 2.;
				total += smoothNoise(p*4. - _Time.x) / 4.;
				total += smoothNoise(p*8. + _Time.x) / 8.;
				total += smoothNoise(p*16. - _Time.x) / 16.;
				total /= 1. + 1. / 2. + 1. / 4. + 1. / 8. + 1. / 16.;
				return total;
			}

			float nestedNoise(float2 p)
			{
				float x = movingNoise(p);
				float y = movingNoise(p + 100.);
				return movingNoise(p + float2(x, y));
			}

			sampler2D _MainTex;
			fixed4 frag (v2f i) : SV_Target
			{
				float2 p = i.uv * 6.;
				float offset = nestedNoise(p);
				fixed3 cOrig = tex2D(_MainTex, i.uv).rgb;
				fixed3 cMod = tex2D(_MainTex, i.uv + float2(offset / 3., offset / 3.)).rgb;
				fixed3 cFinal = cOrig * cMod * 5.;
				return fixed4(cFinal, 1);
			}
			ENDCG
		}
	}
}
