using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L20250217
{
    public class TextRenderer : Renderer
    {
        public string contents;
        public IntPtr surface;
        public IntPtr texture;
        public SDL.SDL_Color color;

        SDL.SDL_Rect Destination;

        public void SetText(string inContent)
        {
            contents = inContent;
            surface = SDL_ttf.TTF_RenderUNICODE_Solid(Engine.Instance.Font, contents, color);
            texture = SDL.SDL_CreateTextureFromSurface(Engine.Instance.myRenderer, surface);
            
            int w = 0;
            int h = 0;
            uint format = 0;
            int access = 0;
            SDL.SDL_QueryTexture(texture, out format, out access, out w, out h);

            Destination.x = transform.X;
            Destination.y = transform.Y;
            Destination.w = w;
            Destination.h = h;          
        }

        public override void Render()
        {
            SDL.SDL_RenderCopy(Engine.Instance.myRenderer, texture, 0, ref Destination); //0은 전체 사이즈 다 쓰겠다는 의미임         
            
        }

        public override void Update()
        {
            
        }
    }
}
