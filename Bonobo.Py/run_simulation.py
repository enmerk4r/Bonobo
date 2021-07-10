
import bpy
import json
import rhino3dm
from pprint import pprint
import mathutils
import argparse
import uuid
import os

def load_simulation(path, steps, start, end):
    # Clear the scene
    candidate_list = [item.name for item in bpy.data.objects if item.type == "MESH"]

    # select them only.
    for object_name in candidate_list:
      bpy.data.objects[object_name].select_set(True)

    # remove all selected.
    bpy.ops.object.delete()

    # remove the meshes, they have no users anymore.
    for item in bpy.data.meshes:
      bpy.data.meshes.remove(item)


    with open(path, "r") as f:
        mesh_list = json.load(f)
        
        
    ## Create Rigid Body Sim
    for mesh_string in mesh_list:   
        mesh = json.loads(mesh_string)
        m = rhino3dm.CommonObject.Decode(json.loads(mesh["mesh"]))


        vertices = []
        edges = []
        faces = []

        for i in range(len(m.Vertices)):
            v = m.Vertices[i]
            vertices.append((v.X, v.Y, v.Z))
            

        for i in range(len(m.Faces)):
            faces.append(m.Faces[i])


        new_mesh = bpy.data.meshes.new('new_mesh')  
        new_mesh.from_pydata(vertices, edges, faces)


            
        new_mesh.update()

        new_object = bpy.data.objects.new('new_object', new_mesh)
        new_object.data = new_mesh    

        new_collection = bpy.data.collections[0]
        # add object to scene collection
        new_collection.objects.link(new_object)

        bpy.context.view_layer.objects.active = new_object
        new_object.select_set(True)

        bpy.ops.object.origin_set(type='ORIGIN_GEOMETRY', center='BOUNDS')
        cursor = bpy.context.scene.cursor

        bpy.ops.rigidbody.object_add()
        bpy.context.object.rigid_body.mass = mesh["mass"]
        bpy.context.object.rigid_body.collision_shape = mesh["collision_shape"]
        bpy.context.object.rigid_body.friction = mesh["friction"]
        bpy.context.object.rigid_body.use_margin = mesh["use_margin"]
        bpy.context.object.rigid_body.collision_margin = mesh["collision_margin"]
        bpy.context.object.rigid_body.linear_damping = mesh["linear_damping"]
        bpy.context.object.rigid_body.angular_damping = mesh["angular_damping"]
        if mesh["is_active"]:
            bpy.context.object.rigid_body.type = 'ACTIVE'
        else:
            bpy.context.object.rigid_body.type = 'PASSIVE'
        
            
    bpy.context.scene.rigidbody_world.steps_per_second =  steps
    bpy.context.scene.frame_start = start
    bpy.context.scene.frame_end = end
        
    bpy.context.scene.rigidbody_world.point_cache.frame_start = start
    bpy.context.scene.rigidbody_world.point_cache.frame_end = end
    

def export_frame(frame, directory):
    rhinoMeshes = []
    for obj in bpy.data.objects:
        if (obj.type == 'MESH'):
            mesh = obj.data
            
            rhinoMesh = rhino3dm.Mesh()
            
            for v in mesh.vertices:
                c = obj.matrix_world @ v.co
                rhinoMesh.Vertices.Add(c.x, c.y, c.z)
            
            for p in mesh.polygons:           
                face = []
                for v in (p.vertices):
                    face.append(v)
                if len(face) == 3:
                    rhinoMesh.Faces.AddFace(face[0], face[1], face[2])
                elif len(face) == 4:
                    rhinoMesh.Faces.AddFace(face[0], face[1], face[2], face[3])
                    
            
            rhinoMeshes.append(rhinoMesh)

    stringList = []
    for m in rhinoMeshes:
        stringList.append(json.dumps(rhino3dm.CommonObject.Encode(m)))
        
    
    

    frame = bpy.data.scenes[0].frame_current

    with open("{0}\\{1}.json".format(directory, frame), "w") as f:
        f.write(json.dumps(stringList))
    
    
def export_scene(rootDir, folderName):
    path = os.path.join(rootDir, folderName)
    os.mkdir(path)
    
    scene = bpy.context.scene
    for f in range(scene.frame_start, scene.frame_end + 1):
        bpy.context.scene.frame_set(f)
        export_frame(f, path)


if __name__ == "__main__":
    parser = argparse.ArgumentParser()

    parser.add_argument('sim_path', metavar='P', type=str, help='Path to the simulation JSON', nargs="?")
    parser.add_argument('steps', metavar='s', type=str, help='Simulation steps per second', nargs="?")
    parser.add_argument('start', metavar='S', type=str, help='Simulation start frame', nargs="?")
    parser.add_argument('end', metavar='E', type=str, help='Simulation end frame', nargs="?")
    parser.add_argument('root', metavar='R', type=str, help='Root folder for exported meshes', nargs="?")
    parser.add_argument('folder_name', metavar='F', type=str, help='Destination folder for exported meshes', nargs="?")

    args = parser.parse_args()

    a = {
        "sim_path": args.sim_path,
        "steps" : args.steps,
        "start" : args.start,
        "end" : args.end,
        "root" : args.root,
        "folder_name" : args.folder_name
        }

    pprint(a)
    
    load_simulation(args.sim_path, int(args.steps), int(args.start), int(args.end))
    export_scene(args.root, args.folder_name)

