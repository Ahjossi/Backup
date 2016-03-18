Shader "Custom/cutoff" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Cutoff ("Cutout", Range(0,1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="TransparentCutout" "Queue"="AlphaTest" }
		LOD 200
		

		cull off


		CGPROGRAM
		#pragma surface surf Lambert alphatest:_Cutoff


		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Emission = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
