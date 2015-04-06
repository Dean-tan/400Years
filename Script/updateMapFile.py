import os
import shutil

# def copy_rename(old_file_name, new_file_name):
#         src_dir= os.curdir
#         dst_dir= os.path.join(os.curdir , "subfolder")
#         src_file = os.path.join(src_dir, old_file_name)
#         shutil.copy(src_file,dst_dir)
        
#         dst_file = os.path.join(dst_dir, old_file_name)
#         new_dst_file_name = os.path.join(dst_dir, new_file_name)
#         os.rename(dst_file, new_dst_file_name)

shutil.copy("../MapTile/map.tmx", "../Project/Assets/Resources/GameMaps/")
os.rename("../Project/Assets/Resources/GameMaps/map.tmx", "../Project/Assets/Resources/GameMaps/map.xml")