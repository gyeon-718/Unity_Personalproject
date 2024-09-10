Shader "Vefects/SH_Unlit_Flipbook_URP" {
	Properties {
		[HideInInspector] _EmissionColor ("Emission Color", Vector) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff ("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin] [Space(13)] [Header(Main Texture)] [Space(13)] _MainTexture ("Main Texture", 2D) = "white" {}
		_UVS ("UV S", Vector) = (1,1,0,0)
		_UVP ("UV P", Vector) = (0,0,0,0)
		[HDR] _R ("R", Vector) = (1,0.9719134,0.5896226,0)
		[HDR] _G ("G", Vector) = (1,0.7230805,0.25,0)
		[HDR] _B ("B", Vector) = (0.5943396,0.259371,0.09812209,0)
		[HDR] _Outline ("Outline", Vector) = (0.2169811,0.03320287,0.02354041,0)
		[Space(13)] [Header(DisolveMapping)] [Space(13)] _disolveMap ("disolveMap", 2D) = "white" {}
		[Header(TextureProps)] [Space(13)] _Intensity ("Intensity", Range(0, 5)) = 1
		_ErosionSmoothness ("Erosion Smoothness", Range(0.1, 15)) = 0.1
		_FlatColor ("Flat Color", Range(0, 1)) = 0
		_UVDS1 ("UV D S", Vector) = (1,1,0,0)
		[Space(13)] [Header(Distortion)] [Space(13)] _DistortionTexture ("Distortion Texture", 2D) = "white" {}
		_UVDP1 ("UV D P", Vector) = (0.1,-0.2,0,0)
		_DistortionLerp ("Distortion Lerp", Range(0, 0.1)) = 0
		[Header(SecondDistortion)] [Space(13)] _DistortionSecond ("DistortionSecond", 2D) = "white" {}
		_SecondDistortionLerp ("SecondDistortionLerp", Range(0.5, 1)) = 0.5
		_UVDS ("UV D S", Vector) = (1,1,0,0)
		_UVDP ("UV D P", Vector) = (0.1,-0.2,0,0)
		[Space(13)] [Header(AR)] [Space(13)] _Cull ("Cull", Float) = 2
		_ZWrite ("ZWrite", Float) = 0
		_ZTest ("ZTest", Float) = 2
		_Src ("Src", Float) = 5
		[ASEEnd] _Dst ("Dst", Float) = 10
		[HideInInspector] _texcoord ("", 2D) = "white" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Hidden/InternalErrorShader"
	//CustomEditor "UnityEditor.ShaderGraph.PBRMasterGUI"
}