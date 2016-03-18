Shader "Custom/New" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		cull off
		
		CGPROGRAM

		#pragma surface surf jp

		#pragma target 3.0




		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;

			o.Alpha = c.a;
		}
		
		
		float4 Lightingjp (SurfaceOutput s, float3 lightDir, float atten ) {

			float4 tt;
			float NdotL = dot(s.Normal , lightDir);
		

			//if
		if (NdotL>0.65) {
		
		NdotL = 0.9;
		}
		else if (NdotL>0.08) {
		
		NdotL = 0.4;
		}
		
		else 
		{
		NdotL = 0.05;
		}
		return NdotL;
		
		//최종출력
		
		tt.rgb = NdotL * _LightColor0.rgb * s.Albedo;
		tt.a = s.Alpha;
		
		
		return tt;
		
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
