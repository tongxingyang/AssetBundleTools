﻿using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AssetBundleBuilder
{
    /// <summary>
    /// 打包前，更新项目文件
    /// </summary>
    public class SvnUpdateBuilding : ABuilding
    {

        public SvnUpdateBuilding() : base(10)
        {
        }

        public override IEnumerator OnBuilding()
        {
            return base.OnBuilding();
        }



        /// <summary>
        /// 文件版本文件
        /// </summary>
        public static void UpdateVersion()
        {
            SVNUtility.Update(BuilderPreference.VERSION_PATH);
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 更新项目根目录
        /// </summary>
        public static void UpdateAll()
        {
            SVNUtility.Update(Application.dataPath);
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 更新打包备份目录的AB资源
        /// </summary>
        public static void UpdateTempAssets()
        {
            string path = ABPackHelper.TEMP_ASSET_PATH + LuaConst.osDir;
            if (Directory.Exists(path))
            {
                SVNUtility.Update(path);
                AssetDatabase.Refresh();
            }
        }
        /// <summary>
        /// 更新打包存储目录的资源
        /// </summary>
        public static void UpdateAssets()
        {
            string path = ABPackHelper.ASSET_PATH + LuaConst.osDir;
            SVNUtility.Update(path);
            AssetDatabase.Refresh();

            var destVersionPath = ABPackHelper.VERSION_PATH + "version.txt";
            if (!File.Exists(destVersionPath)) destVersionPath = ABPackHelper.ASSET_PATH + LuaConst.osDir + "/version.txt";
            var versionPath = ABPackHelper.BUILD_PATH + LuaConst.osDir + "/version.txt";
            File.Copy(destVersionPath, versionPath, true);
            if (File.Exists(versionPath) && AssetBundleEditor.gameVersion == null)
                AssetBundleEditor.gameVersion = GameVersion.CreateVersion(File.ReadAllText(versionPath));
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 更新配置表
        /// </summary>
        public static void UpdateTableOnly()
        {
            string path = LuaConst.luaDir + "/xlsdata/";
            SVNUtility.Update(path);
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 更新Lua
        /// </summary>
        public static void UpdateLuaOnly()
        {
            SVNUtility.Update(LuaConst.luaDir);
            AssetDatabase.Refresh();
        }
    }
}