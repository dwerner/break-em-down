Shader "Petes/Solid Transparent Color" {

	Properties {
	    _Color ("Solid Color (A = Opacity)", Color) = (0,0,0,1) 
	}

	Category {

	    Tags {Queue = Transparent}
	    ZWrite Off
	    Blend SrcAlpha OneMinusSrcAlpha

	    SubShader {
	        Pass {
	            GLSLPROGRAM
	            #ifdef VERTEX       
	            void main() {
	                gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
	            }
	            #endif

	            #ifdef FRAGMENT
	            uniform vec4 _Color;

	            

	            void main() {

	                gl_FragColor = _Color;

	            }

	            #endif
	            ENDGLSL
	        }

	    }

	    SubShader {
	        Color[_Color]
	        Pass {}
	    }
	}

}