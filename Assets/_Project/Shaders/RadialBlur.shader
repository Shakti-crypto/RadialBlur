Shader "FX/RadialBlur"
{
    Properties 
    {
        _MainTex ("Input", RECT) = "white" {}
        _BlurStrength ("Blur Strength", Float) = 0.5
        _BlurWidth ("Blur Width", Range(0,1)) =0 
    }

    SubShader 
    {
        Pass 
        {
            ZTest Always Cull Off ZWrite Off
            Fog { Mode off }
       
            CGPROGRAM
   
            #pragma vertex vert_img 
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
 
            #include "UnityCG.cginc"

            struct appdata
            {
                half2 uv : TEXCOORD0;

            };

            struct v2f
            {
                half2 uv : TEXCOORD0;
            };

 
            uniform sampler2D _MainTex;
            half4 _MainTex_ST;

            uniform half4 _MainTex_TexelSize;
            uniform half _BlurStrength;
            uniform half _BlurWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f_img  i) : SV_Target
            {       
                #if UNITY_UV_STARTS_AT_TOP
                i.uv.y = 1.0 - i.uv.y;
                #endif

                half4 color = tex2D(_MainTex, i.uv);

                // some sample positions
                half samples[10];
                samples[0] = -0.08;
                samples[1] = -0.05;
                samples[2] = -0.03;
                samples[3] = -0.02;
                samples[4] = -0.01;
                samples[5] =  0.01;
                samples[6] =  0.02;
                samples[7] =  0.03;
                samples[8] =  0.05;
                samples[9] =  0.08;
       
                half2 center = half2(0.5, 0.5); // Center of the screen

                //vector to the middle of the screen
                half2 dir = center - i.uv;
       
                //distance to center
                half dist = sqrt(dir.x*dir.x + dir.y*dir.y);


                half blurFactor = smoothstep(1-_BlurWidth, 1.0, dist); // Blurs the edges

                //normalize direction
                dir = dir/dist;
       
                //additional samples towards center of screen
                half4 sum = color;
                for(int n = 0; n < 10; n++)
                {
                    sum += tex2D(_MainTex, i.uv + dir * samples[n]*blurFactor);
                }
       
                //eleven samples...
                sum *= 1.0/11.0;
       
                //weighten blur depending on distance to screen center
                half t = dist * _BlurStrength;
                t = clamp(t, 0.0, 1.0);

                //blend original with blur
                return lerp(color, sum, t);
            }
            ENDCG
        }
    }
}