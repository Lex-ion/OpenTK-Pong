#version 330 core
out vec4 FragColor;  
in vec3 ourColor;
  


void main()
{
    float x = ourColor.x;
    float y = ourColor.y;
    

    if(x<0)
    {
    x*=-1;
    }
    if(y<0)
    {
    y*=-1;
    }



    float z = x/2-y/2;
    FragColor = vec4(x,y,z, 1.0);
}