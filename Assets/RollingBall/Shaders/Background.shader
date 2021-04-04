Shader "Custom/BackgroundShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    CGINCLUDE
    #include "UnityCG.cginc"

    float rand(float2 st)
    {
        return frac(sin(dot(st, float2(12.9898, 78.233))) * 43758.5453);
    }

    float circle(float2 st, float r)
    {
        float d = distance(float2(0.5, 0.5), st);
        return step(r, d);
    }

    // ノコギリ波
    float saw(float t)
    {
        return frac(t - floor(t) - 1);
    }

    // ノコギリ波もどき
    float wave(float t)
    {
        float PI = radians(180);
        float sx = abs(sin(t / 2 * PI)) * step(0, sin((t + 0.0) * PI));
        float cx = abs(cos(t / 2 * PI)) * step(0, cos((t + 0.5) * PI));
        return sx + cx;
    }

    float4 frag(v2f_img i) : SV_Target
    {
        float n = 6; // 縦横の繰り返し数
        float2 st = frac(i.uv * n);

        float t = _Time.y * 0.5 + rand(floor(i.uv * n));
        float outer = circle(st, 0.4 * wave(t));
        float inner = circle(st, 0.4 * saw(t));

        return lerp(
            float4(1.0, 0.7, 0.0, 0.0),
            float4(1.0, 0.8, 0.4, 1.0),
            inner - outer
        );
    }

    ENDCG

    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            ENDCG
        }
    }
}