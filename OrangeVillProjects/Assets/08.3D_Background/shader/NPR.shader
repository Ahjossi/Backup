Shader "Custom/NPR" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_tt ("외곽선 두께", Range(0,0.1)) = 0.005

	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		cull front
		
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert


		float _tt;

		sampler2D _MainTex;

		void vert (inout appdata_full v){
		
		v.vertex.xyz = v.vertex.xyz + v.normal.xyz *_tt;
			
		
		}


		struct Input {
			float4 color:COLOR;
		};



		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			
			o.Alpha = 1;
		}
		
		
		ENDCG
		
		
		cull back

		CGPROGRAM

		#pragma surface surf jp



	

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 color:COLOR;
		};



		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = 1;
			
		}
		
	float4 Lightingjp ( SurfaceOutput s , float3 lightDir, float atten ){
		
		float NdotL = dot(s.Normal, lightDir);
		
		if (NdotL > 0){
			NdotL = 1;
		
		}
		
		else if (NdotL > 0.5)
		{
		NdotL = 0.5;
		
		}
		
		else
		{
		NdotL = 0.2;
		}
		
//
//		NdotL = NdotL * 8;
//		NdotL = ceil(NdotL)/3;
//		

		
		return NdotL;
		
		}
		
		ENDCG
		


	} 
	FallBack "Diffuse"
}
