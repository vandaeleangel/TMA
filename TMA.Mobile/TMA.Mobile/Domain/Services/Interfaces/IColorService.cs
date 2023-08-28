using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMA.Mobile.Domain.Models;

namespace TMA.Mobile.Domain.Services.Interfaces
{
    public interface IColorService
    {
        Task<List<CustomColor>> GetColors();
        Task<SKColor> ConvertColorStringValue(string colorValue);
    }
}
