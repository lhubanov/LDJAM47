Shader "Unlit/SpaceShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float2x2 Rot( float a )
            {
                float sa;
                float ca;
                sincos( a, sa, ca );
                return float2x2( ca, -sa, sa, ca );
            }

            float Hash21( float2 p )
            {
                float2 outp;
                outp = frac( p * float2(233.34, 851.74) );
                outp += dot( p, p + 23.45 );
                return frac( outp.x * outp.y );
            }

            half Star(half2 uv, float a, float sparkle) {
                half2 av1 = abs(uv);
                half2 av2 = abs( mul( uv, Rot(a) ) );
                half2 av = min(av1, av2);
                
                half3 col = half3(0.0, 0.0, 0.0);
                float d = length(uv);
                float star = av1.x*av1.y;
                star = max(av1.x*av1.y, av2.x*av2.y);
                star = max(0., 1.0 -star*1e3);
                
                float m = min(5., 1e-2/d);
                
                return m+pow(star, 4.)*sparkle;
            }

            half3 StarLayer( half2 uv, float t, float sparkle )
            {
                half2 gv = frac( uv ) - 0.5;
                half2 id = floor( uv );
                half3 col = 0.0;

                for ( int y = -1; y <= 1; y++ )
                {
                    for ( int x = -1; x <= 1; x++ )
                    {
                        half2 offs = half2( x, y );
                        half n = Hash21( id - offs );
                        half3 N = frac( n * half3( 10.0, 100.0, 1000.0 ) );
                        float2 p = ( N.xy - 0.5 ) * 0.7;

                        half brightness = Star( gv - p + offs, n * 6.2831 + t, sparkle );
                        half3 star = brightness * half3( 0.6 + p.x, 0.4, 0.6 + p.y ) * N.z * N.z;

                        star *= 1.0 + sin( ( t + n ) * 20.0 ) * smoothstep( sin( t ) * 0.5 + 0.5, 1.0, frac( 10.0 * n ) );
                        half d = length( gv + offs );
                        col += star * smoothstep( 1.5, 0.8, d );
                    }
                }
                return col;
            }

            fixed4 frag (v2f input) : SV_Target
            {
                // sample the texture
                fixed4 col = fixed4( 0.0, 0.0, 0.0, 1.0 ); //tex2D(_MainTex, i.uv);

                float2 uv = input.uv - 0.5;
                float t = -_Time.y * 0.03;

                float twirl = sin( t * 0.1 );
                float d = dot( uv, uv );
                // twirl *= twirl * twirl * sin( d );

                uv = mul( Rot( -t * 0.2 ), uv );
                uv *= 2.0 + sin( t * 0.05 );

                const half speed = 0.1;
                float bla = sin( t + sin( t + sin( t ) * 0.5 ) ) * 0.5 + 0.5;

                d = dot( uv, uv );
                float a = atan2( uv.x, uv.y );
                uv /= d;

                float burst = sin( _Time.y * 0.05 );
                // uv *= burst + 0.2;

                float stp = 1.0 / 5.0;
                for ( float i = 0.0; i < 1.0; i += stp )
                {
                    float lt = frac( t * speed + i );
                    float scale = lerp( 10.0, 0.25, lt );
                    float fade = smoothstep( 0, 0.4, lt ) * smoothstep( 1.0, 0.95, lt );
                    float2 sv = uv * scale + i * 134.53;

                    col.rgb += StarLayer( sv, 0.0, 0.0 );// * fade;
                }

                if ( 0 )
                {
                    float burstFade = smoothstep( 0, 0.02, abs( burst ) );
                    float size = 0.9 * sin( t ) + 1.0;
                    size = max( size, sqrt( size ) );
                    float fade = size/d;

                    col *= lerp( 1.0, fade, burstFade );
                    col.rgb += fade * 0.2 * float3( 1.0, 0.5, 0.1 ) * bla * burstFade;

                    t *= 1.5;

                    float rays = sin( a * 5.0 + t * 3.0 ) - cos( a * 7.0 - t );
                    rays *= sin( a + t + sin( a * 4.0 ) * 10.0 ) * 0.5 + 0.5;
                    col += rays * bla * 0.1 * burstFade;
                    col += 1.0 - burstFade;
                }

				col.rgb = saturate( col.rgb );

                col.a = lerp( 0.0, 1.0, dot( col.rgb, col.rgb ) );

                return col;
            }
            ENDCG
        }
    }
}
