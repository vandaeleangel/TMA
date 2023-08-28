using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Models;
using TMA.Mobile.Domain.Services.Interfaces;

namespace TMA.Mobile.Domain.Services
{
    public class ColorService : IColorService
    {
        public Task<SKColor> ConvertColorStringValue(string colorValue)
        {
            SKColor color;

            switch (colorValue.ToLower())
            {
                case "blauw":
                    color = new SKColor(0, 0, 255);
                    break;
                case "rood":
                    color = new SKColor(255, 0, 0);
                    break;
                case "groen":
                    color = new SKColor(0, 150, 0);
                    break;
                case "geel":
                    color = new SKColor(200, 200, 0);
                    break;
                case "roze":
                    color = new SKColor(255, 192, 203);
                    break;
                case "oranje":
                    color = new SKColor(255, 165, 0);
                    break;
                case "zwart":
                    color = new SKColor(0, 0, 0);
                    break;
                case "paars":
                    color = new SKColor(128, 0, 128);
                    break;
                default:                   
                    color = new SKColor(0, 0, 0); 
                    break;
            }

            return Task.FromResult(color);
        }

        public Task<List<CustomColor>> GetColors()
        {
            List<CustomColor> colors = new List<CustomColor>();
            
            CustomColor blue = new CustomColor
            {
                Name = "Blauw"
            };

            CustomColor red = new CustomColor
            {
                Name = "Rood"
            };

            CustomColor green = new CustomColor
            {
                Name = "Groen"
            };

            CustomColor yellow = new CustomColor
            {
                Name = "Geel"
            };

            CustomColor pink = new CustomColor
            {
                Name = "Roze"
            };

            CustomColor orange = new CustomColor
            {
                Name = "Oranje"
            };

            CustomColor black = new CustomColor
            {
                Name = "Zwart"
            };

            CustomColor purple = new CustomColor
            {
                Name = "Paars"
            };

            colors.Add(blue);
            colors.Add(red);
            colors.Add(green);
            colors.Add(yellow);
            colors.Add(purple);
            colors.Add(black);
            colors.Add(orange);
            colors.Add(pink);
           
            return Task
                .FromResult(colors);
        }
    }
}
