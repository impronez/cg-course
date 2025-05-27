using _3dsScene;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

var nativeWindowSettings = new NativeWindowSettings()
{
    ClientSize = new Vector2i(1200, 900),
    Title = "3ds scene",
        
    Flags = ContextFlags.Default,
    APIVersion = new Version(3, 3),
    Profile = ContextProfile.Compatability
};

using var window = new ViewWindow(GameWindowSettings.Default, nativeWindowSettings);
    
window.Run();


//ChangeColor("../../../Resources/Queen.3ds", "../../../Resources/BlackQueen.3ds");
//FlipModel("../../../Resources/BlackQueen.3ds", "../../../Resources/BlackQueen.3ds");
//
//RotateModel("../../../Resources/swing.3ds", "../../../swing.3ds");
// //
// void RotateModel(string inputFilePath, string outputFilePath)
// {
//     AssimpContext context = new AssimpContext();
//
//     // Загрузка модели
//     Assimp.Scene scene = context.ImportFile(inputFilePath, PostProcessSteps.Triangulate | PostProcessSteps.GenerateNormals);
//
//     // Создаем матрицу поворота (90 градусов вокруг оси X)
//     var rotationMatrix = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(90f));
//
//     // Применяем матрицу поворота ко всем вершинам и нормалям
//     foreach (var mesh in scene.Meshes)
//     {
//         for (int i = 0; i < mesh.VertexCount; i++)
//         {
//             // Применяем поворот к каждой вершине
//             Vector3D vertex = mesh.Vertices[i];
//             var trans = Vector3.TransformPosition(new Vector3(vertex.X, vertex.Y, vertex.Z), rotationMatrix);
//             mesh.Vertices[i] = new Vector3D(trans.X, trans.Y, trans.Z);
//
//             // Применяем поворот к нормалям
//             Vector3D normal = mesh.Normals[i];
//             var norm = Vector3.TransformNormal(new Vector3(normal.X, normal.Y, normal.Z), rotationMatrix);
//             mesh.Normals[i] = new Vector3D(norm.X, norm.Y, norm.Z);
//         }
//     }
//
//     // Экспортируем измененную модель
//     context.ExportFile(scene, outputFilePath, "3ds");
// }
//
// void FlipModel(string inputFilePath, string outputFilePath)
// {
//     AssimpContext context = new AssimpContext();
//
//     // Загрузка модели
//     Assimp.Scene scene = context.ImportFile(inputFilePath, PostProcessSteps.Triangulate | PostProcessSteps.GenerateNormals);
//
//     // Создаем матрицу поворота (90 градусов вокруг оси X)
//     var rotationMatrix = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(90f));
//
//     // Применяем матрицу поворота ко всем вершинам и нормалям
//     foreach (var mesh in scene.Meshes)
//     {
//         for (int i = 0; i < mesh.VertexCount; i++)
//         {
//             // Отражаем вершины
//             Vector3D v = mesh.Vertices[i];
//             mesh.Vertices[i] = new Vector3D(v.X, -v.Y, v.Z); // Flip по Y
//
//             if (mesh.HasNormals)
//             {
//                 Vector3D n = mesh.Normals[i];
//                 mesh.Normals[i] = new Vector3D(n.X, -n.Y, n.Z); // Flip по Y
//             }
//         }
//     }
//
//     // Экспортируем измененную модель
//     context.ExportFile(scene, outputFilePath, "3ds");
// }
//
// void ChangeColor(string inputFilePath, string outputFilePath)
// {
//     AssimpContext context = new AssimpContext();
//
//     // Загрузка модели
//     Scene scene = context.ImportFile(inputFilePath, PostProcessSteps.Triangulate | PostProcessSteps.GenerateNormals);
//     
//     if (scene.MaterialCount > 0)
//     {
//         foreach (var material in scene.Materials)
//         {
//             
//             // Изменим цвет диффуза (основной цвет)
//             material.ColorDiffuse = new Color4D(
//                 1 - material.ColorDiffuse.R, 
//                 1 - material.ColorDiffuse.G,
//                 1 - material.ColorDiffuse.B, 
//                 1 - material.ColorDiffuse.A);
//             
//             material.ColorAmbient = new Color4D(
//                 1 - material.ColorAmbient.R, 
//                 1 - material.ColorAmbient.G,
//                 1 - material.ColorAmbient.B, 
//                 1 - material.ColorAmbient.A);
//             
//             material.ColorDiffuse = new Color4D(
//                 1 - material.ColorDiffuse.R, 
//                 1 - material.ColorDiffuse.G,
//                 1 - material.ColorDiffuse.B, 
//                 1 - material.ColorDiffuse.A);
//
//             material.ColorAmbient = new Color4D(0.2f, 0.2f, 0.2f, 1.0f);
//             material.ColorSpecular = new Color4D(1.0f, 1.0f, 1.0f, 1.0f);
//         }
//     }
//
//     // Экспортируем измененную модель
//     context.ExportFile(scene, outputFilePath, "3ds");
// }