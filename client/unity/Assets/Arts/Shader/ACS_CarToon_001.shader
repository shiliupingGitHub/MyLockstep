
Shader "ArtCenterShader/CustomToon/ACS_CarToon_001" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _MainColor ("Main Color", Color) = (0.8,0.8,0.8,1)
        _TextureIntensity ("Texture Intensity", Float ) = 1
		_MaskL("Mask L", 2D) = "white" {}
        _SelfLitWeight ("Self Lit Weight", Range(0, 1)) = 0.01
		_SelfLitPower("Self Lit Power", Float) = 1
        _DirectionalLightIntensity ("DirectionalLightIntensity", Float ) = 0
        _AmbientLightIntensity ("Ambient Light Intensity", Range(0, 1)) = 0
        _SelfShadowColor ("Self Shadow Color", Color) = (1,1,1,1)
        _SelfShadowPower ("Self Shadow Power", Range(-1, 1)) = 0.2
		_BrightPower("Bright Power", Float) = 1
        _OverallShadowPower ("Overall Shadow Power", Range(0, 1)) = 0.1
        _SelfShadowThreshold ("SelfShadow Threshold", Range(-1, 1)) = -0.6
        _SelfShadowHardness ("SelfShadow Hardness", Range(0.01, 1)) = 0.01
        _ShadowContribution ("Shadow Contribution", Range(0, 1)) = 1
        _RimColor ("Rim Color", Color) = (0,1,0.95,1)
        _RimIntensity ("Rim Intensity", Range(0, 1)) = 0.05
		[MaterialToggle] _RimIntensity_Multi_off("Rim Intensity_Multi off", Float) = 0
		_RimIntensity_Multi("Rim Intensity_Multi", Range(0, 1)) = 0.05
        _RimOffset ("Rim Offset", Range(0, 1)) = 0.5
        _RimPower ("Rim Power", Range(0.01, 1)) = 0.1
        [MaterialToggle] _StaticHighLights ("Static HighLights", Float ) = 0
        _HightlightColor ("Hightlight Color", Color) = (1,1,1,1)
        _HightlightIntensity ("Hightlight Intensity", Range(0, 1)) = 0.1
        _HighlightCelloffset ("Highlight Cell offset", Range(-1, -0.5)) = -0.9
        _HighlightCellSharpness ("Highlight Cell Sharpness", Range(0.001, 1)) = 0.001
        _XYZPosition ("XYZ Position", Vector) = (0,0,0,0)
        _XYZHardness ("XYZ Hardness", Float ) = 8
        _SmoothObjectNormal ("SmoothObjectNormal", Range(0, 1)) = 0
		[Toggle(Outline_ON)] _Outline("Outline", Float) = 1
        _OutlineWidth ("Outline Width", Range(0, 0.03)) = 0.002
        _OutlineTint ("Outline Tint", Color) = (0,0,0,1)
		[Toggle(HSV_ON)] _HSV("HSVC Set Up", Float) = 0
		_HSVHue("HSVC Hue", Range(0, 1)) = 0
		_HSVSaturation("HSVC Saturation", Range(0, 3)) = 1
		_HSVValue("HSVC Value", Range(0, 3)) = 1
		_Contras("HSVC Contras", Range(0, 3)) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog 
            #pragma target 3.0
			#pragma shader_feature Outline_ON

            uniform sampler2D _Texture;
            uniform half4 _MainColor;
            uniform fixed _TextureIntensity;
            uniform fixed _OutlineWidth;
            uniform half4 _OutlineTint;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };

            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;

                #ifdef Outline_ON
                o.uv0 = v.texcoord0;
                half4 _Texture_var = tex2Dlod(_Texture, half4(o.uv0, 0, 0));
                half _OutlineCustomWidth = _Texture_var.a;
                half _SetOutlineWidth = (_OutlineWidth*_OutlineCustomWidth);
                o.pos = UnityObjectToClipPos(half4(v.vertex.xyz + v.normal*_SetOutlineWidth,1) );
                #else
				o.pos = UnityObjectToClipPos(v.vertex);
                #endif

                UNITY_TRANSFER_FOG(o,o.pos);
				return o;
				
            }
            float4 frag(VertexOutput i) : COLOR {
				#ifdef Outline_ON
                half4 _Texture_var = tex2D(_Texture,i.uv0);
                half3 _DiffuseColor = (_Texture_var.rgb*_MainColor.rgb*_TextureIntensity);
                half3 _SetOutlineColor = (_DiffuseColor*_OutlineTint.rgb);
				return fixed4(_SetOutlineColor, 0);
                #else
				clip(0.0 - 0.5);
				half3 finalColor = 0;
				return fixed4(finalColor, 1);
				#endif
            }
            
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
			#pragma shader_feature HSV_ON

            uniform sampler2D _Texture;
            uniform half4 _MainColor;
            uniform fixed _ShadowContribution;
            uniform fixed _SelfShadowThreshold;
            uniform fixed _SelfShadowHardness;
            uniform float4 _SelfShadowColor;
            uniform fixed _OverallShadowPower;
            uniform fixed _SelfShadowPower;
            uniform fixed _RimOffset;
            uniform fixed _RimPower;
            uniform half4 _RimColor;
            uniform fixed _TextureIntensity;
            uniform half _DirectionalLightIntensity;
            uniform fixed _StaticHighLights;
            uniform fixed _HighlightCelloffset;
            uniform fixed _HighlightCellSharpness;
            uniform half4 _HightlightColor;
            uniform fixed _HightlightIntensity;
            uniform fixed _RimIntensity;
            uniform fixed _AmbientLightIntensity;
            uniform fixed _SelfLitWeight;
            uniform fixed4 _XYZPosition;
            uniform fixed _XYZHardness;
            uniform fixed _SmoothObjectNormal;
			uniform fixed _BrightPower;
			uniform sampler2D _MaskL;
			uniform fixed _SelfLitPower;
			uniform fixed _HSVHue;
			uniform fixed _HSVSaturation;
			uniform fixed _HSVValue;
			uniform fixed _Contras;

			half3 RGBtoHSVtoRGB(half3 inputRGB, half HSVHue, half HSVSaturation, half HSVValue, half Contras) {
            #ifdef HSV_ON
				half4 RGBtoHSV_k = half4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				half4 RGBtoHSV_p = lerp(
					half4(half4(inputRGB, 0.0).zy, RGBtoHSV_k.wz),
					half4(half4(inputRGB, 0.0).yz, RGBtoHSV_k.xy),
					step(half4(inputRGB, 0.0).z, half4(inputRGB, 0.0).y)
				);
				half4 RGBtoHSV_q = lerp(
					half4(RGBtoHSV_p.xyw, half4(inputRGB, 0.0).x),
					half4(half4(inputRGB, 0.0).x, RGBtoHSV_p.yzx),
					step(RGBtoHSV_p.x, half4(inputRGB, 0.0).x)
				);
				half  RGBtoHSV_d = RGBtoHSV_q.x - min(RGBtoHSV_q.w, RGBtoHSV_q.y);
				half  RGBtoHSV_e = 1.0e-10;
				half3 RGBtoHSV = half3(
					abs(RGBtoHSV_q.z + (RGBtoHSV_q.w - RGBtoHSV_q.y) / (6.0 * RGBtoHSV_d + RGBtoHSV_e)),
					RGBtoHSV_d / (RGBtoHSV_q.x + RGBtoHSV_e),
					RGBtoHSV_q.x
					);
				half3 outputRGB = (lerp(
					half3(1, 1, 1),
					saturate(3.0 * abs(1.0 - 2.0 * frac(fmod((HSVHue + RGBtoHSV.r), 1.0) + half3(0.0, -1.0 / 3.0, 1.0 / 3.0))) - 1),
					saturate((RGBtoHSV.g * HSVSaturation))) * saturate((RGBtoHSV.b * HSVValue)
					)
					);

				half3 outputRGBc = lerp(half3(0.5,0.5,0.5), outputRGB, Contras);

				return outputRGBc;
            #else
				half3 outputRGB;
				return outputRGB;
            #endif
			}

            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
				half4 objPos = mul ( unity_ObjectToWorld, half4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				half3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
			float4 frag(VertexOutput i) : COLOR {
				half4 objPos = mul ( unity_ObjectToWorld, half4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
				half3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				half3 normalDirection = i.normalDir;
				half3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				half3 lightColor = _LightColor0.rgb;
				half3 halfDirection = normalize(viewDirection+lightDirection);

                UNITY_LIGHT_ATTENUATION(attenuation,i, i.posWorld.xyz);

                half3 _AmbientLight = (UNITY_LIGHTMODEL_AMBIENT.rgb*_AmbientLightIntensity);
				half3 emissive = _AmbientLight;
                half4 _Texture_var = tex2D(_Texture,i.uv0);

                half3 _DiffuseColor = (_Texture_var.rgb*_MainColor.rgb*_TextureIntensity);
				
                half3 _SphereNormal = mul( unity_WorldToObject, half4((i.posWorld.rgb-objPos.rgb),0) ).xyz.rgb;
                half3 _SphereNormaMHardnessRGB = (_SphereNormal*_XYZHardness).rgb;
				half  _SNorMHR_M_XYZPosR = _XYZPosition.r + _SphereNormaMHardnessRGB.r;
				half  _SNorMHR_M_XYZPosG = _XYZPosition.g + _SphereNormaMHardnessRGB.g;
				half  _SNorMHR_M_XYZPosB = _XYZPosition.b + _SphereNormaMHardnessRGB.b;
				half3 _SNorMH_M_XYZPosRGB = half3(_SNorMHR_M_XYZPosR, _SNorMHR_M_XYZPosG, _SNorMHR_M_XYZPosB);
				half3 _SNormalDir = mul(unity_ObjectToWorld, half4(_SNorMH_M_XYZPosRGB, 0)).xyz.rgb;
				half3 _NewNormalDir = lerp(normalDirection, _SNormalDir, _SmoothObjectNormal);

                half _NdotL = max(0,dot(lightDirection,_NewNormalDir));

                half _NdotLToonClamp = saturate(((_NdotL+_SelfShadowThreshold)/_SelfShadowHardness));
                half _ShadowBlend = lerp((1.0 - ((1.0 - attenuation)*_WorldSpaceLightPos0.a)),_NdotLToonClamp,_ShadowContribution);
                half3 _CartoonLight = (
					                   (_LightColor0.rgb * _ShadowBlend * attenuation * _BrightPower)
					                    +
					                   ((1.0 - _NdotLToonClamp)*_OverallShadowPower*_LightColor0.a)
					                    +
					                   (_SelfShadowColor.rgb*_SelfShadowPower*(1.0 - _ShadowBlend)*_LightColor0.a)
					                    +
					                   _DirectionalLightIntensity
					                  );

                half3 _LightColorFalloff = (_LightColor0.rgb*attenuation);

				half  _NdotV = max(0, dot(normalDirection, viewDirection));
				half  _RE_Offset = 1.0 - saturate(_NdotV + _RimOffset);
				half  _RE_Power = pow(_RE_Offset, _RimPower);
				half  _RE_Power_M_NdotL = saturate(_NdotL) * _RE_Power;
				half3 _RimEffect = _RE_Power_M_NdotL * _RimColor.rgb * _LightColorFalloff * _RimIntensity;

				half  _NdotH = max(0, dot(normalDirection, halfDirection));
				half  _NdotH_or_NdotL = lerp(_NdotH, _NdotL, _StaticHighLights);
				half  _HL_Offset = _NdotH_or_NdotL + _HighlightCelloffset;
				half  _HL_Sharpness = saturate(_HL_Offset / _HighlightCellSharpness);
				half3 _Hightlight = _HL_Sharpness * _HightlightColor.rgb * _LightColorFalloff * _HightlightIntensity;
				
				half3 _CartoonColor = _DiffuseColor * _CartoonLight;
				half3 _CartoonfinalColor = _CartoonColor + _RimEffect + _Hightlight;
				half4 _MaskL_var = tex2D(_MaskL, i.uv0);
				half3 _GetfinalColor = emissive + lerp(_CartoonfinalColor, _DiffuseColor * _MaskL_var.r * _SelfLitPower, _SelfLitWeight);

                #ifdef HSV_ON
				half3 finalColor = RGBtoHSVtoRGB(_GetfinalColor, _HSVHue, _HSVSaturation, _HSVValue, _Contras);
                #else
				half3 finalColor = _GetfinalColor;
                #endif

                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog 
            #pragma target 3.0
			#pragma shader_feature HSV_ON

            uniform sampler2D _Texture;
            uniform half4 _MainColor;
            uniform fixed _ShadowContribution;
            uniform fixed _SelfShadowThreshold;
            uniform fixed _SelfShadowHardness;
            uniform float4 _SelfShadowColor;
            uniform fixed _OverallShadowPower;
            uniform fixed _SelfShadowPower;
            uniform fixed _RimOffset;
            uniform fixed _RimPower;
            uniform half4 _RimColor;
            uniform fixed _TextureIntensity;
            uniform half _DirectionalLightIntensity;
            uniform fixed _StaticHighLights;
            uniform fixed _HighlightCelloffset;
            uniform fixed _HighlightCellSharpness;
            uniform half4 _HightlightColor;
            uniform fixed _HightlightIntensity;
			uniform fixed _RimIntensity;
            uniform fixed _RimIntensity_Multi;
			uniform fixed _RimIntensity_Multi_off;
            uniform fixed _AmbientLightIntensity;
            uniform fixed _SelfLitWeight;
            uniform fixed4 _XYZPosition;
            uniform fixed _XYZHardness;
            uniform fixed _SmoothObjectNormal;
			uniform fixed _BrightPower;
			uniform sampler2D _MaskL;
			uniform fixed _SelfLitPower;
			uniform fixed _HSVHue;
			uniform fixed _HSVSaturation;
			uniform fixed _HSVValue;
			uniform fixed _Contras;

			half3 RGBtoHSVtoRGB(half3 inputRGB, half HSVHue, half HSVSaturation, half HSVValue, half Contras) {
            #ifdef HSV_ON
				half4 RGBtoHSV_k = half4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
				half4 RGBtoHSV_p = lerp(
					half4(half4(inputRGB, 0.0).zy, RGBtoHSV_k.wz),
					half4(half4(inputRGB, 0.0).yz, RGBtoHSV_k.xy),
					step(half4(inputRGB, 0.0).z, half4(inputRGB, 0.0).y)
				);
				half4 RGBtoHSV_q = lerp(
					half4(RGBtoHSV_p.xyw, half4(inputRGB, 0.0).x),
					half4(half4(inputRGB, 0.0).x, RGBtoHSV_p.yzx),
					step(RGBtoHSV_p.x, half4(inputRGB, 0.0).x)
				);
				half  RGBtoHSV_d = RGBtoHSV_q.x - min(RGBtoHSV_q.w, RGBtoHSV_q.y);
				half  RGBtoHSV_e = 1.0e-10;
				half3 RGBtoHSV = half3(
					abs(RGBtoHSV_q.z + (RGBtoHSV_q.w - RGBtoHSV_q.y) / (6.0 * RGBtoHSV_d + RGBtoHSV_e)),
					RGBtoHSV_d / (RGBtoHSV_q.x + RGBtoHSV_e),
					RGBtoHSV_q.x
					);
				half3 outputRGB = (lerp(
					half3(1, 1, 1),
					saturate(3.0 * abs(1.0 - 2.0 * frac(fmod((HSVHue + RGBtoHSV.r), 1.0) + half3(0.0, -1.0 / 3.0, 1.0 / 3.0))) - 1),
					saturate((RGBtoHSV.g * HSVSaturation))) * saturate((RGBtoHSV.b * HSVValue)
					)
					);

				half3 outputRGBc = lerp(half3(0.5, 0.5, 0.5), outputRGB, Contras);

				return outputRGBc;
                #else
				half3 outputRGB;
				return outputRGB;
                #endif
			}

            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
				half4 objPos = mul ( unity_ObjectToWorld, half4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				half3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
			float4 frag(VertexOutput i) : COLOR {
				half4 objPos = mul ( unity_ObjectToWorld, half4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
				half3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
				half3 normalDirection = i.normalDir;
				half3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
				half3 lightColor = _LightColor0.rgb;
				half3 halfDirection = normalize(viewDirection+lightDirection);

                UNITY_LIGHT_ATTENUATION(attenuation,i, i.posWorld.xyz);

                half4 _Texture_var = tex2D(_Texture,i.uv0);
                half3 _DiffuseColor = (_Texture_var.rgb*_MainColor.rgb*_TextureIntensity);

				half3 _SphereNormal = mul(unity_WorldToObject, half4((i.posWorld.rgb - objPos.rgb), 0)).xyz.rgb;
				half3 _SphereNormaMHardnessRGB = (_SphereNormal*_XYZHardness).rgb;
				half  _SNorMHR_M_XYZPosR = _XYZPosition.r + _SphereNormaMHardnessRGB.r;
				half  _SNorMHR_M_XYZPosG = _XYZPosition.g + _SphereNormaMHardnessRGB.g;
				half  _SNorMHR_M_XYZPosB = _XYZPosition.b + _SphereNormaMHardnessRGB.b;
				half3 _SNorMH_M_XYZPosRGB = half3(_SNorMHR_M_XYZPosR, _SNorMHR_M_XYZPosG, _SNorMHR_M_XYZPosB);
				half3 _SNormalDir = mul(unity_ObjectToWorld, half4(_SNorMH_M_XYZPosRGB, 0)).xyz.rgb;
				half3 _NewNormalDir = lerp(normalDirection, _SNormalDir, _SmoothObjectNormal);

				half _NdotL = max(0,dot(lightDirection,_NewNormalDir));

                half _NdotLToonClamp = saturate(((_NdotL+_SelfShadowThreshold)/_SelfShadowHardness));
                half _ShadowBlend = lerp(attenuation,_NdotLToonClamp,_ShadowContribution);
				half3 _CartoonLight = (
					                   (_LightColor0.rgb * _ShadowBlend * attenuation * _BrightPower)
					                   +
					                   ((1.0 - _NdotLToonClamp)*_OverallShadowPower*_LightColor0.a)
					                   +
					                   (_SelfShadowColor.rgb*_SelfShadowPower*(1.0 - _ShadowBlend)*_LightColor0.a)
					                   +
					                   _DirectionalLightIntensity
					                  );

				half3 _LightColorFalloff = (_LightColor0.rgb*attenuation);
                
				half  _NdotV = max(0, dot(normalDirection, viewDirection));
				half  _RE_Offset = 1.0 - saturate(_NdotV + _RimOffset);
				half  _RE_Power = pow(_RE_Offset, _RimPower);
				half  _RE_Power_M_NdotL = saturate(_NdotL) * _RE_Power;
				half  _finalRimIntensity = lerp(_RimIntensity, _RimIntensity_Multi, _RimIntensity_Multi_off);
				half3 _RimEffect = _RE_Power_M_NdotL * _RimColor.rgb * _LightColorFalloff * _finalRimIntensity;
                
				half3 _CartoonColor = _DiffuseColor * _CartoonLight;
				half3 _CartoonfinalColor = _CartoonColor + _RimEffect;
				half4 _MaskL_var = tex2D(_MaskL, i.uv0);
				half3 _GetfinalColor = lerp(_CartoonfinalColor, _DiffuseColor * _MaskL_var.r * _SelfLitPower, _SelfLitWeight);

                #ifdef HSV_ON
				half3 finalColor = RGBtoHSVtoRGB(_GetfinalColor, _HSVHue, _HSVSaturation, _HSVValue, _Contras);
                #else
				half3 finalColor = _GetfinalColor;
                #endif

                fixed4 finalRGBA = fixed4(finalColor,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Mobile/Diffuse"
}
