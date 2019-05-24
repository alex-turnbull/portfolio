// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Echolocation" {
	Properties{
		_ColorBase("ColorBase", Color) = (1, 1, 1, 1)
		_Color1("Color1", Color) = (1, 1, 1, 1)
		_Color2("Color2", Color) = (1, 1, 1, 1)
		_Color3("Color3", Color) = (1, 1, 1, 1)
		_Color4("Color4", Color) = (1, 1, 1, 1)
		_Color5("Color5", Color) = (1, 1, 1, 1)

		_Center1("Center1", vector) = (0, 0, 0)
		_Center2("Center2", vector) = (0, 0, 0)
		_Center3("Center3", vector) = (0, 0, 0)
		_Center4("Center4", vector) = (0, 0, 0)
		_Center5("Center5", vector) = (0, 0, 0)
		_Radius1("Radius1", float) = 0
		_Radius2("Radius2", float) = 0
		_Radius3("Radius3", float) = 0
		_Radius4("Radius4", float) = 0
		_Radius5("Radius5", float) = 0

		_MainTex("Texture", 2D) = "white" {}

		//_WireThickness("Wire Thickness", RANGE(0, 800)) = 100
		//_WireSmoothness("Wire Smoothness", RANGE(0, 20)) = 3
		//_WireColor("Wire Color", Color) = (0.0, 1.0, 0.0, 1.0)
		//_BaseColor("Base Color", Color) = (0.0, 0.0, 0.0, 0.0)
		//_MaxTriSize("Max Tri Size", RANGE(0, 200)) = 25
	}
		SubShader{
			Pass {
				Tags 
				{ 
					"IgnoreProjector" = "True"
					"Queue" = "Transparent"
					"RenderType" = "Transparent" 
				}

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				float4 _ColorBase;
				float4 _Color1;
				float4 _Color2;
				float4 _Color3;
				float4 _Color4;
				float4 _Color5;
				float3 _Center1;
				float3 _Center2;
				float3 _Center3;
				float3 _Center4;
				float3 _Center5;
				float _Radius1;
				float _Radius2;
				float _Radius3;
				float _Radius4;
				float _Radius5;

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f {
					float4 pos : SV_POSITION;
					float3 worldPos : TEXCOORD1;
					float2 uv : TEXCOORD0;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;

				v2f vert(appdata v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					return o;
				}

				fixed4 frag(v2f i) : COLOR {
					float dist1 = distance(_Center1, i.worldPos);
					float dist2 = distance(_Center2, i.worldPos);
					float dist3 = distance(_Center3, i.worldPos);
					float dist4 = distance(_Center4, i.worldPos);
					float dist5 = distance(_Center5, i.worldPos);

					fixed4 col = tex2D(_MainTex, i.uv);

					//float val1 = 1 - step(dist1, _Radius1 - 0.1) * 0.5 / _Color.a;
					float val1 = step(_Radius1 - 10, dist1) * step(dist1, _Radius1) * _Color1.a;

					//float val2 = 1 - step(dist2, _Radius2 - 0.1) * 0.5 / _Color.a;					
					float val2 = step(_Radius2 - 10, dist2) * step(dist2, _Radius2) * _Color2.a;

					//float val3 = 1 - step(dist3, _Radius3 - 0.1) * 0.5 / _Color.a;
					float val3 = step(_Radius3 - 10, dist3) * step(dist3, _Radius3) * _Color3.a;

					//float val4 = 1 - step(dist4, _Radius4 - 0.1) * 0.5 / _Color.a;
					float val4 = step(_Radius4 - 10, dist4) * step(dist4, _Radius4) * _Color4.a;

					//float val5 = 1 - step(dist5, _Radius5 - 0.1) * 0.5 / _Color.a;
					float val5 = step(_Radius5 - 10, dist5) * step(dist5, _Radius5) * _Color5.a; // * val5
					
					float val = (val1 + val2 + val3 + val4 + val5) * col;

					return fixed4(val * _ColorBase.r, val * _ColorBase.g,val * _ColorBase.b, 1.0);
				}

				ENDCG
			}
	}
		FallBack "Diffuse"
}