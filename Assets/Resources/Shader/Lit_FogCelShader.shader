Shader "Lit/FogCelShader" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_AmbientColor ("Ambient Color", Vector) = (1,1,1,1)
		_Color ("Color offset", Vector) = (1,1,1,1)
		_ShadowColor ("Shadow Color", Vector) = (0.35,0.4,0.45,1)
		_ShadowStrength ("Shadow Strength", Float) = 0.5
		_TintStrength ("Tint Strength", Float) = 1
		_FogMaxY ("Fog Max Y", Float) = 0
		_FogMinY ("Fog Min Y", Float) = 0
		_FogColor ("Fog Color", Vector) = (0,0,0,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}