Shader "Custom/mask_u"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex("Mask Texture", 2D) = "white" {}
    }
    SubShader
    {
        // 所有Pass中渲染队列使用透明度混合队列,渲染类型选择透明度混合类型,并且渲染过程中不受投影器的影响
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        LOD 100

        Pass
        {
            // 关闭深度写入,避免深度变化造成半透明异常
            ZWrite Off

            // 开启透明度混合,并设置片元颜色的乘积因子以及颜色缓冲区中颜色的乘积因子
            Blend SrcAlpha OneMinusSrcAlpha

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
            float4 _MainTex_ST;
            sampler2D _MaskTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				//fixed2 uvs = fixed2(i.uv.x + _Time.x, i.uv.y);
                fixed4 mask_col = tex2D(_MaskTex, i.uv);
                // 乘以遮罩图的透明度
                col.a = col.a * mask_col.a;
                return col;
            }
            ENDCG
        }
    }
}