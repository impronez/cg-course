﻿using Cannabis;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

var nativeWindowSettings = new NativeWindowSettings()
{
    ClientSize = new Vector2i(1200, 900),
    Title = "Cannabis",
            
    Flags = ContextFlags.Default,
    APIVersion = new Version(3, 3),
    Profile = ContextProfile.Compatability
};
        
using var window = new ViewWindow(GameWindowSettings.Default, nativeWindowSettings);
        
window.Run();