Shader "Custom/02" {
	Properties {
		_tt ("외곽선 두께" , Range(0,0.2)) = 0.08
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2 ("Tex2", 2D) = "white" {}
 		_BumpTex ("노말텍스쳐", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
	
	
	
	
		CGPROGRAM
		#pragma surface surf mk noambient

		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _BumpTex;
		
		
		
		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpTex;	
		};

		void surf (Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);

			o.Normal = UnpackNormal(tex2D (_BumpTex, IN.uv_BumpTex));

			
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			}
			
		float4 Lightingmk ( SurfaceOutput s , float3 lightDir, float3 viewDir , float atten ){
		
		float4 tt;
		float NdotL = dot(s.Normal, lightDir)*0.5+0.5;

		//스펙큘러
		
		float3 h = normalize(normalize(lightDir) + normalize(viewDir));
		float sp = saturate(dot(s.Normal, h));
		sp = pow(sp,100);

		//tex2 , NdotL뒤에 위치해야 한다.
		
		float4 ramp = tex2D(_MainTex2, float2(NdotL,0.5));
		
		//림
		float rim = dot(normalize(viewDir), s.Normal);
		
		
		if (rim<0.3){
		rim = 0;
		}
		else{
		rim = 1;
		
		}
		
		
		
		
		//최종 출력
		
		tt.rgb = ramp.rgb*s.Albedo*atten*_LightColor0*NdotL*rim;
		tt.a = ramp.a;
		



		return tt+sp;
	
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
