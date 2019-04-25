Shader "Unlit/ZTestOff"
{
	Properties
	{
	  _Color("_Color",Color) = (1,1,1,1)
	}
		SubShader
	{
		

		Pass
		{
			Tags { "LightMode " = "Always" }
			Blend One Zero
			ZTest Always
			ZWrite Off
			Offset 0, -1
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
		// make fog work
			fixed4 _Color;
            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };



            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
              
                return _Color;
            }
            ENDCG
        }
    }
}
