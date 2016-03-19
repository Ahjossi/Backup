Shader "Custom/NewSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}

	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		LOD 200
		
		blend SrcAlpha OneMinusSrcAlpha

//		zwrite off

		CGPROGRAM

		#pragma surface surf Lambert keepalpha

		sampler2D _MainTex;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
		};




		void surf (Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Emission = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
//	FallBack "Diffuse"
	FallBack "Legacy Shaders/Transparent/Cutout/Diffuse"
}
