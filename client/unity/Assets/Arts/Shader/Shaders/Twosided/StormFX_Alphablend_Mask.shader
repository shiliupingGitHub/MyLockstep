// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1799578,fgcg:0.2352941,fgcb:0.08131487,fgca:1,fgde:0.1,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33401,y:32256,varname:node_4795,prsc:2|emission-3949-OUT,alpha-6036-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:28886,y:32185,ptovrint:False,ptlb:Emissvie,ptin:_Emissvie,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False|UVIN-865-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:29692,y:31782,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:29890,y:32120,ptovrint:True,ptlb:Emissive Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Power,id:3949,x:32748,y:32112,varname:node_3949,prsc:2|VAL-955-OUT,EXP-9553-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9553,x:32440,y:32198,ptovrint:False,ptlb:Emissive Contrast,ptin:_EmissiveContrast,varname:_Fresnel_amount_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:998,x:29503,y:32068,varname:node_998,prsc:2|A-6074-RGB,B-6436-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6436,x:29154,y:31980,ptovrint:False,ptlb:Emissive Brightness,ptin:_EmissiveBrightness,varname:_Fresenl_Power_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_TexCoord,id:6122,x:28315,y:31921,varname:node_6122,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:3192,x:27856,y:32146,ptovrint:False,ptlb:Emissvie Panner X,ptin:_EmissviePannerX,varname:_Emissvie_Add_01UVEffectPannerX_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:9817,x:28332,y:32263,varname:node_9817,prsc:2|A-6889-OUT,B-5956-TSL;n:type:ShaderForge.SFN_Time,id:5956,x:28078,y:32374,varname:node_5956,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:9362,x:27853,y:32314,ptovrint:False,ptlb:Emissvie Panner Y,ptin:_EmissviePannerY,varname:_Emissvie_Add_01UVEffectPannerY_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:6889,x:28094,y:32196,varname:node_6889,prsc:2|A-3192-OUT,B-9362-OUT;n:type:ShaderForge.SFN_Add,id:865,x:28636,y:32140,varname:node_865,prsc:2|A-6122-UVOUT,B-9817-OUT;n:type:ShaderForge.SFN_Tex2d,id:6522,x:30665,y:32755,ptovrint:False,ptlb:Emissive Mask,ptin:_EmissiveMask,varname:node_6522,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8313-OUT;n:type:ShaderForge.SFN_Multiply,id:955,x:31541,y:32212,varname:node_955,prsc:2|A-4063-OUT,B-6522-RGB;n:type:ShaderForge.SFN_Multiply,id:4063,x:30241,y:31870,varname:node_4063,prsc:2|A-2053-RGB,B-998-OUT,C-797-RGB;n:type:ShaderForge.SFN_TexCoord,id:8736,x:29488,y:32954,varname:node_8736,prsc:2,uv:1,uaff:False;n:type:ShaderForge.SFN_SwitchProperty,id:8313,x:30266,y:32845,ptovrint:False,ptlb:Emissive Mask UV2,ptin:_EmissiveMaskUV2,varname:node_8313,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-1436-UVOUT,B-8736-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1436,x:29411,y:32703,varname:node_1436,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:4277,x:32768,y:32676,varname:node_4277,prsc:2|A-6074-A,B-6522-A;n:type:ShaderForge.SFN_Multiply,id:6036,x:32869,y:32425,varname:node_6036,prsc:2|A-797-A,B-4277-OUT,C-2053-A;proporder:6074-797-3192-9362-6436-9553-6522-8313;pass:END;sub:END;*/

Shader "StormFX Studio/Twosided/StormFX_Alphablend_Mask" {
    Properties {
        _Emissvie ("Emissvie", 2D) = "black" {}
        _TintColor ("Emissive Color", Color) = (0.5,0.5,0.5,1)
        _EmissviePannerX ("Emissvie Panner X", Float ) = 0
        _EmissviePannerY ("Emissvie Panner Y", Float ) = 0
        _EmissiveBrightness ("Emissive Brightness", Float ) = 1
        _EmissiveContrast ("Emissive Contrast", Float ) = 1
        _EmissiveMask ("Emissive Mask", 2D) = "white" {}
        [MaterialToggle] _EmissiveMaskUV2 ("Emissive Mask UV2", Float ) = 0
		[HideInInspector]_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal n3ds wiiu 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Emissvie; uniform float4 _Emissvie_ST;
            uniform float4 _TintColor;
            uniform float _EmissiveContrast;
            uniform float _EmissiveBrightness;
            uniform float _EmissviePannerX;
            uniform float _EmissviePannerY;
            uniform sampler2D _EmissiveMask; uniform float4 _EmissiveMask_ST;
            uniform fixed _EmissiveMaskUV2;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_5956 = _Time + _TimeEditor;
                float2 node_865 = (i.uv0+(float2(_EmissviePannerX,_EmissviePannerY)*node_5956.r));
                float4 _Emissvie_var = tex2D(_Emissvie,TRANSFORM_TEX(node_865, _Emissvie));
                float2 _EmissiveMaskUV2_var = lerp( i.uv0, i.uv1, _EmissiveMaskUV2 );
                float4 _EmissiveMask_var = tex2D(_EmissiveMask,TRANSFORM_TEX(_EmissiveMaskUV2_var, _EmissiveMask));
                float3 emissive = pow(((i.vertexColor.rgb*(_Emissvie_var.rgb*_EmissiveBrightness)*_TintColor.rgb)*_EmissiveMask_var.rgb),_EmissiveContrast);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_TintColor.a*(_Emissvie_var.a*_EmissiveMask_var.a)*i.vertexColor.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    
}
