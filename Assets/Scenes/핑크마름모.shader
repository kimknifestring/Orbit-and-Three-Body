Shader "Custom/핑크마름모"
{
    Properties
    {
        _MainTex ("New Scene Texture", 2D) = "white" {} // 새 장면 텍스처
        _Progress ("Transition Progress", Range(0, 1)) = 0 // 전환 진행도 (0~1)
        _DiamondSize ("Diamond Tile Size", Float) = 0.05 // 마름모 타일 크기
        _Variation ("Tile Variation", Float) = 0.1 // 타일별 전환 시간 변화
        _Duration ("Transition Duration", Float) = 0.2 // 전환 지속 시간
        _Alpha ("Overlay Alpha", Range(0, 1)) = 0.5 // 오버레이 투명도
        _BaseColor ("Base Color (Pastel)", Color) = (0.9, 0.6, 0.7, 1) // 파스텔톤 분홍 계열 기본 색상
        _ColorVariation ("Color Variation", Range(0, 1.0)) = 0.5 // 타일별 색상 변화 폭
        _HueVariation ("Hue Variation", Range(0, 0.2)) = 0.05 // 색조 변화 폭 (매우 작게 설정)
        _SatVariation ("Saturation Variation", Range(0, 0.5)) = 0.3 // 채도 변화 폭 (차이 강화)
        _ValVariation ("Value Variation", Range(0, 0.3)) = 0.2 // 밝기 변화 폭 (차이 강화)
        _SparkleIntensity ("Sparkle Intensity", Range(0, 1)) = 0.5 // 무지개빛 반짝 강도
        _SparkleSpeed ("Sparkle Speed", Float) = 1.5 // 반짝임 속도
        _Direction ("Transition Direction", Range(-1, 1)) = 1 // 진행 방향 (1: 왼쪽->오른쪽, -1: 오른쪽->왼쪽)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha // 알파 블렌딩 활성화
        ZWrite Off // Z 버퍼 쓰기 비활성화

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Progress;
            float _DiamondSize;
            float _Variation;
            float _Duration;
            float _Alpha;
            float4 _BaseColor;
            float _ColorVariation;
            float _HueVariation;
            float _SatVariation;
            float _ValVariation;
            float _SparkleIntensity;
            float _SparkleSpeed;
            float _Direction; // 새로운 방향 프로퍼티 추가

            // RGB를 HSV로 변환
            float3 RGBtoHSV(float3 c)
            {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 p = c.g < c.b ? float4(c.bg, K.wz) : float4(c.gb, K.xy);
                float4 q = c.r < p.x ? float4(p.xyw, c.r) : float4(c.r, p.yzx);
                float d = q.x - min(q.w, q.y);
                float e = 1.0e-10;
                return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }

            // HSV를 RGB로 변환
            float3 HSVtoRGB(float3 c)
            {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 화면 좌표를 마름모 그리드로 변형 (UV 좌표 0~1 기준)
                float s = _DiamondSize;
                float u = (i.uv.x - i.uv.y) / sqrt(2.0); // 45도 회전된 좌표계
                float v = (i.uv.x + i.uv.y) / sqrt(2.0);

                // 그리드 인덱스 계산
                float i_grid = floor(u / s);
                float j_grid = floor(v / s);

                // 의사 난수 오프셋으로 불규칙성 추가
                float offset = frac(sin(dot(float2(i_grid, j_grid), float2(12.9898, 78.233))) * 43758.5453);

                // 타일별 색상 변화 계산 (난수 기반)
                float hueOffset = (sin(dot(float2(i_grid + j_grid, i_grid - j_grid), float2(12.9898, 78.233))) * 0.5 + 0.5) * _HueVariation;
                float satOffset = (frac(sin(dot(float2(i_grid, j_grid + 1.0), float2(12.9898, 78.233))) * 43758.5453) * 2.0 - 1.0) * _SatVariation;
                float valOffset = (frac(sin(dot(float2(i_grid + 1.0, j_grid + 1.0), float2(12.9898, 78.233))) * 43758.5453) * 2.0 - 1.0) * _ValVariation;

                // 타일 중심의 x좌표 계산
                float u_center = (i_grid + 0.5) * s;
                float v_center = (j_grid + 0.5) * s;
                float x_center = (u_center + v_center) / sqrt(2.0);

                // 진행 방향에 따라 x_center 조정
                float adjusted_x_center = _Direction > 0 ? x_center : 1.0 - x_center; // 방향에 따라 좌우 반전

                // 전환 시작 시간 계산 (조정된 x_center 사용)
                float fade_time = adjusted_x_center + offset * _Variation;

                // 투명도 계산 (1.0에서 0.0으로 부드럽게 감소)
                float opacity = 1.0 - smoothstep(fade_time - _Duration / 2.0, fade_time + _Duration / 2.0, _Progress);

                // 마름모 내부인지 확인
                float du = abs(u - (i_grid + 0.5) * s);
                float dv = abs(v - (j_grid + 0.5) * s);
                float diamond_border = (du + dv) / (s * sqrt(2.0));

                if (diamond_border > 1.0) {
                    return fixed4(0, 0, 0, 0);
                }

                // 기본 색상을 HSV로 변환
                float3 baseHSV = RGBtoHSV(_BaseColor.rgb);
                float finalHue = frac(baseHSV.x + hueOffset + (i_grid + j_grid) * 0.01);
                float finalSat = clamp(baseHSV.y + satOffset, 0.3, 0.8);
                float finalVal = clamp(baseHSV.z + valOffset, 0.7, 1.0);

                // HSV를 다시 RGB로 변환
                fixed3 base_rgb = HSVtoRGB(float3(finalHue, finalSat, finalVal));
                fixed4 base_overlay = fixed4(base_rgb, _BaseColor.a) * _Alpha * opacity;

                // 무지개빛 반짝임 효과 추가
                float time = _Time.y * _SparkleSpeed;
                float sparkle = sin(dot(i.uv, float2(12.9898, 78.233)) * 10.0 + time) * 0.5 + 0.5;
                sparkle *= _SparkleIntensity;
                fixed3 rainbow = fixed3(
                    sin(time + i.uv.x * 3.0 + finalHue * 2.0),
                    sin(time + i.uv.y * 3.0 + 2.0 + finalHue * 2.0),
                    sin(time + i.uv.x * 3.0 + 4.0 + finalHue * 2.0)
                ) * 0.5 + 0.5;

                // 기본 색상에 무지개빛 반짝임 추가
                fixed3 final_color = base_overlay.rgb + rainbow * sparkle;
                fixed alpha = base_overlay.a;

                return fixed4(final_color, alpha);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}