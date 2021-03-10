﻿namespace BlazingPizza.ComponentsLibrary.Map
{
    public record Marker
    {
        public string Description { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public bool ShowPopup { get; set; }
    }
}