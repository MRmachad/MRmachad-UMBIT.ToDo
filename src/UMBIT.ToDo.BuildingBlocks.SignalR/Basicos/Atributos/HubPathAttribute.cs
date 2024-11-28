using System;

namespace TSE.Nexus.SDK.SignalR.Basicos.Atributos
{
    public class HubPathAttribute : Attribute
    {
        public string Path { get; set; }
        public HubPathAttribute(string path = "/hub")
        {
            this.Path = path;
        }
    }
}
