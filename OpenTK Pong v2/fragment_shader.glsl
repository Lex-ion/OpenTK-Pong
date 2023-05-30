#version 330
 
in vec3 color_in;

 out vec4 color_out;

 void main() {
    color_out = vec4(color_in.r,color_in.g,color_in.b,1.0);
 }