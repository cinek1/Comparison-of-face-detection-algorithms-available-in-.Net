﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ComparingApp.Model
{
    public class Rectangle
    {
        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
    }
}
