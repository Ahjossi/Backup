Shader "Custom/qksxnaud" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200

		blend SrcAlpha OneMinusSrcAlpha
		zwrite on
		ColorMask 0

		CGPROGRAM
		
		#pragma surface surf jp keepalpha
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float4 Color:color;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Alpha = 0.5;
		}



		float4 Lightingjp (SurfaceOutput s , float3 lightDir, float atten) {


			return float4 (0, 0, 0, s.Alpha);

		}

		ENDCG

			//2pass


			ColorMask rgb

		CGPROGRAM


		#pragma surface surf jp keepalpha
		#pragma target 3.0

			sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		void surf(Input IN, inout SurfaceOutput o) {

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = 0.5;
		}



		float4 Lightingjp(SurfaceOutput s, float3 lightDir, float atten) {


			return float4 (0, 0, 0, s.Alpha);

		}

		ENDCG


	} 
	FallBack "Diffuse"
}
