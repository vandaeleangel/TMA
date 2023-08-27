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
                Name = "Yellow"
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
                Name = "Purple"
            };

            return Task
                .FromResult(colors);
        }
    }
}
