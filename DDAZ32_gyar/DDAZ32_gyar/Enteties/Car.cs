using DDAZ32_gyar.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDAZ32_gyar.Enteties
{
    public class Car : Toy
    {
        protected override void DrawImage(Graphics g)
        {
            Image imageFiles = Image.FromFile("Images/car.png");
            g.DrawImage(imageFiles, new Rectangle(0, 0, Width, Height));
        }
    }
}
