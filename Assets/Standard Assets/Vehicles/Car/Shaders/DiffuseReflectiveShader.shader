Shader "Reflective/Diffuse Transperant" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
<<<<<<< HEAD
<<<<<<< HEAD
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" {  }
=======
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
>>>>>>> cb39df0bd0769cb9ae0e2ed7fba6b36d8a078257
=======
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
=======
	_Cube ("Reflection Cubemap", Cube) = "_Skybox" {  }
>>>>>>> cbf288d (多分これが基なるやつ。)
>>>>>>> a02a984290f956c0a87879bddb368a3159a50cf1
}
SubShader {
	LOD 300
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

CGPROGRAM
#pragma surface surf Lambert alpha

sampler2D _MainTex;
samplerCUBE _Cube;

fixed4 _Color;
fixed4 _ReflectColor;

struct Input {
	float2 uv_MainTex;
	float3 worldRefl;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
	fixed4 c = tex * _Color;
	o.Albedo = c.rgb;
	
	fixed4 reflcol = texCUBE (_Cube, IN.worldRefl);
	reflcol *= tex.a;
	o.Emission = reflcol.rgb * _ReflectColor.rgb;
	o.Alpha = c.a;
}
ENDCG
}

FallBack "Reflective/VertexLit"
}
