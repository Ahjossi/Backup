Shader "Custom/03" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2 ("Tex2", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
			
		cull off
		
		
		CGPROGRAM
		#pragma surface surf mk noambient

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex2;
		
		
		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);


			
			o.Albedo = c.rgb*1.2;
			o.Alpha = c.a;
			}
			
		float4 Lightingmk ( SurfaceOutput s , float3 lightDir, float3 viewDir , float atten ){
		
		float4 tt;
		float NdotL = dot(s.Normal, lightDir)*0.5+0.5;

		//스펙큘러
		
		float3 h = normalize(normalize(lightDir) + normalize(viewDir));
		float sp = dot(s.Normal, h);
		sp = pow(sp,100);

		//tex2 , NdotL뒤에 위치해야 한다.
		
		float4 ramp = tex2D(_MainTex2, float2(NdotL,0.5));
		
		
		
		//최종 출력
		
		tt.rgb = ramp.rgb*s.Albedo*atten*_LightColor0*NdotL;
		tt.a = ramp.a;
		



		return tt+sp;
	
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
